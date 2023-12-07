using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelProbeEffect : MonoBehaviour
{
    void Start()
    {
        // 注册事件
        GameManager.Instance.eventCenter.AddEventListener("ProbeOn",ShowProbeEffect);
        GameManager.Instance.eventCenter.AddEventListener("ProbeOff",HideProbeEffect);
        // 隐藏
        this.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        // 移除事件
        GameManager.Instance.eventCenter.RemoveEventListener("ProbeOn",ShowProbeEffect);
        GameManager.Instance.eventCenter.RemoveEventListener("ProbeOff",HideProbeEffect);
    }

    /// <summary>
    /// 展示探测特效
    /// </summary>
    private void ShowProbeEffect()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 移除探测特效
    /// </summary>
    private void HideProbeEffect()
    {
        this.gameObject.SetActive(false);
    }
}
