using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFloat : MonoBehaviour {

    public float hoverForce = 12f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "tankFloat")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
        }
    }
}
