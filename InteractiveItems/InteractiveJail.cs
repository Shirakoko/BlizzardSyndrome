using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveJail : MonoBehaviour
{
    private bool isOpen; // 监狱门是否被开启
    public Collider itemCol; // 子物体中的Collider组件
    private Animator animator; // 开门的动画控制器
    private bool isInCollider; // 玩家是否处在碰撞体范围内

    void Start()
    {
        // 搜索碰撞体
        itemCol = GetComponent<Collider>(); 
        // 搜索动画控制器
        animator = GetComponent<Animator>();
        // 绑定事件
        GameManager.Instance.eventCenter.AddEventListener("OpenJail",OpenJail);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!isOpen)
        {
            // Usage中的第一个格子钥匙描边，通过EventCenter发信息
            GameManager.Instance.eventCenter.TriggerEvent("OutLine0On");
            isInCollider = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(!isOpen)
        {
            // Usage中的第一个格子钥匙取消描边，通过EventCenter发信息
            GameManager.Instance.eventCenter.TriggerEvent("OutLine0Off");
            isInCollider = false;
        }
    }

    private void OpenJail()
    {
        // 打开监狱门
        animator.SetTrigger("DoorOpen");
        isOpen = true;
    }
}
