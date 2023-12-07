using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PanelEnter : MonoBehaviour
{
    private Image imageLoad;
    void Awake()
    {
        imageLoad = transform.Find("Panel_Load").GetComponent<Image>();
        imageLoad.color = new Color(0,0,0,0);
    }

    public void ChangeToVillageScene()
    {
        // 开启异步加载的协程
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync((int)SCENE.Village, LoadSceneMode.Single);
        loadScene.allowSceneActivation = false; // 先不跳转

        // 播放过场动画，画面在1.5秒内逐渐变黑
        imageLoad.gameObject.SetActive(true);
        imageLoad.DOFade(1.0f,2.0f).OnComplete(()=>{
            loadScene.allowSceneActivation=true;
        });

        // 使用while循环来检查加载进度，直到达到0.9就不再挂起
        while (loadScene.progress < 0.9f)
        {
            yield return null; // 等待一帧
        }
    }
}
