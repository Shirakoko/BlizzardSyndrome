using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelBag : MonoBehaviour
{
    private Transform imgNoteTrans;
    private int currentNoteIndex; // 当前日记的索引
    private Transform imgMissionTrans;
    private Button btnMission;
    private Button btnNote;
    private Button btnClose;
    [Header("按钮的图片")]
    public Sprite[] btnSprites;

    // [Header("总任务UI")]
    // public GameObject mainMission;
    [Header("子任务UI")]
    public GameObject[] subMissions;
    private int currentMissionIndex = -1; // 当前正在执行的子任务索引
    [Header("神父日记的图片")]
    public Sprite[] diarySprites;
    public Sprite[] clearSprites; // 清除污点后的日记
    private bool[] isClear; // 是否解锁
    public static PanelBag Instance{get; private set;}

    void Awake()
    {
        Instance = this;
        imgNoteTrans = transform.Find("Img_Note");
        imgMissionTrans = transform.Find("Img_Mission");
        btnMission = transform.Find("Btn_Mission").GetComponent<Button>();
        btnNote = transform.Find("Btn_Note").GetComponent<Button>();
        btnClose = transform.Find("Btn_Close").GetComponent<Button>();
        isClear = new bool[diarySprites.Length];
    }

    void Start()
    {
        // 不可见
        this.gameObject.SetActive(false);
        // 注册按钮
        btnClose.onClick.AddListener(GameManager.Instance.mCurrentScene.HidePanelBag);
        // 显示第一个子任务，其余子任务不显示
        AddNewMission();
        // 显示第0篇神父日记
        currentNoteIndex = 0; imgNoteTrans.GetComponent<Image>().sprite = diarySprites[currentNoteIndex];
    }   

    public void ChangeToMission()
    {
        // 更换按钮样式
        btnMission.GetComponent<Image>().sprite = btnSprites[0]; btnMission.GetComponent<Image>().SetNativeSize();
        btnNote.GetComponent<Image>().sprite = btnSprites[3]; btnNote.GetComponent<Image>().SetNativeSize();
        // 更换面板
        imgMissionTrans.gameObject.SetActive(true); imgNoteTrans.gameObject.SetActive(false);
    }

    public void ChangeToNote()
    {
        // 更换按钮样式
        btnMission.GetComponent<Image>().sprite = btnSprites[1]; btnMission.GetComponent<Image>().SetNativeSize();
        btnNote.GetComponent<Image>().sprite = btnSprites[2]; btnNote.GetComponent<Image>().SetNativeSize();
        // 更换面板
        imgMissionTrans.gameObject.SetActive(false); imgNoteTrans.gameObject.SetActive(true);
        // 显示当前页笔记内容，要根据是否clear显示对应的版本
        if(!isClear[currentNoteIndex]){imgNoteTrans.GetComponent<Image>().sprite = diarySprites[currentNoteIndex];}
        else{imgNoteTrans.GetComponent<Image>().sprite = clearSprites[currentNoteIndex];}
    }

    /// <summary>
    /// 添加新的子任务
    /// </summary>
    public void AddNewMission()
    {
        currentMissionIndex++;
        for(int i=0;i<subMissions.Length;i++)
        {
            if(i<currentMissionIndex){subMissions[i].transform.Find("CompleteMark").gameObject.SetActive(true);}
            else if(i==currentMissionIndex)
            {
                subMissions[i].SetActive(true);
                subMissions[i].transform.Find("CompleteMark").gameObject.SetActive(false);
            }
            else{subMissions[i].SetActive(false);}
        }
    }

    public void ToLastDiary()
    {
        if(currentNoteIndex>0)
        {
            currentNoteIndex--;
            if(!isClear[currentNoteIndex])
            {
                imgNoteTrans.GetComponent<Image>().sprite = diarySprites[currentNoteIndex];
            }
            else
            {
                imgNoteTrans.GetComponent<Image>().sprite = clearSprites[currentNoteIndex];
            }
        }
    }

    public void ToNextDiary()
    {
        if(currentNoteIndex<diarySprites.Length-1)
        {
            currentNoteIndex++;
            if(!isClear[currentNoteIndex])
            {
                imgNoteTrans.GetComponent<Image>().sprite = diarySprites[currentNoteIndex];
            }
            else
            {
                imgNoteTrans.GetComponent<Image>().sprite = clearSprites[currentNoteIndex];
            }
        }
    }

    /// <summary>
    /// 清除笔记污点
    /// </summary>
    /// <param name="index">笔记索引</param>
    public void ClearDiary(int index)
    {
        currentNoteIndex = index; isClear[currentNoteIndex] = true;
        imgNoteTrans.GetComponent<Image>().sprite = clearSprites[currentNoteIndex];
    }
<<<<<<< HEAD
=======

    public void ToUnderground()
    {
        GameManager.Instance.ChangeToScene(SCENE.Underground);
    }

    public void ToVillage()
    {
        GameManager.Instance.ChangeToScene(SCENE.Village);
    }

    public void ToEnter()
    {
        GameManager.Instance.ChangeToScene(SCENE.Enter);
    }
>>>>>>> 90c4bc3 (1208)
}
