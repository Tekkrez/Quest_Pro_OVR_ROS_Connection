using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class EnableTracking : MonoBehaviour
{
    ROSConnection ros;
    readonly string serviceName = "toggle_tracking";
    bool prevButtonState = false;
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterRosService<SetBoolRequest, SetBoolResponse>(serviceName);
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        // if B is pressed, toggle tracking.
        // If tracking, stop. If stopped, start tracking
        if(OVRInput.Get(OVRInput.RawButton.B) != prevButtonState)
        {
            prevButtonState = OVRInput.Get(OVRInput.RawButton.B);
            
            if(OVRInput.Get(OVRInput.RawButton.B) == true)
            {
                ToggleTrackingRequest();
            }
        }
    }

    private void ToggleTrackingRequest()
    {
        SetBoolRequest triggerRequest = new SetBoolRequest(true);
        ros.SendServiceMessage<SetBoolResponse>(serviceName,triggerRequest,ToggleTrackingCallback);
    }

    private void ToggleTrackingCallback(SetBoolResponse response)
    {

    }
}
