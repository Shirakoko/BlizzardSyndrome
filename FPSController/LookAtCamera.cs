using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        if(Time.frameCount%5==0) // 每5帧调用一次
        {
            // 当前对象始终面向摄像机
            this.transform.LookAt(Camera.main.transform.position);
            this.transform.Rotate(90,0,0);
        }
    }
}
