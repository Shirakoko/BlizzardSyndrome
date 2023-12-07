using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour 
{
    
    void Update()
    {
        if(Time.frameCount%5==0) // 每5帧调用一次
        {
            // 当前对象始终面向摄像机
            this.transform.LookAt(Camera.main.transform.position);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        LandMarkManager.Instance.ShowNextLandmark();
        this.gameObject.SetActive(false); // 失活
    }
}
