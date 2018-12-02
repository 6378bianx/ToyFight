using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBoss : MonoBehaviour {

    public Transform beam;
    public float jumpVelocity;
    public float push_player;
    public float bear_rush;
    public float movement_speed;

    private GameObject player;
    private Vector3 patrol_1;
    private Vector3 patrol_2;
    private Vector3 currentMovement;
    public bool stop_patrol;
    public bool stop_movement;
    public bool attack_activated;
    public bool beam_activated;
    private const string PLAYER = "Player";
    private const string PLATFORM = "Platform";
    private const int EPSILON = 1;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag(PLAYER);
        patrol_1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        patrol_2 = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        currentMovement = patrol_2;
        stop_patrol = false;
        stop_movement = true;
        attack_activated = false;
        beam_activated = false;
    }

    // Update is called once per frame
    void Update() {
        MoveTowardPlayer();
        Patrol();
        CareBearBeam();
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PLATFORM &&
                !(collision.collider.bounds.max.y < GetComponent<Collider>().bounds.max.y &&
                collision.collider.bounds.min.y < GetComponent<Collider>().bounds.min.y))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if(collision.gameObject.tag == PLAYER)
        {
            if (player.transform.position.x > transform.position.x)
            {
                player.GetComponent<Rigidbody>().velocity = Vector3.right * push_player;
                GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                player.GetComponent<Rigidbody>().velocity = Vector3.left * push_player;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    IEnumerator PowerPunch(int num)
    {
        yield return new WaitForSeconds(.5f);
        if(num == 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.right * bear_rush;
        }
        if(num == 1)
        {
            GetComponent<Rigidbody>().velocity = Vector3.left * bear_rush;
        }
        
        attack_activated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLATFORM && player.transform.position.y > transform.position.y && IsGrounded() && !stop_movement && stop_patrol)
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>(), false);
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
        }
        if(other.gameObject.tag == PLAYER)
        {
            if (player.transform.position.x > transform.position.x)
            {
                StopCoroutine(PowerPunch(0));
                StartCoroutine(PowerPunch(0));
                attack_activated = true;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                StopCoroutine(PowerPunch(1));
                StartCoroutine(PowerPunch(1));
                attack_activated = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == PLAYER)
        {
            stop_movement = true;
            stop_patrol = false;
        }
    }

    private void Patrol()
    {
        if (!stop_patrol && !attack_activated)
        {
            float step = movement_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentMovement, step);

            if ((transform.position - patrol_1).magnitude < EPSILON)
            {
                currentMovement = patrol_2;
            }
            if ((transform.position - patrol_2).magnitude < EPSILON)
            {
                currentMovement = patrol_1;
            }
        }
    }

    private void MoveTowardPlayer()
    {
        if (!stop_movement && !attack_activated)
        {
            float step = movement_speed * Time.deltaTime;
            Vector3 offset = new Vector3(player.transform.position.x, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, offset, step);
        }
    }

    private void CareBearBeam()
    {
        beam_activated = true;
        if (beam_activated)
        {
            beam.LookAt(player.transform);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    private bool IsPlayerGrounded()
    {
        return Physics.Raycast(player.transform.position, -Vector3.up, player.GetComponent<Collider>().bounds.extents.y + 0.1f);
    } 
}

