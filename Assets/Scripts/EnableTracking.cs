using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class EnableTracking : MonoBehaviour
{

    ROSConnection ros;
    readonly string serviceName = "toggle_tracking";
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterRosService<TriggerRequest, TriggerResponse>(serviceName);
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        // if A is pressed, toggle tracking.
        // If tracking, stop. If stopped, start tracking
        if(OVRInput.GetDown(OVRInput.Button.One,OVRInput.Controller.RTouch))
        {
            ToggleTrackingRequest();
        }

    }

    private void ToggleTrackingRequest()
    {
        TriggerRequest triggerRequest = new TriggerRequest();
        ros.SendServiceMessage<TriggerResponse>(serviceName,triggerRequest,ToggleTrackingCallback);
    }

    private void ToggleTrackingCallback(TriggerResponse response)
    {

    }
}
