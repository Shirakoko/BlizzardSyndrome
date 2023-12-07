using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家输入管理器
/// </summary>
public class InputManager
{

    private void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKey(key))
        {
            GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("Key",key);
        }
    }

    private void CheckKeyCodeDown(KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("KeyDown",key);
        }
    }

    private void CheckKeyCodeUp(KeyCode key)
    {
        if(Input.GetKeyUp(key))
        {
            GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("KeyUp",key);
        }
    }

    public void CheckInputUpdate()
    {
        // 检测按下
        CheckKeyCodeDown(KeyCode.L); // 锁定或解锁视角
        CheckKeyCodeDown(KeyCode.E); // 交互
        CheckKeyCodeDown(KeyCode.N); // 下一句
        // 检测按住
        CheckKeyCode(KeyCode.P);
        // 检测抬起
        CheckKeyCodeUp(KeyCode.P);
    }
}
