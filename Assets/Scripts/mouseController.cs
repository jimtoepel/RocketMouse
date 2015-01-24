using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {

	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	public Transform groundCheckTransform;
	public bool grounded;
	public LayerMask groundCheckLayerMask;
	public ParticleSystem jetpack;
	public Texture2D coinIconTexture;
	public AudioClip coinCollectSound;
	public AudioClip hitByLazerSound;
	public AudioSource jetpackAudio;
	public AudioSource footstepsAudio;
	public UnityEngine.UI.Text coinsLabel;
	public GameObject restartDialog;

	public ParallaxScrolling parallax;
	
	private bool dead = false;
	private uint coins = 0;



Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		restartDialog.SetActive (false);
	}


	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate () {
		bool jetpackActive = Input.GetButton ("Fire1");

		jetpackActive = jetpackActive && !dead;

		if (jetpackActive) {

			rigidbody2D.AddForce(new Vector2(0, jetpackForce));

		}

		if (!dead) {
		
			Vector2 newVelocity = rigidbody2D.velocity;
			newVelocity.x = forwardMovementSpeed;
			rigidbody2D.velocity = newVelocity;

		}

		UpdateGroundedStatus();
		AdjustJetpack (jetpackActive);
		AdjustFootstepsAndJetpackSound (jetpackActive);
		parallax.offset = transform.position.x;
	}


	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.1f, groundCheckLayerMask);


		//2
		animator.SetBool ("grounded", grounded);

	}


	void AdjustJetpack (bool jetpackactive)
	{
		jetpack.enableEmission = ! grounded;
		jetpack.emissionRate = jetpackactive ? 300.0f : 75.0f;
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag ("Coins"))
			CollectCoin (collider);
		if (collider.gameObject.CompareTag ("Lasers"))
			HitByLaser (collider);
	}


	void HitByLaser(Collider2D laserCollider)
	{
		if (!dead)
			AudioSource.PlayClipAtPoint (hitByLazerSound, transform.position);
		dead = true;
		animator.SetBool ("dead", true);
		restartDialog.SetActive (true);
	}


	void CollectCoin(Collider2D coinCollider)
	{
		coins ++;
		DestroyObject (coinCollider.gameObject);
		AudioSource.PlayClipAtPoint (coinCollectSound, transform.position);
		coinsLabel.text = coins.ToString ();
	}


	void AdjustFootstepsAndJetpackSound(bool jetpackActive)
	{
		footstepsAudio.enabled = !dead && grounded;

		jetpackAudio.enabled = !dead && !grounded;
		jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;

	}


	public void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void ExitToMenu()
	{
		Application.LoadLevel ("MenuScene");
	}
}
