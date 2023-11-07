using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundScene : MonoBehaviour, IBaseScene
{
    
    public PanelDialog panelDialog; // 对话框面板
    public PanelBag panelBag; // 背包面板
    public PanelUsage panelUsage; // 使用的物品面板
    public PanelMain panelMain; // 主面板
    public PanelKeyboard panelKeyboard; // 键盘面板
    void Awake() 
    {
        RegisterScene();
        panelDialog.gameObject.SetActive(false);
        panelBag.gameObject.SetActive(false);
        panelMain.gameObject.SetActive(true);
        panelUsage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 在GameManager.Instance中注册当前场景
    /// </summary>
    public void RegisterScene()
    {
        GameManager.Instance.mCurrentScene = this;
    }

    /// <summary>
    /// 注册NPC
    /// </summary>
    /// <param name="npc">触发对话框的NPC</param>
    public void RegisterNPC(BaseNPC npc)
    {
        panelDialog.currentNPC = npc;
    }

    public void ShowPanelDialog()
    {
        panelDialog.gameObject.SetActive(true);
    }

    public void HidePanelDialog()
    {
        panelDialog.gameObject.SetActive(false);
    }

    /// <summary>
    /// 展示背包页面，默认在任务页面
    /// </summary>
    public void ShowPanelBag()
    {
        panelBag.gameObject.SetActive(true);
        panelMain.ImgNewItem1.gameObject.SetActive(false); // 每次点开后红点点消失
        panelBag.ChangeToMission(); // 默认在任务页面
    }

    public void HidePanelBag()
    {
        panelBag.gameObject.SetActive(false);
    }

    /// <summary>
    /// 展示背包页面，默认在笔记页面
    /// </summary>
    public void ShowPanelBagNote()
    {
        panelBag.gameObject.SetActive(true);
        panelMain.ImgNewItem1.gameObject.SetActive(false); // 每次点开后红点点消失
        panelBag.ChangeToNote(); // 跳转到笔记页面
    }

    public void ShowPanelKeyboard()
    {
        panelKeyboard.gameObject.SetActive(true);
    }

    public void HidePanelKeyboard()
    {
        panelKeyboard.gameObject.SetActive(false);
    }

    /// <summary>
    /// 展示使用物品页码
    /// </summary>
    public void ShowPanelUsage()
    {
        panelUsage.gameObject.SetActive(!panelUsage.gameObject.activeInHierarchy);
        panelMain.ImgNewItem2.gameObject.SetActive(false); // 每次点开后红点点消失
    }

}
