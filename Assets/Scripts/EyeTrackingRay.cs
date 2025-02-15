using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.TeleopInterfaces;
using RosMessageTypes.Std;
using System.Diagnostics.CodeAnalysis;
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
    private bool mainEye;    
    [SerializeField]
    private string graspAssistServiceName;
    [SerializeField]
    private string headViewServiceName = "head_view";

    [SerializeField]
    private Camera cameraFacing;
    private LineRenderer lineRenderer;
    private bool prevButtonState;
    // Service stuff
    ROSConnection ros;

    // Start is called before the first frame update
    void Start()
    {
     lineRenderer = GetComponent<LineRenderer>();
     ros = ROSConnection.GetOrCreateInstance();
     ros.RegisterRosService<GraspTriggerRequest, GraspTriggerResponse>(graspAssistServiceName);
     ros.RegisterRosService<SetBoolRequest, SetBoolResponse>(headViewServiceName);
     if(visualizeLine)
     {
        SetupRay(); 
     }
     else
     {
        lineRenderer.enabled = false;
     }
    }

    void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth =  rayWidth;
        lineRenderer.startColor=UnityEngine.Color.red;
        lineRenderer.endColor=UnityEngine.Color.red;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,new Vector3(transform.position.x,transform.position.y,transform.position.z+rayDistance));
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3  rayCastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;
        if(Physics.Raycast(transform.position,rayCastDirection, out hit,Mathf.Infinity,layersToInclude))
        {
            if(visualizeLine)
            {
                lineRenderer.startColor=UnityEngine.Color.yellow;
                lineRenderer.endColor=UnityEngine.Color.yellow;
            }
            //If grip button is pressed and the user is looking at the image plane, activate grasping assistance
            if(hit.collider.gameObject == plane.gameObject)
            {
                Vector3 point  = hit.point;
                point[2] = point[2]-0.3f;
                cursorCircle.transform.position = point;
                cursorCircle.transform.LookAt(2*hit.point-cameraFacing.transform.position);

                if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.RHandTrigger)!= prevButtonState)
                {   
                    Debug.Log($"Grasp requsted in eye: {mainEye}");
                    prevButtonState = OVRInput.Get(OVRInput.RawButton.RHandTrigger);
                    GraspTriggerRequest graspReq = new GraspTriggerRequest();
                    double[] gazePoint = {hit.textureCoord.x, hit.textureCoord.y};
                    graspReq.gaze_point.data = gazePoint;
                    ros.SendServiceMessage<GraspTriggerResponse>(graspAssistServiceName,graspReq,GraspTriggerCallback);
                }
                else if(!OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.RHandTrigger) != prevButtonState)
                {
                    prevButtonState = OVRInput.Get(OVRInput.RawButton.RHandTrigger);
                    if (mainEye)
                    {  
                        SetBoolRequest headViewReq = new SetBoolRequest();
                        headViewReq.data = true;
                        ros.SendServiceMessage<SetBoolResponse>(headViewServiceName,headViewReq, HeadViewCallback);

                    }
                }
            }
            // If grip is held, and trigger is pressed trigger grasp based on sight
            // do a trigger state check in here
        }
    }

    private void GraspTriggerCallback(GraspTriggerResponse response)
    {
        // Debug.Log($"Response: {response.message}");
    }

    private void HeadViewCallback(SetBoolResponse response)
    {
        // Debug.Log($"Response: {response.message}");
    }
}
