using UnityEngine;
using UnityEngine.UI;

public class PanelMain : MonoBehaviour
{
    private Button btnNotePrompt;
    private Button btnIconNote;
    private Button btnIconBag;
    private Button btnHelp;
    // 笔记的红点点
    private Image imgNewItem1;
    public Image ImgNewItem1{get{return imgNewItem1;}set{imgNewItem1 = value;}}
    // 背包的红点点
    private Image imgNewItem2;
    public Image ImgNewItem2{get{return imgNewItem2;}set{imgNewItem2 = value;}}

    void Awake()
    {
        btnNotePrompt = transform.Find("Btn_NotePrompt").GetComponent<Button>();
        btnIconNote = transform.Find("Btn_IconNote").GetComponent<Button>();
        btnIconBag = transform.Find("Btn_IconBag").GetComponent<Button>();
        btnHelp = transform.Find("Btn_Help").GetComponent<Button>();
        imgNewItem1 = btnIconNote.transform.Find("Img_NewItem").GetComponent<Image>();  
        imgNewItem2 = btnIconBag.transform.Find("Img_NewItem").GetComponent<Image>();
    }
    
    void Start()
    {
        // 注册按钮，点击展示背包页面（默认在背包页面）
        btnIconNote.onClick.AddListener(GameManager.Instance.mCurrentScene.ShowPanelBag);
        // 注册按钮，点击展示可使用物品页码
        btnIconBag.onClick.AddListener(GameManager.Instance.mCurrentScene.ShowPanelUsage);
        // 注册按钮，点击加入背包
        btnNotePrompt.onClick.AddListener(AddNoteToBag);
        // 注册按钮，点击打开键位操作
        btnHelp.onClick.AddListener(GameManager.Instance.mCurrentScene.ShowPanelKeyboard);
        // 一开始笔记出现提示和两个红点都不出现
        btnNotePrompt.gameObject.SetActive(false);
        imgNewItem1.gameObject.SetActive(false); imgNewItem2.gameObject.SetActive(false);
        // 给方法AddItemToUsage绑定事件名称
        GameManager.Instance.eventCenter.AddEventListener("FetchKey",AddItemToUsage);
    }

    /// <summary>
    /// 显示笔记提示
    /// </summary>
    public void ShowNotePrompt()
    {
        btnNotePrompt.gameObject.SetActive(true);
    }

    /// <summary>
    /// 收入笔记碎片到背包
    /// </summary>
    public void AddNoteToBag()
    {
        btnNotePrompt.gameObject.SetActive(false);
        imgNewItem1.gameObject.SetActive(true); // 出现红点
        Debug.Log("加入笔记碎片到背包");
        // 其他的操作，如恢复视角转动，场景中的笔记碎片消失
        GameManager.Instance.protagonist.GetComponent<MouseLook>().isLocked = false; // 锁定视角
        GameManager.Instance.protagCamera.GetComponent<MouseLook>().isLocked = false; // 锁定视角
        // NPC_Tom.Instance.PieceGo.SetActive(false);
        GameManager.Instance.eventCenter.TriggerEvent("PieceGoInactive");
    }

    private void AddItemToUsage()
    {
        // Debug.Log("trigger additemtousage");
        imgNewItem2.gameObject.SetActive(true); // 出现红点
    }
}
