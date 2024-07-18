using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlayPlaneArea : MonoBehaviour
{
    // UI/UX
    public TextMeshProUGUI text;
    public Canvas startCanvas;
    public Canvas bottomText;

    // AR
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    // Plane
    public static ARPlane currentPlane = null;
    public GameObject fakePlane;

    // Target
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Tap an area to play!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentPlane && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARRaycastHit hit = hits[0];
                    currentPlane = hit.trackable as ARPlane;

                    planeManager.enabled = false;

                    startCanvas.enabled = true;
                    bottomText.enabled = false;

                    foreach (ARPlane plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    Vector3 currentPlanePos = currentPlane.transform.position;
                    List<Vector3> boundaryPoints = new List<Vector3>();

                    foreach (Vector2 point in currentPlane.boundary)
                    {
                        boundaryPoints.Add(new Vector3(point.x, currentPlanePos.y, point.y));
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        Vector3 randomPosition = GetRandomPointWithinPlane(boundaryPoints) + currentPlanePos;
                        Instantiate(target, randomPosition, Quaternion.identity);
                    }
                    //Instantiate(fakePlane, currentPlanePos, Quaternion.identity); // FIX THIS PLEASEEE
                }
            }
        }
    }

    private Vector3 GetRandomPointWithinPlane(List<Vector3> boundaryPoints)
    {
        if (boundaryPoints.Count < 3) return boundaryPoints[0];

        // Select three random points from the boundary
        Vector3 p1 = boundaryPoints[Random.Range(0, boundaryPoints.Count)];
        Vector3 p2 = boundaryPoints[Random.Range(0, boundaryPoints.Count)];
        Vector3 p3 = boundaryPoints[Random.Range(0, boundaryPoints.Count)];

        // Use Barycentric coordinates to generate a random point within the triangle
        float a = Random.value;
        float b = Random.value;
        if (a + b > 1)
        {
            a = 1 - a;
            b = 1 - b;
        }
        float c = 1 - a - b;

        return a * p1 + b * p2 + c * p3;
    }
}