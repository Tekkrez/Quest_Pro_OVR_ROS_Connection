//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.TeleopInterfaces
{
    [Serializable]
    public class ExecuteGraspRequest : Message
    {
        public const string k_RosMessageName = "teleop_interfaces/ExecuteGrasp";
        public override string RosMessageName => k_RosMessageName;

        //  Custom service to request grasp poses from a given point cloud
        public Geometry.PoseMsg grasp_pose;
        public double grasp_score;
        public Geometry.PointMsg contact_point;
        public double gripper_opening;

        public ExecuteGraspRequest()
        {
            this.grasp_pose = new Geometry.PoseMsg();
            this.grasp_score = 0.0;
            this.contact_point = new Geometry.PointMsg();
            this.gripper_opening = 0.0;
        }

        public ExecuteGraspRequest(Geometry.PoseMsg grasp_pose, double grasp_score, Geometry.PointMsg contact_point, double gripper_opening)
        {
            this.grasp_pose = grasp_pose;
            this.grasp_score = grasp_score;
            this.contact_point = contact_point;
            this.gripper_opening = gripper_opening;
        }

        public static ExecuteGraspRequest Deserialize(MessageDeserializer deserializer) => new ExecuteGraspRequest(deserializer);

        private ExecuteGraspRequest(MessageDeserializer deserializer)
        {
            this.grasp_pose = Geometry.PoseMsg.Deserialize(deserializer);
            deserializer.Read(out this.grasp_score);
            this.contact_point = Geometry.PointMsg.Deserialize(deserializer);
            deserializer.Read(out this.gripper_opening);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.grasp_pose);
            serializer.Write(this.grasp_score);
            serializer.Write(this.contact_point);
            serializer.Write(this.gripper_opening);
        }

        public override string ToString()
        {
            return "ExecuteGraspRequest: " +
            "\ngrasp_pose: " + grasp_pose.ToString() +
            "\ngrasp_score: " + grasp_score.ToString() +
            "\ncontact_point: " + contact_point.ToString() +
            "\ngripper_opening: " + gripper_opening.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
