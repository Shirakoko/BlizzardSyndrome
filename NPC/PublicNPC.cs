using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicNPC : MonoBehaviour
{
    //��ÿ��NPC��GameObject�������һ���˽ű�
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
