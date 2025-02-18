//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.TeleopInterfaces
{
    [Serializable]
    public class GraspReqResponse : Message
    {
        public const string k_RosMessageName = "teleop_interfaces/GraspReq";
        public override string RosMessageName => k_RosMessageName;

        public Geometry.PoseMsg[] grasp_poses;
        public double[] grasp_scores;
        public Geometry.PointMsg[] contact_points;
        public double[] gripper_openings;
        public Sensor.ImageMsg grasp_visualization;
        public bool success;

        public GraspReqResponse()
        {
            this.grasp_poses = new Geometry.PoseMsg[0];
            this.grasp_scores = new double[0];
            this.contact_points = new Geometry.PointMsg[0];
            this.gripper_openings = new double[0];
            this.grasp_visualization = new Sensor.ImageMsg();
            this.success = false;
        }

        public GraspReqResponse(Geometry.PoseMsg[] grasp_poses, double[] grasp_scores, Geometry.PointMsg[] contact_points, double[] gripper_openings, Sensor.ImageMsg grasp_visualization, bool success)
        {
            this.grasp_poses = grasp_poses;
            this.grasp_scores = grasp_scores;
            this.contact_points = contact_points;
            this.gripper_openings = gripper_openings;
            this.grasp_visualization = grasp_visualization;
            this.success = success;
        }

        public static GraspReqResponse Deserialize(MessageDeserializer deserializer) => new GraspReqResponse(deserializer);

        private GraspReqResponse(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.grasp_poses, Geometry.PoseMsg.Deserialize, deserializer.ReadLength());
            deserializer.Read(out this.grasp_scores, sizeof(double), deserializer.ReadLength());
            deserializer.Read(out this.contact_points, Geometry.PointMsg.Deserialize, deserializer.ReadLength());
            deserializer.Read(out this.gripper_openings, sizeof(double), deserializer.ReadLength());
            this.grasp_visualization = Sensor.ImageMsg.Deserialize(deserializer);
            deserializer.Read(out this.success);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.grasp_poses);
            serializer.Write(this.grasp_poses);
            serializer.WriteLength(this.grasp_scores);
            serializer.Write(this.grasp_scores);
            serializer.WriteLength(this.contact_points);
            serializer.Write(this.contact_points);
            serializer.WriteLength(this.gripper_openings);
            serializer.Write(this.gripper_openings);
            serializer.Write(this.grasp_visualization);
            serializer.Write(this.success);
        }

        public override string ToString()
        {
            return "GraspReqResponse: " +
            "\ngrasp_poses: " + System.String.Join(", ", grasp_poses.ToList()) +
            "\ngrasp_scores: " + System.String.Join(", ", grasp_scores.ToList()) +
            "\ncontact_points: " + System.String.Join(", ", contact_points.ToList()) +
            "\ngripper_openings: " + System.String.Join(", ", gripper_openings.ToList()) +
            "\ngrasp_visualization: " + grasp_visualization.ToString() +
            "\nsuccess: " + success.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}
