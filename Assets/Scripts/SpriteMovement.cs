using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{ 
    private Rigidbody2D rb;
    private Animator animate;
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;
    private CircleCollider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
        AnimationState();
        animate.SetInteger("State", (int)state);
    }

    private void MovementController()
    {
        float hdirection = Input.GetAxis("Horizontal");

        if (hdirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            Debug.Log("left");
            transform.localScale = new Vector2(-7, 7);

        }
        else if (hdirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Debug.Log("Right");
            transform.localScale = new Vector2(7, 7);
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    void AnimationState(){
        if(state == State.jumping){
             
            if(rb.velocity.y < 0.1f){
                state = State.falling;
            }

       }else if(state == State.falling){

            if(coll.IsTouchingLayers(ground)){
                state = State.idle;
            }

        }else if (Mathf.Abs(rb.velocity.x) > 2f){

            state = State.running;

        }else{

            state = State.idle;

        }
    }

}
