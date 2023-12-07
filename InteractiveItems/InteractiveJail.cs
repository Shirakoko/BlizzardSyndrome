using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class InteractiveJail : MonoBehaviour
=======
public class InteractiveJail : BaseInteractive
>>>>>>> 90c4bc3 (1208)
{
    private bool isOpen; // 监狱门是否被开启
    public Collider itemCol; // 子物体中的Collider组件
    private Animator animator; // 开门的动画控制器
<<<<<<< HEAD
    private bool isInCollider; // 玩家是否处在碰撞体范围内
=======
    // private bool isInCollider; // 玩家是否处在碰撞体范围内
>>>>>>> 90c4bc3 (1208)

    void Start()
    {
        // 搜索碰撞体
        itemCol = GetComponent<Collider>(); 
        // 搜索动画控制器
        animator = GetComponent<Animator>();
        // 绑定事件
        GameManager.Instance.eventCenter.AddEventListener("OpenJail",OpenJail);
    }

<<<<<<< HEAD
    public void OnTriggerEnter(Collider other)
=======
    protected override void OnTriggerEnter(Collider other)
>>>>>>> 90c4bc3 (1208)
    {
        if(!isOpen)
        {
            // Usage中的第一个格子钥匙描边，通过EventCenter发信息
            GameManager.Instance.eventCenter.TriggerEvent("OutLine0On");
<<<<<<< HEAD
            isInCollider = true;
        }
    }

    public void OnTriggerExit(Collider other)
=======
            // isInCollider = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
>>>>>>> 90c4bc3 (1208)
    {
        if(!isOpen)
        {
            // Usage中的第一个格子钥匙取消描边，通过EventCenter发信息
            GameManager.Instance.eventCenter.TriggerEvent("OutLine0Off");
<<<<<<< HEAD
            isInCollider = false;
=======
            // isInCollider = false;
>>>>>>> 90c4bc3 (1208)
        }
    }

    private void OpenJail()
    {
        // 打开监狱门
        animator.SetTrigger("DoorOpen");
        isOpen = true;
    }
<<<<<<< HEAD
=======

    protected override void ShowPrompt()
    {

    }

    protected override void HidePrompt()
    {
        
    }
>>>>>>> 90c4bc3 (1208)
}
