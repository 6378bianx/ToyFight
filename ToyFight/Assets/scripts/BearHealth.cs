using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHealth : MonoBehaviour {

    public PlayerHealth player;
    public int bear_health;
    private int bear_damage = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public int getDamage()
	{
		return bear_damage;
	}
    
}
