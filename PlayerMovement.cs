using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState{
	walk,
	Attack,
	interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{

	public PlayerState currentState;
	public float movSpeed = 5f;

	public Rigidbody2D myRigidbody;
	public Animator animator;

	Vector2 movement;

	void Start(){
		currentState = PlayerState.walk;
		animator.GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
		animator.SetFloat("Horizontal", 0);
		animator.SetFloat("Vertical", -1);

	}

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.Attack && currentState != PlayerState.stagger){
        	StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle){
        	UpdateAnimationAndMove();
        }
        
    }

    private IEnumerator AttackCo(){
    	animator.SetBool("Attacking", true);
    	currentState = PlayerState.Attack;
    	yield return null;
    	animator.SetBool("Attacking", false);
    	yield return new WaitForSeconds(.3f);
    	currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove(){
    	if (movement != Vector2.zero) {
        	animator.SetFloat("Horizontal", movement.x);
        	animator.SetFloat("Vertical", movement.y);
        	animator.SetBool("Moving", true);
        } else {
        	animator.SetBool("Moving", false);
        }

    }

    void FixedUpdate() 
    {
    	movement.Normalize();
    	myRigidbody.MovePosition(myRigidbody.position + movement * movSpeed * Time.fixedDeltaTime);
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime){
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime){
        if(myRigidbody != null){
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
