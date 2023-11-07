using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLamp : BaseInteractive
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider itemCol; // 子物体中的Collider组件
    private GameObject prompt; // 图标提示
    private GameObject shaftGo; // 路灯灯光
    private bool isInCollider; // 玩家是否处在碰撞体范围内
    // private bool isInteracted; // 是否已经交互过

    void Start()
    {
        // 搜索碰撞体
        itemCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
        // 搜索路灯灯光
        shaftGo = transform.Find("WallLamp").Find("Shaft").gameObject;
        // 隐藏提示图标
        HidePrompt();
        // 隐藏灯光
        shaftGo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) // 如果玩家按下了E
        {
            if(isInCollider && !isInteracted)
            {
                if(isInInteraction==false) // 当前NPC没处于交互状态，表示可以交互
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互状态
                    isInInteraction = true;
                    // 执行触发后的操作
                    shaftGo.SetActive(true);
                    isInteracted = true;
                }
            }     
        }
    }

    protected override void ShowPrompt()
    {
        prompt.SetActive(true);
    }

    protected override void HidePrompt()
    {
        prompt.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        ShowPrompt();
        isInInteraction = false; // 刚进入时设置为还没交互的状态
        isInCollider = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        HidePrompt();
        // isInInteraction = false; // 视为交互结束
        isInCollider = false;
    }
}
