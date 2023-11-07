using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUsage : MonoBehaviour
{
    // 背包解锁的个数
    private int currentUsage = -1;
    [Header("背包中的物体图片")]
    public Image[] imgUsages;
    [Header("背包中的物体的描边")]
    public Button[] btnOutlines;
    void Awake()
    {
        // 格子里的图片全都不可见
        foreach (var item in imgUsages)
        {
            item.gameObject.SetActive(false);
        }
        // 描边也全都不可见
        foreach (var item in btnOutlines)
        {
            item.gameObject.SetActive(false);
        }
        // 给方法FetchOne绑定事件名称
        GameManager.Instance.eventCenter.AddEventListener("FetchKey",FetchOne);
        // 给方法OutLine0On和OutLine0Off绑定事件名称
        GameManager.Instance.eventCenter.AddEventListener("OutLine0On",OutLine0On);
        GameManager.Instance.eventCenter.AddEventListener("OutLine0Off",OutLine0Off);
        // 给按钮OutLine0注册事件
        if(btnOutlines[0]!=null){btnOutlines[0].onClick.AddListener(OnOutLine0Click);}
    }

    /// <summary>
    /// 得到一个物品
    /// </summary>
    private void FetchOne()
    {
        currentUsage++;
        if (currentUsage>-1 && currentUsage<imgUsages.Length)
        {
            imgUsages[currentUsage].gameObject.SetActive(true); // 可见
        }
    }

    /// <summary>
    /// 描边第一个格子
    /// </summary>
    private void OutLine0On()
    {
        if(imgUsages[0].gameObject.activeInHierarchy)
        {
            btnOutlines[0].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 描边第一个格子
    /// </summary>
    private void OutLine0Off()
    {
        if(imgUsages[0].gameObject.activeInHierarchy)
        {
            btnOutlines[0].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 第一个格子（钥匙）的描边被点击
    /// </summary>
    private void OnOutLine0Click()
    {
        Debug.Log("OutLine0Click");
        // 描边消失
        btnOutlines[0].gameObject.SetActive(false);
        // 触发打开监狱门的事件
        GameManager.Instance.eventCenter.TriggerEvent("OpenJail");
    }
}
