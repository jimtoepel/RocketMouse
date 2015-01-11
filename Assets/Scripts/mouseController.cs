using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

public float jetpackForce = 75.0f;
public float forwardMovementSpeed = 3.0f;
public Transform groundCheckTransform;
public bool grounded;
public LayerMask groundCheckLayerMask;

Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		bool jetpackActive = Input.GetButton ("Fire1");

		if (jetpackActive) {

			rigidbody2D.AddForce(new Vector2(0, jetpackForce));

		}

		Vector2 newVelocity = rigidbody2D.velocity;
		newVelocity.x = forwardMovementSpeed;
		rigidbody2D.velocity = newVelocity;

		UpdateGroundedStatus();

	}

	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.1f, groundCheckLayerMask);


		//2
		animator.SetBool ("grounded", grounded);

	}


}
