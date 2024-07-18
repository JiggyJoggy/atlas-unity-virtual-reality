using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialTouchPosition;
    private Vector3 initialObjectPosition;
    private Vector3 pullbackDirection;
    private float pullbackDistance;
    private bool isDragging = false;

    public float slingshotForceMultiplier = 10f;  // Adjust this value to change the shooting force

    //private ARStartBNT gameManager;
    private GameObject currentAmmo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAmmo = this.gameObject;
        //gameManager = FindObjectOfType<ARStartBNT>();
    }

    void Update()
    {
        //if (transform.position.y < ARPlayPlaneArea.currentPlane.transform.position.y)
        //{
        //    gameManager.StartingGame();
        //}
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            isDragging = true;
                            initialTouchPosition = touch.position;
                            initialObjectPosition = transform.position;
                            rb.useGravity = false;
                            rb.velocity = Vector3.zero;
                            rb.angularVelocity = Vector3.zero;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 currentTouchPosition = touch.position;
                        Vector3 currentWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(currentTouchPosition.x, currentTouchPosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
                        pullbackDirection = (initialObjectPosition - currentWorldPosition).normalized;
                        pullbackDistance = Vector3.Distance(initialObjectPosition, currentWorldPosition);
                        transform.position = new Vector3(currentWorldPosition.x, currentWorldPosition.y, currentWorldPosition.z);
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        rb.useGravity = true;
                        rb.AddForce(pullbackDirection * pullbackDistance * slingshotForceMultiplier, ForceMode.Impulse);
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Destroy(other.gameObject);
            //gameManager.StartingGame();
        }
        if (other.tag == "PlaneTrigger")
        {
            Destroy(currentAmmo);
        }
    }
}