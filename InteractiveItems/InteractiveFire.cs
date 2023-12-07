using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class InteractiveFire: BaseInteractive
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider itemCol; // 子物体中的Collider组件
    private GameObject prompt; // 图标提示
    private GameObject fireGo; // 火光
    private bool isInCollider; // 玩家是否处在碰撞体范围内
=======
using UnityEngine.Events;

public class InteractiveFire: BaseInteractive
{
    // private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider itemCol; // 子物体中的Collider组件
    private GameObject prompt; // 图标提示
    private GameObject fireGo; // 火光
    // private bool isInCollider; // 玩家是否处在碰撞体范围内
>>>>>>> 90c4bc3 (1208)
    // private bool isInteracted; // 是否已经交互过

    void Start()
    {
<<<<<<< HEAD
=======
        // 添加触发后执行的操作
        subAction += (()=>{fireGo.SetActive(true);});
>>>>>>> 90c4bc3 (1208)
        // 搜索碰撞体
        itemCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
        // 搜索路灯灯光
        fireGo = transform.Find("Firewood").Find("Fire").gameObject;
        // 隐藏提示图标
        HidePrompt();
        // 隐藏火光
        fireGo.SetActive(false);
<<<<<<< HEAD
=======
        // 添加输入检测（按键）的监听事件
        GameManager.Instance.eventCenter.AddEventListener<KeyCode>("KeyDown",Interact);
        # region(舍弃)
        // // GameManager.Instance.eventCenter.AddEventListener<KeyCode>("KeyDown",(KeyCode key)=>{
        // //     if(key == KeyCode.E) // 如果玩家按下了E
        // //     {
        // //         if(isInCollider && !isInteracted)
        // //         {
        // //             if(isInInteraction==false) // 当前NPC没处于交互状态，表示可以交互
        // //             {
        // //                 // 隐藏提示
        // //                 HidePrompt();
        // //                 // 设置为处于交互状态
        // //                 isInInteraction = true;
        // //                 // 执行触发后的操作
        // //                 fireGo.SetActive(true);
        // //                 isInteracted = true;
        // //             }
        // //         }     
        // //     }
        // // });
        # endregion
        # region(舍弃)
        // // GameManager.Instance.AddUpdateFunction(()=>{
        // //     if(Input.GetKeyDown(KeyCode.E)) // 如果玩家按下了E
        // //     {
        // //         if(isInCollider && !isInteracted)
        // //         {
        // //             if(isInInteraction==false) // 当前NPC没处于交互状态，表示可以交互
        // //             {
        // //                 // 隐藏提示
        // //                 HidePrompt();
        // //                 // 设置为处于交互状态
        // //                 isInInteraction = true;
        // //                 // 执行触发后的操作
        // //                 fireGo.SetActive(true);
        // //                 isInteracted = true;
        // //             }
        // //         }     
        // //     }
        // // });
        # endregion
>>>>>>> 90c4bc3 (1208)
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
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
                    fireGo.SetActive(true);
                    isInteracted = true;
                }
            }     
        }
=======
        
>>>>>>> 90c4bc3 (1208)
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
<<<<<<< HEAD
        ShowPrompt();
        isInInteraction = false; // 刚进入时设置为还没交互的状态
        isInCollider = true;
=======
        if(isInteracted==false)
        {
            ShowPrompt();
            isInInteraction = false; // 刚进入时设置为还没交互的状态
            isInCollider = true;
        }
>>>>>>> 90c4bc3 (1208)
    }

    protected override void OnTriggerExit(Collider other)
    {
        HidePrompt();
        // isInInteraction = false; // 视为交互结束
        isInCollider = false;
    }
}
