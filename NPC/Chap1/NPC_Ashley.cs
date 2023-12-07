using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class NPC_Ashley : BaseNPC, IPrompt // 既是NPC又具有提示（可交互）
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider npcCol; // NPC子物体中的Collider组件
    private GameObject prompt; // 图标提示

    void Start()
    {
        // 设置NPC名字
        npcName = "Ashley";
        // 搜索动画组件和碰撞组件的引用
        // animController = transform.Find("Avatar").GetComponent<Animation>();
        npcCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
=======
public class NPC_Ashley : BaseNPC
{
    protected override void Start()
    {
        base.Start();
        // 设置NPC名字
        npcName = "Ashley";
>>>>>>> 90c4bc3 (1208)
        // 隐藏提示图标
        HidePrompt();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) // 如果玩家按下了E
        {
            if(isInCollider) // 玩家在NPC碰撞体内才可交互
            {
                if(isInInteraction==false && isTalked==false) // 当前NPC没处于交互状态，表示可以交互
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互状态
                    isInInteraction = true;
                    // 执行触发后的操作，比如锁定视角、改变动作、弹出对话框等
                    Debug.Log("NPC被按E触发");
                    GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.mCurrentScene.RegisterNPC(this); // 注册NPC
                    GameManager.Instance.mCurrentScene.ShowPanelDialog(); // 显示对话面板
                    // 设置NPC动画为talk
                    animController.Play("talk");
                    // 新增PanelBag中的子任务2
                    PanelBag.Instance.AddNewMission();
                }
            }
        }
    }

<<<<<<< HEAD
    public void ShowPrompt()
    {
        prompt.SetActive(true);
    }
    public void HidePrompt()
    {
        prompt.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
=======
    public override void OnTriggerEnter(Collider other)
>>>>>>> 90c4bc3 (1208)
    {
        // Debug.Log(Time.time + ":进入该触发器的对象是：" + other.gameObject.name);
        ShowPrompt();
        isInInteraction = false; // 刚进入时设置为还没交互的状态
        isInCollider = true; // 玩家进入碰撞体
    }

<<<<<<< HEAD
    public void OnTriggerExit(Collider other)
=======
    public override void OnTriggerExit(Collider other)
>>>>>>> 90c4bc3 (1208)
    {
        // Debug.Log(Time.time + "离开触发器的对象是：" + other.gameObject.name);
        HidePrompt();
        isInInteraction = false; // 视为交互结束
        currentOptionIndex = -1; currentWordIndex = -1; // 重置对话和选项索引
        isInCollider = false; // 玩家离开碰撞体
        GameManager.Instance.mCurrentScene.HidePanelDialog(); // 隐藏对话框
    }
}
