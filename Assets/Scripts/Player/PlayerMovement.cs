using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#region fields Utilisateur
    [SerializeField]
    [Range(0.0f,10.0f)]
    private float speed = 4.0f;
    [SerializeField]
    private float jumpForce = 600.0f;

    [SerializeField]
    private float jumpForceFromConveyor = 300.0f; 

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private LayerMask EnvironmentLayer;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Animator anim;
    #endregion

#region private properties
    private float speedModifier = 0.0f;
    
    float vx, vy;

    private Vector3 positionInitial;
    private Rigidbody2D rg;
    private bool canJump = true;

    private bool facingRight = true;
    private bool onConveyorBelt = false;


    private int playerLayer;
    private int platformLayer;

    private BoxCollider2D col;
#endregion
    
    // Start is called before the first frame update
    void Awake()
    {
        positionInitial = transform.position;
        col = GetComponent<BoxCollider2D>();
        rg = GetComponentInChildren<Rigidbody2D>();
        if (rg == null)
        {
            Debug.LogError("No Rigidbody2D attached to the player");
        }

        playerLayer = gameObject.layer;
        platformLayer = LayerMask.NameToLayer("Platform");
        anim.SetBool("Moving", false);
        anim.SetBool("Jumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        MoveLeftRight(Time.deltaTime);
        vy = rg.velocity.y;
        canJump = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        if (canJump)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        rg.velocity = new Vector2(vx, vy);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, (vy > 0.0f));
    }

    private void MoveLeftRight(float deltaTime)
    {
        vx = Input.GetAxisRaw("Horizontal") * speed;
        if(vx != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        vx += speedModifier;
        Vector2 moveDir = new Vector2(vx * deltaTime, 0.2f);
        Vector2 bottomRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
        Vector2 topLeft = new Vector2(col.bounds.min.x, col.bounds.min.y);
        bottomRight += moveDir;
        topLeft += moveDir;
        if(Physics2D.OverlapArea(topLeft, bottomRight, EnvironmentLayer)){
            vx = 0;
        }
        if (vx < 0 && transform.localScale.x>0)
        {
            transform.localScale = new Vector3(-1* transform.localScale.x, transform.localScale.y);
        }
        else if(vx> 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
    }

    private void Jump()
    {
        if (canJump)
        {
            anim.SetBool("Jumping", true);
            vy = 0;
            canJump = false;
            if (onConveyorBelt)
                rg.AddForce(new Vector2(jumpForceFromConveyor, jumpForce));
            else
                rg.AddForce(new Vector2(0, jumpForce));
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConveyorBelt")
        {
            onConveyorBelt = true;
            ConveyorBelt cb = collision.gameObject.GetComponent<ConveyorBelt>();
            speedModifier = cb.Speed;
            cb.PlayerIsOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConveyorBelt")
        {
            onConveyorBelt = false;
            ConveyorBelt cb = collision.gameObject.GetComponent<ConveyorBelt>();
            speedModifier = 0;
            cb.PlayerIsOn = false;
        }
    }

    public void UpdateSpeedModifier(float newSpeed)
    {
        speedModifier = newSpeed;
    }
    
}
