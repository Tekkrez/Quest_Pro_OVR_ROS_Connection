using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosImage = RosMessageTypes.Sensor.CompressedImageMsg;

public class RosSubscriber : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private string vrImageTopic;
    private Texture2D texture2D;
    private byte[] image;
    private bool isMessageReceived = false;
    private bool doOnce = false;
    ROSConnection ros;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.material = new Material(Shader.Find("Standard"));
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<RosImage>(vrImageTopic,ReceiveMessage);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMessageReceived){
            ProcessMessage();
        }
    }

    void ReceiveMessage(RosImage imageData)
    {
        if(!doOnce)
        {   
            //Not sure why I put 2,2, doesn't seem to correspond to size of the plane
            texture2D = new Texture2D(2,2);
            doOnce = !doOnce;
        }
        image =imageData.data;
        isMessageReceived = true;
    }

    void ProcessMessage()
    {
        texture2D.LoadImage(image);
        texture2D.Apply();
        meshRenderer.material.SetTexture("_MainTex",texture2D);
        isMessageReceived = false;
    }
}
