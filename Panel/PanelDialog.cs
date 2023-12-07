using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
<<<<<<< HEAD
using Unity.PlasticSCM.Editor.WebApi;
=======
// using Unity.PlasticSCM.Editor.WebApi;
>>>>>>> 90c4bc3 (1208)

public class PanelDialog : MonoBehaviour
{
    public Image imgFrameDialog; // 对话框背景
    public Text textCharaName; // 角色名称
    public Text textWord; // 它说的话
    public BaseNPC currentNPC; // 当前这个对话框显示的话语是某个NPC说的
    public GameObject optionsGo; // 选项卡
    private Button[] buttonOptions;
    private Text[] textOptions;
    private bool canNext; // 是否能按下N开启下一句，为了防止玩家打断NPC说话

    void Awake()
    {
        imgFrameDialog = transform.Find("Img_DialogFrame").GetComponent<Image>();
        textCharaName = transform.Find("Text_CharaName").GetComponent<Text>();
        textWord = transform.Find("Text_CharaWord").GetComponent<Text>();
        buttonOptions = new Button[2]; textOptions = new Text[2];
        buttonOptions[0] = optionsGo.transform.GetChild(0).GetComponent<Button>(); textOptions[0] = buttonOptions[0].GetComponentInChildren<Text>();
        buttonOptions[1] = optionsGo.transform.GetChild(1).GetComponent<Button>(); textOptions[1] = buttonOptions[1].GetComponentInChildren<Text>();
        optionsGo.SetActive(false); // 默认不显示选项卡
    }

    void OnEnable()
    {
        ShowText(++currentNPC.currentWordIndex); // 激活后展示第一句话
    }

<<<<<<< HEAD
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N) && canNext) // 按下N键下一句
        {
            ShowText(++currentNPC.currentWordIndex);
        }
    }

    public void ShowText(int index)
=======
    void Start()
    {
        // 添加输入检测（按键）的监听事件，按下N键下一句
        GameManager.Instance.eventCenter.AddEventListener<KeyCode>("KeyDown",(KeyCode key)=>{
            if(key == KeyCode.N && canNext)
            {
                ShowText(++currentNPC.currentWordIndex);
            }
        });
    }
    
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.N) && canNext) // 按下N键下一句
    //     {
    //         ShowText(++currentNPC.currentWordIndex);
    //     }
    // }

    private void ShowText(int index)
>>>>>>> 90c4bc3 (1208)
    {
        canNext = false;
        if(index>=currentNPC.dialogCount)
        {
            // Debug.Log("对话已经结束了");
            currentNPC.isTalked = true; // 设置为已经交谈过了
            this.gameObject.SetActive(false); // 关闭对话框
            // 重置NPC的对话和选项进度
            currentNPC.currentWordIndex = -1;
            currentNPC.currentOptionIndex = -1;
            if(currentNPC.npcName == "Tina")
            {
                // 新增PanelBag中的子任务1
                PanelBag.Instance.AddNewMission();
            }
            else if(currentNPC.npcName == "Mike")
            {
                // 新增PanelBag中的子任务2
                PanelBag.Instance.AddNewMission();
            }
            // 如果是可怜的即将被烧死的Tom，要点火
            else if(currentNPC.npcName == "Tom") 
            {
                (currentNPC as NPC_Tom).SetFire(); // 放火
                return;
            }
            // 如果是带路看房的Peter，看房之后需要回去捡编织袋，这里给事件中心发送事件
            else if(currentNPC.npcName == "Peter")
            {
                (currentNPC as NPC_Peter).TalkCount++;
                GameManager.Instance.eventCenter.TriggerEvent("PeterTalked");
                if((currentNPC as NPC_Peter).TalkCount==2)
                {
                    // 新增PanelBag中的子任务3
                    PanelBag.Instance.AddNewMission();
                }
            }
            else if(currentNPC.npcName == "Adam")
            {
                (currentNPC as NPC_Adam).TalkCount++;
            }
            else if(currentNPC.npcName == "Abbey")
            {
                (currentNPC as NPC_Abbey).TalkCount++;
            }
            else if(currentNPC.npcName == "Alex")
            {
                if((currentNPC as NPC_Alex).TalkCount==3)
                {
                    // 新增PanelBag中的子任务4
                    PanelBag.Instance.AddNewMission();
                }
                (currentNPC as NPC_Alex).TalkCount++;
            }
            // 设置视角恢复转动
            GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = false;
            GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = false;
            return;
        }

        bool isTriggerOptionPanel = currentNPC.dialogInfo.dialogs[index].isTriggerOptionPanel; // 是否触发选项卡
        // textCharaName.text = currentNPC.npcName;
        textCharaName.text = currentNPC.dialogInfo.dialogs[index].charaName;

        // 播放文字动画之前先把原来的文字置空
        textWord.text = "";
        string targetText = currentNPC.dialogInfo.dialogs[index].charaWord;

        Tween ShowTextTween = textWord.DOText(targetText,targetText.Length*0.05f).Pause(); // 文字输入动画，先暂停

        if(isTriggerOptionPanel==false) // 如果该对话不会触发选项分支
        {
            ShowTextTween.OnComplete(()=>{canNext = true;}); 
            ShowTextTween.Play();
        }
        else // 如果该对话会触发选项分支
        {
            // 文字播放完成后进入选项分支
            ShowTextTween.OnComplete(()=>{optionsGo.SetActive(true);}); 
            SetOptionTextNext(); // 设置选项信息
            ShowTextTween.Play();
        }
    }

    /// <summary>
    /// 设置选项分支的文字
    /// </summary>
    /// <param name="optionIndex">选项分支的索引</param>
    private void SetOptionTexts(int optionIndex)
    {
        if(optionIndex>=0 && optionIndex+1 <= currentNPC.optionInfo.options.Count)
        {
            // Debug.Log("设置选项信息："+optionIndex);
            textOptions[0].text = currentNPC.optionInfo.options[optionIndex].option_1;
            textOptions[1].text = currentNPC.optionInfo.options[optionIndex].option_2;
        }
        else{Debug.Log("索引不符合要求");}
    }

    public void SetOptionTextNext()
    {
        SetOptionTexts(++currentNPC.currentOptionIndex);
    }

    public void ChooseOption()
    {
        optionsGo.SetActive(false); // 选项分支面板消失
        // 由于剧情只有一条线，过剧情增并加索引，相当按N下一幕
        ShowText(++currentNPC.currentWordIndex);
    }
}
