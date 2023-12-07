using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using DG.Tweening;

<<<<<<< HEAD
public class NPC_Tom : BaseNPC, IPrompt // 既是NPC又具有提示（可交互）
{
    private bool isInInteraction; // NPC是否处于和玩家交互的状态中，0表示没有，1表示对话，2表示打开编织袋
    public Collider npcCol; // NPC子物体中的Collider组件
    private GameObject prompt; // 图标提示
=======
public class NPC_Tom : BaseNPC
{
>>>>>>> 90c4bc3 (1208)
    private GameObject fireGo; // 火焰游戏物体
    public Animator rootAnimator; // 用来控制火焰和人物的动画控制器，挂在NPC7上
    public List<NavNPC> navNPCs; // 围观的吃瓜群众
    private bool isFired = false; // 是否被烧过
    private bool isPeterTalked = false;
    // public bool IsPeterTalked{get{return isPeterTalked;} set{isPeterTalked=value;}} // 是否和Peter交谈过（看房回来之后）
    private int openBagCount = 0; // 0表示编织袋未出现，1表示编织袋出现但未打开，2表示编织袋已打开
    private GameObject bagGo; // 编织袋游戏物体
    private GameObject pieceGo; // 笔记碎片游戏物体
    public GameObject PieceGo{get{return pieceGo;}}
    private Ray ray; // 鼠标射线
    private RaycastHit hit; // 鼠标射线击中结构体
    private GameObject lastHit; // 上一次击中的物体

    // private NPC_Tom instance;
    // public static NPC_Tom Instance{get; private set;} // 这里还是不用静态单例，因为占内存，这个Tom并不是一直有用的

    protected override void Awake()
    {
        base.Awake();
        // Instance = this;
        rootAnimator = GetComponent<Animator>();
        bagGo = transform.Find("Bag").gameObject; bagGo.SetActive(false);
        pieceGo = transform.Find("Piece").gameObject; pieceGo.SetActive(false);
    }
<<<<<<< HEAD
    void Start()
    {
=======
    protected override void Start()
    {
        base.Start();
>>>>>>> 90c4bc3 (1208)
        // 给方法SetPeterTalked绑定监听事件名PeterTalked
        GameManager.Instance.eventCenter.AddEventListener("PeterTalked",SetPeterTalked);
        // 给方法SetPieceGoTalked绑定监听事件名PieceGoInactive
        GameManager.Instance.eventCenter.AddEventListener("PieceGoInactive",SetPieceGoInactive);
        // 搜索火焰游戏物体并隐藏
        fireGo = transform.Find("Fire").gameObject; fireGo.gameObject.SetActive(false);
        // 设置NPC名字
        npcName = "Tom";
<<<<<<< HEAD
        // 搜索动画组件和碰撞组件的引用
        npcCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
=======
>>>>>>> 90c4bc3 (1208)
        // 隐藏提示图标
        HidePrompt();
    }

    void OnDisable() 
    {
        // 移除事件
        GameManager.Instance.eventCenter.RemoveEventListener("PeterTalked",SetPeterTalked);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) // 如果玩家按下了E
        {
            if(isInCollider && !isFired) // 玩家在NPC碰撞体内才可交互
            {
                if(isInInteraction==false) // 当前NPC没处于交互状态，表示可以交互
                {
                    // 隐藏提示
                    HidePrompt();
                    // 设置为处于交互（对话）状态
                    isInInteraction = true;
                    // 执行触发后的操作，比如锁定视角、改变动作、弹出对话框等
                    // Debug.Log("NPC被按E触发");
                    GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                    GameManager.Instance.mCurrentScene.RegisterNPC(this); // 注册NPC
                    GameManager.Instance.mCurrentScene.ShowPanelDialog(); // 显示对话面板
                    // 设置NPC动画为talk
                    // animController.Play("talk");
                }
            }
            // else if(isInCollider && isFired)
            // {
            //     if(isInInteraction==false)
            //     {
            //         // 执行触发后的操作，即把编织袋打开
            //         if(openBagCount==1)
            //         {
            //             Debug.Log("第一次按下E：打开编织袋，出现笔记碎片"); openBagCount++;
            //         }
            //         else if(openBagCount==2)
            //         {
            //             Debug.Log("第二次按下：把笔记碎片收集入背包"); HidePrompt(); isInInteraction = true;
            //         }
            //     }
            // }
        }
        if(Input.GetMouseButtonDown(1)) // 如果玩家按下了鼠标右键
        {
            Debug.Log("点击鼠标右键");
            if(isInCollider && isFired && !isInInteraction)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 创建射线
                if (Physics.Raycast(ray, out hit, 10, 1<<LayerMask.NameToLayer("Concealment")))
                {
                    lastHit = hit.collider.gameObject; // 得到被射线检测到的物体（编织袋）
                    if(openBagCount==1 && lastHit.name == "Bag") // 再次确认被射线检测到的是编织袋
                    {
                        // Debug.Log("第一次按下：打开编织袋，出现笔记碎片"); openBagCount++;
                        bagGo.SetActive(false); // 编织袋消失
                        pieceGo.SetActive(true); // 笔记碎片出现
                        openBagCount++;
                    }
                    else if(openBagCount==2)
                    {
                        // Debug.Log("第二次按下：把笔记碎片收集入背包"); openBagCount++; HidePrompt(); isInInteraction = true;
                        GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                        GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = true; // 锁定视角
                        // 获得笔记
                        (GameManager.Instance.mCurrentScene as VillageScene).panelMain.ShowNotePrompt();
                        // 笔记污点清除
                        PanelBag.Instance.ClearDiary(0);
                        // 新增PanelBag中的子任务
                        PanelBag.Instance.AddNewMission();
                    }
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
        if(isTalked==false && isFired==false) // 没被对话过且没被烧过才能对话
        {
            ShowPrompt();
            isInInteraction = false; // 刚进入时设置为还没交互的状态
            isInCollider = true; // 玩家进入碰撞体
        }
        else if(isTalked && isFired && isPeterTalked) // 已经对话过且被烧过才能找到编织袋
        {
            // 由于人躺倒了，设置提示的位置并出现
            // prompt.transform.localPosition = new Vector3(-1.7f,0.7f,0); ShowPrompt();
            isInInteraction = false; // 刚进入时设置为还没交互的状态
            isInCollider = true; // 玩家进入碰撞体
            if(openBagCount==0) // 死掉之后且编织袋未出现
            {
                Debug.Log("进入碰撞体：出现编织袋");
                bagGo.SetActive(true);
                openBagCount++;
                // 新增PanelBag中的子任务
                PanelBag.Instance.AddNewMission();    
            }
            else if(openBagCount==1) // 编织袋出现但未打开
            {
                Debug.Log("进入碰撞体：编织袋出现但未打开");
            }
            else if(openBagCount==2)
            {
                Debug.Log("进入碰撞体：编织袋已打开露出笔记，但还未收集");
            }
        }
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
    
    /// <summary>
    /// 点火烧人
    /// </summary>
    public void SetFire()
    {
        StartCoroutine(FireAndDie());
    }

    /// <summary>
    /// 开启一个协程，在放火之后人倒下
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireAndDie()
    {
        GameManager.Instance.LookUpToSky();
        rootAnimator.Play("NPC7Dying");
        yield return new WaitForSeconds(13.0f); // 火焰动画是4+9=13秒，前3秒是镜头强制看向月亮
        animController.Play("die");
        yield return new WaitForSeconds(1.0f); // 死亡动画1秒
        isFired = true; // 已经被烧死了
        // 死掉之后可以解锁视角
        GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = false;
        GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = false;
        foreach (NavNPC item in navNPCs) // 移动到指定位置
        {
            item.MoveToTargetPos();
        }
    }

    private void SetPeterTalked()
    {
        isPeterTalked = true;
    }

    private void SetPieceGoInactive()
    {
        PieceGo.SetActive(false);
    }
}
