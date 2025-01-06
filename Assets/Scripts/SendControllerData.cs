using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;

public class SendControllerData : MonoBehaviour
{
    //Use camera to represent 
    public Camera hmdCamera;
    //ROS related variables
    readonly string topicName = "vr_pose";
    ROSConnection ros;
    // Start is called before the first frame update
    void Start()
    {
        //Ros Connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseArrayMsg>(topicName, 10);
        //Try to send control data at 50 hz
        StartCoroutine(PublishData(0.01f));
    }

    IEnumerator PublishData(float countTime = 0.01f)
    {
        while (true)
        {
            Vector3 leftPostitionVal;
            Quaternion leftRotationVal;
            Vector3 rightPostitionVal;
            Quaternion rightRotationVal;
            if(OVRInput.GetControllerPositionTracked(OVRInput.Controller.RTouch) && OVRInput.GetControllerOrientationTracked(OVRInput.Controller.LTouch))
            {
                //Read values from controllers and scene
                leftPostitionVal = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                leftRotationVal = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
                rightPostitionVal = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                rightRotationVal = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

            }
            else
            {
                leftPostitionVal = new Vector3(3,3,3);
                leftRotationVal = new Quaternion();
                rightPostitionVal = new Vector3(3,3,3);
                rightRotationVal = new Quaternion();
                
            }
            var XRPose = new PoseArrayMsg();

            Vector3 hmdPositionVal = hmdCamera.transform.position;
            Quaternion hmdRotationVal = hmdCamera.transform.rotation;
            //Assign it to message and change frame convention from unity to ROS
            PoseMsg hmdPose = new PoseMsg(hmdPositionVal.To<FLU>(), hmdRotationVal.To<FLU>());
            PoseMsg rightPose = new PoseMsg(rightPostitionVal.To<FLU>(), rightRotationVal.To<FLU>());
            PoseMsg leftPose = new PoseMsg(leftPostitionVal.To<FLU>(), leftRotationVal.To<FLU>());
            PoseMsg[] poseArray = {hmdPose,rightPose,leftPose};
            XRPose.poses = poseArray;
            //Send message
            ros.Publish(topicName, XRPose);
            yield return new WaitForSeconds(countTime);
        }
    }

    //Not expected to be called
    private void OnDisable(){
        StopCoroutine(PublishData());
    }
}
