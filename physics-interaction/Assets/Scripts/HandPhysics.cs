using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{

    public Transform target;
    private Rigidbody rb;

    private Collider[] handColliders;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableColliders()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }

    public void EnableDelay(float delay)
    {
        Invoke("EnableColliders", delay);
    }

    public void DisableColliders()
    {
        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pos
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
