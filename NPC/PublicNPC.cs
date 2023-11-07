using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicNPC : MonoBehaviour
{
    //在每个NPC的GameObject额外挂载一个此脚本
    public bool ifTalked;
    void Awake()
    {
        ifTalked = false;
    }

    public bool GetState()
    {
        return ifTalked;
    }
}
