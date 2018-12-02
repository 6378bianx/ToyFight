using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	private int health = 100;
	private int attack = 2;
	private float armour = 0.0f;
	private int damage;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnCollisionEnter(Collision col)
	{
		
	
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
