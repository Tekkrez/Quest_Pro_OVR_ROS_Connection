using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class SetGripperState : MonoBehaviour
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
        if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) != prevButtonState)
        {
            prevButtonState = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
            SetGripperStateRequest(OVRInput.Get(OVRInput.RawButton.RIndexTrigger));
        }
    }

    private void SetGripperStateRequest(bool state)
    {
        SetBoolRequest boolRequest = new SetBoolRequest(state);
        ros.SendServiceMessage<SetBoolResponse>(serviceName,boolRequest,SetGripperStateCallback);
    }

    private void SetGripperStateCallback(SetBoolResponse response)
    {

    }
}
