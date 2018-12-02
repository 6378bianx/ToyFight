using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    public GameObject spwanObject;
    // Use this for initialization
    public GameObject bullet;
    public Transform newPosition;
    public float fireRate = 0.85f;
    private float nextFire;


    void Start()
    {
        nextFire = Time.time + fireRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            // Update the time when our player can fire next
            float randtime = Random.Range(0, 2);
            nextFire = Time.time + fireRate + randtime;
            GameObject spawnbullet = Instantiate(bullet, newPosition.position, Quaternion.identity);
           spawnbullet.GetComponent<Rigidbody>().AddForce(newPosition.transform.forward * 2500);
            spawnbullet.transform.forward = newPosition.transform.forward;
            Destroy(spawnbullet, 3 + (randtime * 0.283f));
        }

    }
}