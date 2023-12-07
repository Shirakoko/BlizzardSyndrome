using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEditor.Animations;
using UnityEngine;

public class BaseNPC : MonoBehaviour
=======
// using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseNPC : MonoBehaviour, IPrompt
>>>>>>> 90c4bc3 (1208)
{
    public string npcName; // NPC的名字，与储存他的对话的json文件同名
    // public Vector3 birthPosition; // 出生位置
    // public Vector3 birthRotation; // 出生朝向
    public Animator animController; // 身上的动画组件
    protected bool isInCollider; // 玩家是否处在碰撞体范围内
<<<<<<< HEAD
=======
    protected bool isInInteraction; // 玩家是否与NPC处于交互中
    public Collider npcCol; // NPC子物体中的Collider组件
    protected GameObject prompt; // 身上的提示
>>>>>>> 90c4bc3 (1208)
    public bool isTalked; // 玩家是否已经和该NPC交谈过

    [Header("对话的总条数")]
    public int dialogCount = 0;

    [Header("对话进度的索引")]
    public int currentWordIndex = -1; // 对话进度的索引
    [Header("选项进度的索引")]
    public int currentOptionIndex = -1; // 选项进度的索引
    public DialogInfo dialogInfo; // 对话信息类
    public OptionInfo optionInfo; // 选项信息类
    
    protected virtual void Awake()
    {
        // 初始化位置和朝向
        // transform.position = birthPosition;
        // transform.rotation = Quaternion.Euler(birthRotation);
        // 搜索动画组件和碰撞组件的引用
        animController = transform.Find("Avatar").GetComponent<Animator>();

        // 使用写好的静态方法实例化信息类并赋值给变量
        dialogInfo = GameManager.LoadByJson(npcName);
        optionInfo = GameManager.LoadOption(npcName+"_Options");
        dialogCount = dialogInfo.dialogs.Count; // 赋值对话的总数量
    }

<<<<<<< HEAD
=======
    protected virtual void Start()
    {
        // animController = transform.Find("Avatar").GetComponent<Animation>();
        npcCol = GetComponent<Collider>(); 
        // 搜索图标提示
        prompt = transform.Find("PlanePrompt").gameObject;
    }

>>>>>>> 90c4bc3 (1208)
    /// <summary>
    /// 调用后若干秒之后消失
    /// </summary>
    /// <param name="time">调用后到消失前的时间</param>
    protected IEnumerator DisappearAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);      
    }
<<<<<<< HEAD
=======

    public void ShowPrompt()
    {
        prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        prompt.SetActive(false);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }

    public virtual void OnTriggerExit(Collider other)
    {
        
    }
>>>>>>> 90c4bc3 (1208)
}
