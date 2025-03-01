//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.TeleopInterfaces
{
    [Serializable]
    public class ManipulatorWaypointsRequest : Message
    {
        public const string k_RosMessageName = "teleop_interfaces/ManipulatorWaypoints";
        public override string RosMessageName => k_RosMessageName;

        public Std.HeaderMsg header;
        public Geometry.PoseMsg[] poses;
        public bool[] gripper_state;

        public ManipulatorWaypointsRequest()
        {
            this.header = new Std.HeaderMsg();
            this.poses = new Geometry.PoseMsg[0];
            this.gripper_state = new bool[0];
        }

        public ManipulatorWaypointsRequest(Std.HeaderMsg header, Geometry.PoseMsg[] poses, bool[] gripper_state)
        {
            this.header = header;
            this.poses = poses;
            this.gripper_state = gripper_state;
        }

        public static ManipulatorWaypointsRequest Deserialize(MessageDeserializer deserializer) => new ManipulatorWaypointsRequest(deserializer);

        private ManipulatorWaypointsRequest(MessageDeserializer deserializer)
        {
            this.header = Std.HeaderMsg.Deserialize(deserializer);
            deserializer.Read(out this.poses, Geometry.PoseMsg.Deserialize, deserializer.ReadLength());
            deserializer.Read(out this.gripper_state, sizeof(bool), deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.header);
            serializer.WriteLength(this.poses);
            serializer.Write(this.poses);
            serializer.WriteLength(this.gripper_state);
            serializer.Write(this.gripper_state);
        }

        public override string ToString()
        {
            return "ManipulatorWaypointsRequest: " +
            "\nheader: " + header.ToString() +
            "\nposes: " + System.String.Join(", ", poses.ToList()) +
            "\ngripper_state: " + System.String.Join(", ", gripper_state.ToList());
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
