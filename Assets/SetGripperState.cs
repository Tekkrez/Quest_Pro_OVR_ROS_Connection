using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class EnableTracking : MonoBehaviour
{
    ROSConnection ros;
    readonly string serviceName = "set_gripper_state";
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
        //If trigger is held, activate gripper, else deactivate gripper
        if(OVRInput.Get(OVRInput.RawButton.LIndexTrigger) != prevButtonState)
        {
            prevButtonState = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
            SetGripperState(OVRInput.Get(OVRInput.RawButton.LIndexTrigger));
        }
    }

    private void SetGripperState(bool state)
    {
        SetBoolRequest triggerRequest = new TriggerRequest();
        ros.SendServiceMessage<TriggerResponse>(serviceName,triggerRequest,SetGripperStateCallback);
    }

    private void SetGripperStateCallback(TriggerResponse response)
    {

    }
}
