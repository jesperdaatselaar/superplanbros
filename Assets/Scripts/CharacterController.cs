using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D characterRigidBody;
    private SpriteRenderer spriteRenderer;
    private float moveHorizontal;
    private float moveVertical;
    [SerializeField]
    private float movementSpeed = 3f;
    [SerializeField]
    private float jumpForce = 300f;
    private Vector2 currentVelocity;
    private bool isJumping = false;
    private bool alreadyJumped = false;

    [SerializeField]
    private Sprite directionSprite;
    [SerializeField]
    private Sprite jumpSprite;
    [SerializeField]
    private Sprite idleSprite;



    // Start is called before the first frame update
    void Start()
    {
        this.characterRigidBody = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        this.moveHorizontal = Input.GetAxis("Horizontal"); // X-Axis
        this.moveVertical = Input.GetAxis("Vertical"); // Y-Axis
        this.currentVelocity = this.characterRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping)
            {
                isJumping = true;
                alreadyJumped = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (this.moveHorizontal != 0)
        {
            this.characterRigidBody.velocity = new Vector2(this.moveHorizontal * this.movementSpeed, this.currentVelocity.y);
        }
        if (isJumping && !alreadyJumped)
        {
            this.characterRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            this.alreadyJumped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("background"))
        {
            this.isJumping = false;
        }
    }
    private void UpdateSprite()
    {
        if (!isJumping)
        {
            if (this.moveHorizontal == 0)
            {
                this.spriteRenderer.sprite = idleSprite;
            }
            else
            {
                this.spriteRenderer.sprite = directionSprite;
                if (moveHorizontal > 0)
                {
                    // MoveHorizontal is bigger than 0, meaning we are going right.
                    // Rotate the sprite to 0 on the Y-axis to make sure it is right-facing.
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    // MoveHorizontal is smaller than 0, meaning we are going left
                    // Rotate the sprite to 180 on the Y-axis to make sure it is left-facing.
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
        else
        {
            this.spriteRenderer.sprite = jumpSprite;
            if (this.moveHorizontal < 0)
            {
                // load jumping left sprite
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // load jumping right sprite
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
