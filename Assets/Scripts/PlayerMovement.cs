using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite; 
    private Animator anim;

    [SerializeField] private LayerMask jumableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    //An enum is a special "class" that represents a group of constants (unchangeable/read-only variables).
     //To create an enum, use the enum keyword (instead of class or interface), 
     //and separate the enum items with a comma:
    private enum MovementState { idle, running, jumping, falling}
    [SerializeField] private AudioSource jumpSoundEffect;
  //  private MovementState state = MovementState.idle;
   /* int wholeNumber = 16;
    float decimalNumber = 4.54f;
    string text = "blabla";
    bool boolean = false;
*/
    // Start is called before the first frame update
    void Start()
    {
      //  Debug.Log("Hello,world!");
       rb =  GetComponent<Rigidbody2D>();
       sprite = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
       coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
         
        dirX = Input.GetAxisRaw("Horizontal");//return float ..
         rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

       if (Input.GetButtonDown("Jump") && IsGrounded())//getbuttondown return true false
       {
            jumpSoundEffect.Play();
         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       }

       UpdateAnimationState();  
    
   }
   private void UpdateAnimationState()
   {
      MovementState state;
      if(dirX > 0f)
      {
        state = MovementState.running;
         //anim.SetBool("running", true);
         sprite.flipX = false;
      }
      else if(dirX < 0f)
       {
         state = MovementState.running;
         sprite.flipX = true;
      } 
      else{
        state = MovementState.idle;
      }

      if (rb.velocity.y > .1f)
      {
          state = MovementState.jumping;
      }
      else if (rb.velocity.y < -.1f)
      {
          state = MovementState.falling;
      }
      anim.SetInteger("state", (int)state);
   }
   private bool IsGrounded()
   {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumableGround);
   }

}
