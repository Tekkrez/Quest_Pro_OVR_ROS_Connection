using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class EyeTrackingRay : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 1.0f;
    [SerializeField]
    private float rayWidth = 0.01f;
    [SerializeField]
    private LayerMask layersToInclude;
    [SerializeField]
    private Transform plane;
    [SerializeField]
    private GameObject cursorCircle;
    [SerializeField]
    private bool visualizeLine = false;
    [SerializeField]
    private Camera cameraFacing;
    private LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
     lineRenderer = GetComponent<LineRenderer>();
     if(visualizeLine)
     {
        SetupRay(); 
     }
    }

    void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth =  rayWidth;
        lineRenderer.startColor=Color.red;
        lineRenderer.endColor=Color.red;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,new Vector3(transform.position.x,transform.position.y,transform.position.z+rayDistance));
    }

    void FixedUpdate()
    {
    RaycastHit hit;
    Vector3  rayCastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;
    if(Physics.Raycast(transform.position,rayCastDirection, out hit,Mathf.Infinity,layersToInclude))
    {
        Debug.Log("here");
        lineRenderer.startColor=Color.yellow;
        lineRenderer.endColor=Color.yellow;
        if (hit.collider.gameObject == plane.gameObject)
        {           
            cursorCircle.transform.position = hit.point;
            cursorCircle.transform.LookAt(cameraFacing.transform.position);
        }
        else
        {
        lineRenderer.startColor=Color.red;
        lineRenderer.endColor=Color.red;
        // Make circle invisible
        }
    }
    }
}
