using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float jumpForce = 3.0f;

    private Vector3 position;
    private Rigidbody2D rg;
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            canJump = true;
        }
#endif
    }

    private void Move()
    {
        float vitesse = Input.GetAxisRaw("Horizontal") * speed;
        rg.velocity = new Vector2(vitesse, rg.velocity.y);
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
}
