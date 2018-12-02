using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed_scale;
	public float bullet_scale;
	public float jump_scale;
    public PlayerHealth playerHealth;

	public Transform bulletSpawn;
	public GameObject bullet;


	private float atTime = 0.5f;

	private float fireRate = 0.5f;
	private float nextFire = 0.0f;

	private bool onGround = true;
	private bool canJump = true;
	private Vector3 jump;
	private Rigidbody thisRigidbody;

	private bool mainPos = true;

	private float y = 5.0f;


	// Use this for initialization
	void Start () {

		thisRigidbody = GetComponent<Rigidbody> ();
		jump = new Vector3(0f,2.0f,0f);

	}

	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed_scale;

		if (mainPos) {
			transform.Translate (0, 0, x);
			Shoot (y);
		} else {
			transform.Translate (0, 0, -x);
			Shoot (-y);
		}


		if (Input.GetKey(KeyCode.A)) {
			if (mainPos) {
				mainPos = false;
				transform.Rotate (new Vector3(0f, 180f, 0f));

			}
			//transform.Translate (0, 0, -x);
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			if (!mainPos) {
				mainPos = true;
				transform.Rotate (new Vector3(0f, 180f, 0f));
			}

		}

		Jump();
		DoubleJump ();
		getDown ();


	}

	public void getDown()
	{
		if(Input.GetKeyDown(KeyCode.S)){
			this.thisRigidbody.AddForce (new Vector3(0f, -2.0f, 0f) * 20f, ForceMode.Impulse);

		}
	}

	//Jump function
	public void Jump()
	{

		if (Input.GetKeyDown(KeyCode.W) && canJump) {
			thisRigidbody.AddForce (jump * jump_scale, ForceMode.VelocityChange);
			onGround = false;

			Debug.Log ("Jump was pressed");
		}
	}

	//double jump function
	public void DoubleJump()
	{
		if (Input.GetKeyDown(KeyCode.P) && onGround) {
			thisRigidbody.AddForce (jump * jump_scale, ForceMode.VelocityChange);
			onGround = false;

			Debug.Log ("Jump was pressed");
		}
	}


	public void OnCollisionStay()
	{
		onGround = true;
	}

	public void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "jumpable") {
			canJump = true;
		}

		//NEED TO WORK ON BOUNCE BACK 
		if (col.gameObject.tag == "enemyBody") {
			

			}
        if(col.gameObject.name == "Rainbow")
        {
            playerHealth.DamageTaken(100);
        }
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Beam")
        {
            playerHealth.DamageTaken(1);
            Debug.Log("Current Health: " + playerHealth.GetHealth());
        }
    }

    public void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "jumpable") {
			canJump = false;
		}
	}


	//Shooting with a delay
	public void Shoot(float y)
	{		
		

		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			GameObject bulletInstance = Instantiate (bullet, bulletSpawn.position, bullet.transform.rotation);

			bulletInstance.GetComponent<Rigidbody>().AddForce(new Vector3 (y, 0f,0f) * bullet_scale, ForceMode.Acceleration);

			Destroy (bulletInstance, atTime);
		}
	}

	public void setFireRate(float fireRate)
	{
		this.fireRate = fireRate;
	}

	public void setSpeedScale(float speed_scale)
	{
		this.speed_scale = speed_scale;
	}
}
