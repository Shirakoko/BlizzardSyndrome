using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Alex : BaseNPC, IPrompt // 既是NPC又具有提示（可交互）
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中
    public Collider npcCol; // NPC子物体中的Collider组件
    private GameObject prompt; // 图标提示
    public Animator moveAnim; // 用来控制人物走动的动画控制器，挂在NPC2上
    public bool isMoved; // 是否开启带路
    public GameObject keyGo; // 钥匙游戏物体
    private Ray ray; // 鼠标射线
    private RaycastHit hit; // 鼠标射线击中结构体
    private GameObject lastHit; // 上一次击中的物体
    private int talkCount = 0;
    public int TalkCount{get{return talkCount;}set{talkCount=value;}}
    private bool isOpen;
    public bool IsOpen{get{return isOpen;}set{isOpen=value;}}

    protected override void Awake()
    {
        base.Awake();
        moveAnim = GetComponent<Animator>();
    }

    void Start()
    {
        // 设置NPC名字
        npcName = "Alex";
        // 搜索动画组件和碰撞组件的引用
        // animController = transform.Find("Avatar").GetComponent<Animation>();
        npcCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
        // 隐藏提示图标
        HidePrompt();
        // 绑定事件
        GameManager.Instance.eventCenter.AddEventListener("OpenJail",()=>{isOpen = true;});
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) // 如果玩家按下了E
        {
            if(isInCollider&&talkCount==0) // 玩家在NPC碰撞体内才可交互
            {
                if(isInInteraction==false && isTalked==false && Preconditions()) // 当前NPC没处于交互状态，并且达成前置条件时表示可以交互
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互状态
                    isInInteraction = true;
                    // 执行触发后的操作，比如锁定视角、改变动作、弹出对话框等
                    GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.mCurrentScene.RegisterNPC(this); // 注册NPC
                    GameManager.Instance.mCurrentScene.ShowPanelDialog(); // 显示对话面板
                    // 设置NPC动画为talk
                    animController.Play("talk");
                    // 新增PanelBag中的子任务3
                    PanelBag.Instance.AddNewMission();
                    GetComponent<PublicNPC>().ifTalked = true;
                    //给PublicNPC传输ifTalked的值
                }
            }
            else if(isInCollider&&talkCount==2) // 已经偷到钥匙了
            {
                if(isInInteraction==false) // 当前NPC没处于交互状态，表示可以交互
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互状态
                    isInInteraction = true;
                    // 执行触发后的操作，比如锁定视角、改变动作、弹出对话框等
                    Debug.Log("偷盗钥匙后和Alex交互");
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
                    animController.Play("hit");
                }
            }
            // 如果NPC到达终点，要停下走路动画
            if(transform.position==new Vector3(-44,6.1f,-39)&&talkCount==3)
            {
                isMoved = false;
                animController.Play("idle");
                // 更新对话信息
                dialogInfo = GameManager.LoadByJson(npcName+"3");
                optionInfo = GameManager.LoadOption(npcName+"3_Options");
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
        if(Input.GetMouseButtonDown(1)) // 如果玩家按下了鼠标右键
        {
            if(!isInInteraction)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 创建射线
                if (Physics.Raycast(ray, out hit, 10, 1<<LayerMask.NameToLayer("Concealment")))
                {
                    lastHit = hit.collider.gameObject; // 得到被射线检测到的物体（钥匙）
                    if(lastHit.name == "Key") // 再次确认被射线检测到的是钥匙
                    {
                        Debug.Log("捡起钥匙"); talkCount++;
                        keyGo.SetActive(false); // 钥匙消失
                        // 背包红点+背包出现第一张图钥匙，可用事件中心处理
                        GameManager.Instance.eventCenter.TriggerEvent("FetchKey");
                    }
                }
            }
        }
    }

    public void ShowPrompt()
    {
        prompt.SetActive(true);
    }
    public void HidePrompt()
    {
        prompt.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        // print(talkCount);
        if(talkCount!=1 && talkCount!=3 && !isMoved) // 若还在说话阶段
        {
            ShowPrompt();
            isInInteraction = false; // 刚进入时设置为还没交互的状态
            isInCollider = true; // 玩家进入碰撞体
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Debug.Log(Time.time + "离开触发器的对象是：" + other.gameObject.name);
        HidePrompt();
        isInInteraction = false; // 视为交互结束
        currentOptionIndex = -1; currentWordIndex = -1; // 重置对话和选项索引
        isInCollider = false; // 玩家离开碰撞体
        GameManager.Instance.mCurrentScene.HidePanelDialog(); // 隐藏对话框
        animController.Play("hit"); // 设置疯子发疯的动作

        // 播放从监狱逃出来的动画
        if(isOpen==true && isMoved == false  && talkCount<4)
        {
            moveAnim.SetTrigger("Move");
            animController.Play("hit");
            isMoved = true;
        }
        else if(talkCount==4)
        {
            // 开启15秒之后消失的协程
            StartCoroutine(DisappearAfterTime(15));
        }        
    }
}
