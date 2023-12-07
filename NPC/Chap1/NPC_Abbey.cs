using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class NPC_Abbey : BaseNPC, IPrompt // 既是NPC又具有提示（可交互）
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider npcCol; // NPC子物体中的Collider组件
    private GameObject prompt; // 图标提示
=======
public class NPC_Abbey : BaseNPC
{
>>>>>>> 90c4bc3 (1208)
    public Animator moveAnim; // 用来控制人物走动的动画控制器，挂在NPC2上
    public bool isMoved; // 是否开启带路
    private int talkCount = 0;
    public int TalkCount{get{return talkCount;}set{talkCount=value;}}

    protected override void Awake()
    {
        base.Awake();
        moveAnim = GetComponent<Animator>();
    }

<<<<<<< HEAD
    void Start()
    {
        // 设置NPC名字
        npcName = "Abbey";
        // 搜索动画组件和碰撞组件的引用
        // animController = transform.Find("Avatar").GetComponent<Animation>();
        npcCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
=======
    protected override void Start()
    {
        base.Start();
        // 设置NPC名字
        npcName = "Abbey";
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
                }
            }
        }
        if(isMoved==true && (Time.frameCount % 5 == 0))
        {
            // 如果主角和NPC2的距离超过10，NPC需要暂停带路
            if((GameManager.Instance.protagonist.transform.position-this.transform.position).magnitude > 10.0f)
            {
                moveAnim.speed = 0; // 冻结，即暂停
                animController.Play("idle");
            }
            else // 主角和NPC的距离在范围之内
            {
                moveAnim.speed = 1; // 正常播放
                if(animController.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                {
                    animController.Play("walk");
                }
            }
            // 如果NPC到达终点，要停下走路动画
            if(transform.position==new Vector3(3,0.05f,4)&&talkCount==1)
            {
<<<<<<< HEAD
                Debug.Log("Abbey到了");
=======
                // Debug.Log("Abbey到了");
>>>>>>> 90c4bc3 (1208)
                isMoved = false;
                animController.Play("idle");
                // 更新对话信息
                dialogInfo = GameManager.LoadByJson(npcName+"2");
                optionInfo = GameManager.LoadOption(npcName+"2_Options");
                dialogCount = dialogInfo.dialogs.Count; // 赋值对话的总数量
                // isTalked = false;
                currentWordIndex = -1; // 重置
                currentOptionIndex = -1; // 重置
                GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                GameManager.Instance.mCurrentScene.RegisterNPC(this); // 注册NPC
                GameManager.Instance.mCurrentScene.ShowPanelDialog(); // 显示对话面板
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
        if(!isMoved && !isTalked) // 若还在说话阶段
        {
            ShowPrompt();
            isInInteraction = false; // 刚进入时设置为还没交互的状态
            isInCollider = true; // 玩家进入碰撞体
        }
    }

<<<<<<< HEAD
    public void OnTriggerExit(Collider other)
    {
=======
    public override void OnTriggerExit(Collider other)
    {
        Debug.Log("exit"+talkCount);
>>>>>>> 90c4bc3 (1208)
        // Debug.Log(Time.time + "离开触发器的对象是：" + other.gameObject.name);
        HidePrompt();
        isInInteraction = false; // 视为交互结束
        currentOptionIndex = -1; currentWordIndex = -1; // 重置对话和选项索引
        isInCollider = false; // 玩家离开碰撞体
        GameManager.Instance.mCurrentScene.HidePanelDialog(); // 隐藏对话框
<<<<<<< HEAD

        // 播放带路动画，设置Trigger
        if(isTalked == true && isMoved == false)
        {
            if(talkCount==1)
            {
                moveAnim.SetTrigger("Move");
            }
=======
        if(transform.position==new Vector3(3,0.05f,4)&&talkCount==1){return;}

        // 播放带路动画，设置Trigger
        if(isTalked == true && talkCount==1 && isMoved == false)
        {
            moveAnim.SetTrigger("Move");
>>>>>>> 90c4bc3 (1208)
            animController.Play("walk");
            isMoved = true;
        }           
    }
}
