using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<MeshRenderer>().isVisible){Debug.Log("在视野内");}
        else{Debug.Log("不在视野内");}
    }
}
