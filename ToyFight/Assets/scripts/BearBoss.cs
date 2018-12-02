using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBoss : MonoBehaviour {

    public GameObject rainbow;
    public GameObject beam;
    public Transform rainbow_location;
    public float jumpVelocity;
    public float push_player;
    public float bear_rush;
    public float movement_speed;
    public int bear_health;
    public float attack_countdown;
    public PlayerHealth playerHealth;

    private GameObject player;
    private ParticleSystem beamPS;
    private Vector3 patrol_1;
    private Vector3 patrol_2;
    private Vector3 currentMovement;
    public bool stop_patrol;
    public bool stop_movement;
    public bool attack_activated;
    public bool attack_inProgress;
    public bool beam_activated;
    public bool rainbow_activated;
    public bool timer_active;
    private const string PLAYER = "Player";
    private const string PLATFORM = "Platform";
    private const int EPSILON = 1;


    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag(PLAYER);
        beamPS = beam.GetComponent<ParticleSystem>();
        patrol_1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        patrol_2 = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        currentMovement = patrol_2;
        stop_patrol = false;
        stop_movement = true;
        attack_activated = false;
        beam_activated = false;
        rainbow_activated = false;
        attack_inProgress = false;
        timer_active = true;
        beamPS.Stop();
        bear_health = 100;
    }

    // Update is called once per frame
    void Update() {
        MoveTowardPlayer();
        Patrol();
        SelectAttack();
    }

    private void FixedUpdate()
    {
        CareBearBeam();
        Rainbow();
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
        if(collision.gameObject.name == "bullet(Clone)")
        {
            bear_health -= playerHealth.getAttack();
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

    private void SelectAttack()
    {
        if (timer_active)
        {
            attack_countdown++;
        }

        if (attack_countdown % 360 == 0)
        {
            beam_activated = true;
        }
        if (attack_countdown % 800 == 0)
        {
            rainbow_activated = true;
        }
    }

    private void Rainbow()
    {
        if (rainbow_activated && !attack_inProgress)
        {
            Debug.Log("Can i see this");
            transform.LookAt(GameObject.FindWithTag("MainCamera").transform);
            StartCoroutine(_Rainbow());
        }
    }

    IEnumerator _Rainbow()
    {
        attack_inProgress = true;
        stop_movement = true;
        stop_patrol = true;
        timer_active = false;
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = rainbow_location.position;
        rainbow.SetActive(true);
        yield return new WaitForSeconds(3f);
        rainbow.SetActive(false);
        GetComponent<Rigidbody>().useGravity = true;
        stop_patrol = false;
        timer_active = true;
        attack_inProgress = false;


    }

    private void CareBearBeam()
    {
        if (beam_activated && !attack_inProgress)
        {
            //Debug.Log("Can i see this");
            beam.transform.LookAt(player.transform);
            StartCoroutine(Beam());
        }
    }

    IEnumerator Beam()
    {
        timer_active = false;
        stop_patrol = true;
        attack_inProgress = true;
        beam.SetActive(true);
        beamPS.Play();
        yield return new WaitForSeconds(3f);
        beamPS.Stop();
        beam.SetActive(false);
        beam_activated = false;
        attack_inProgress = false;
        timer_active = true;
        stop_patrol = false;

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

