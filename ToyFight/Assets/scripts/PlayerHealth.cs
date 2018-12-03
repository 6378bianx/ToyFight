using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	private int health = 250;
	private int attack = 2;
	private float armour = 0.0f;
	private int damage;

	public BearBoss bear;
	public TankHealth tank;

	// Use this for initialization
	void Start () {
		health = 250;
		Debug.Log ("health" + health);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (this.gameObject);

			//yield return new WaitForSeconds (2f);
			SceneManager.LoadScene ("MainMenu");
		}
	}


	public void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Bear") {
			DamageTaken (bear.getDamage());
			Debug.Log ("health" + health);
		}
		if (col.gameObject.tag == "tankFloat") {
			DamageTaken (tank.getDamage ());
			Debug.Log ("health" + health);
		}
		if (col.gameObject.tag == "tankBullet") {
			DamageTaken (tank.getDamage ());
			Debug.Log ("health" + health);
		}
	
	}

    public int GetHealth()
    {
        return health;
    }

	public void DamageTaken(int damage)
	{
		health -= damage;
	}

	public int getAttack()
	{
		return attack;
	}

	public void SetAttack(int attack)
	{
		this.attack = attack;
	}

	public void SetArmour(float armour)
	{
		this.armour = armour;
	}
	public float getArmour()
	{
		return armour;
	}

}
