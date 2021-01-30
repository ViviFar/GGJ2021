using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,10.0f)]
    private float speed = 4.0f;
    [SerializeField]
    private float jumpForce = 600.0f;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheck;

    private float speedModifier = 0.0f;
    
    float vx, vy;

    private Vector3 positionInitial;
    private Rigidbody2D rg;
    private bool canJump = true;

    private bool facingRight = true;

    private int playerLayer;
    private int platformLayer;

    // Start is called before the first frame update
    void Awake()
    {
        positionInitial = transform.position;

        rg = GetComponentInChildren<Rigidbody2D>();
        if (rg == null)
        {
            Debug.LogError("No Rigidbody2D attached to the player");
        }

        playerLayer = gameObject.layer;
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        MoveLeftRight();
        vy = rg.velocity.y;
        canJump = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        rg.velocity = new Vector2(vx, vy);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, (vy > 0.0f));
    }

    private void MoveLeftRight()
    {
        vx = Input.GetAxisRaw("Horizontal") * speed + speedModifier;
    }

    private void Jump()
    {
        if (canJump)
        {
            vy = 0;
            canJump = false;
            rg.AddForce(new Vector2(0, jumpForce));
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConveyorBelt")
        {
            ConveyorBelt cb = collision.gameObject.GetComponent<ConveyorBelt>();
            speedModifier = cb.Speed;
            cb.PlayerIsOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConveyorBelt")
        {
            ConveyorBelt cb = collision.gameObject.GetComponent<ConveyorBelt>();
            speedModifier = 0;
            cb.PlayerIsOn = false;
        }
    }
}
