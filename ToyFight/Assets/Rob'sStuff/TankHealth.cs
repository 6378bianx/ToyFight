using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankHealth : MonoBehaviour {
    public static int tankHealth = 450;
    public PlayerHealth player;
	// Use this for initialization
	void Start () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "playerBullet")
        {
            damageTaken();
            Debug.Log("tank-health: " + tankHealth);
            Destroy(collision.gameObject);
        }
    }


    public void damageTaken()
    {
        tankHealth -= player.getAttack()*5;
    }
    // Update is called once per frame
    void Update () {
		if(tankHealth <= 0)
        {
            GameObject Tank = GameObject.FindGameObjectWithTag("tankFloat");
            GameObject fire = GameObject.FindGameObjectWithTag("we'llbangokay");
            Destroy(fire);
            NavMeshAgent tanknav = Tank.GetComponent<NavMeshAgent>();
            tanknav.updatePosition = false;
            tanknav.updateRotation = false;
            tanknav.SetDestination(Tank.transform.position);
            tanknav.nextPosition = Tank.transform.position;
            Destroy(Tank,1.5f);
        }
	}
}
