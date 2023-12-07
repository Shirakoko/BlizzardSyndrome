using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.Events;
>>>>>>> 90c4bc3 (1208)

public abstract class BaseInteractive : MonoBehaviour
{
    protected bool isInteracted; // 是否已经交互过
<<<<<<< HEAD
=======
    protected bool isInCollider; // 是否在碰撞体内
    protected bool isInInteraction; // 是否处在交互中
>>>>>>> 90c4bc3 (1208)
    protected abstract void ShowPrompt();
    protected abstract void HidePrompt();
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
<<<<<<< HEAD
=======

    protected UnityAction subAction; // 触发之后的操作

    /// <summary>
    /// 按下E后执行的操作，可以被子类重写
    /// </summary>
    /// <param name="key">触发"KeyDown"事件后传入的键</param>
    protected virtual void Interact(KeyCode key)
    {
        if(key == KeyCode.E)
        {
            if(isInCollider && !isInteracted)
            {
                if(isInInteraction==false) // 当前未处于交互状态
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互状态
                    isInInteraction = true;
                    // 设置为已经交互过了
                    isInteracted = true;
                    // 执行触发后的操作
                    if(subAction!=null){ subAction(); } 
                }
            }
        }
    }
>>>>>>> 90c4bc3 (1208)
}
