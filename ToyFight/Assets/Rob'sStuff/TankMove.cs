using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMove : MonoBehaviour {
    
    //public float speed = 0;
    //bool shouldMove = true;
    //bool stopped = false;
    //bool turned = false;

    public NavMeshAgent TankNPC;
    public Transform[] Stops;

    private int counter = 0;


	// Use this for initialization
	void Start ()
    {
        TankNPC.SetDestination(Stops[counter].position);

        TankNPC.updatePosition = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        TankNPC.transform.position = new Vector3(TankNPC.nextPosition.x,TankNPC.transform.position.y, 0);
        if(TankNPC.velocity.magnitude   < 200 && (TankNPC.transform.position - TankNPC.pathEndPosition).sqrMagnitude < 200)
        {
            counter += 1;
            counter %= Stops.Length;
            TankNPC.SetDestination(Stops[counter].position);
            
        }
        Debug.Log(counter);
    }

    

}


/* moveLeft();
       if (stopped == true)
       {
           turnAround();

       }
       moveRight();
*/

//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!      OLD CODE      !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

/*
    int counter = 1;
    
    void moveLeft()
    {
        speed = 5f;
        if(shouldMove == true)
        {

            transform.Translate((-speed * Time.deltaTime) * 5, 0, 0);
        }
       
        
    }

    void moveRight()
    {
        speed = 5f;
        if(shouldMove == true && turned == true)
        {
           transform.Translate((speed * Time.deltaTime) * 5, 0, 0);
        }
       
    }
    void turnAround()
    {
        bool thing = false;
        speed = 5f;
          counter = counter % 220;
          Debug.Log(transform.forward);
        if (counter != 0)
        {
            transform.Rotate((Vector3.up * -speed * Time.deltaTime) * 11);

            counter++;
        }
        else
        {
            this.transform.forward = Vector3.back;
            
        }
    }


    
    void stopMove()
    {
        
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
       
        Debug.Log("attempting to stop");
    }

    public void OnCollisionEnter(Collision collision)
    {
        stopMove();
        if (collision.gameObject.tag == "invisibleWall")
        {
            Debug.Log("Hit stop 1");
            stopped = true;
            shouldMove = false;   ///Change shouldMove
        }
        
    }
    */
