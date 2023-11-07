using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    public string npcName; // NPC的名字，与储存他的对话的json文件同名
    // public Vector3 birthPosition; // 出生位置
    // public Vector3 birthRotation; // 出生朝向
    public Animator animController; // 身上的动画组件
    protected bool isInCollider; // 玩家是否处在碰撞体范围内
    public bool isTalked; // 玩家是否已经和该NPC交谈过
    [Header("设置前置NPC")]
    public GameObject[] prenpcobjects;//在Inspector中设置若干个前置NPC的GameObject

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

    //对话交互触发的额外条件
    protected bool Preconditions()
    {
        for(int i = 0;i < prenpcobjects.Length; i++)
        {
           if (!prenpcobjects[i].GetComponent<PublicNPC>().GetState())
            {
               return false;
            }
        }
   
        return true;
    }


    /// <summary>
    /// 调用后若干秒之后消失
    /// </summary>
    /// <param name="time">调用后到消失前的时间</param>
    protected IEnumerator DisappearAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);      
    }
}
