
using System;
using System.IO;
using LitJson;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private event UnityAction updateEvents; // 用于装载其他类中需要每一帧都调用的方法（减少Update的个数以提高性能）
    public GameObject protagonist; // 主角游戏物体
    private GameObject protagScope; // 主角随身带的探测仪
    public GameObject protagCamera; // 主角头上的摄像机
    public IBaseScene mCurrentScene; // 当前场景
    private Image imageLoad; // 过场景的黑幕
    public Image mImgMoon; // 月亮贴纸
    private Ray ray;
    // public RaycastHit[] hitColliders;
    private RaycastHit hit;
    private GameObject lastHit;
    public EventCenter eventCenter;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        // 保证只有一个GameManager游戏物体
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if(eventCenter==null) // 实例化一个事件中心类（如果没有）
        {
            eventCenter = new EventCenter();
        }
        else{eventCenter.ClearEvents();} // 每次过场景调用Awake方法时都会清空事件中心中的事件

        protagonist = GameObject.Find("Protagonist");
        protagCamera = protagonist.transform.Find("Camera").gameObject;
        protagScope = protagCamera.transform.Find("scope").gameObject;
        mImgMoon = GameObject.Find("Canvas_Moon").transform.GetComponentInChildren<Image>();
        imageLoad = transform.Find("Canvas_Load").Find("Panel_Load").GetComponent<Image>();
    }

    void Start()
    {
        protagScope.SetActive(false); // 一开始不出现探测仪
        imageLoad.gameObject.SetActive(false); // 一开始不出现过长黑幕
    }
    
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)) // 点击鼠标不符合交互逻辑，要改成直接（悬浮）检测
        if(Input.GetKey(KeyCode.P)) // 按住P时开启“探测”（Probe)，同时要视角锁定
        {
            // 锁定视角
            protagonist.GetComponent<MouseLook>().isLocked = true;
            Instance.protagCamera.GetComponent<MouseLook>().isLocked = true;
            // 显示探测仪
            protagScope.SetActive(true);
            // 播放镜头特效
            this.eventCenter.TriggerEvent("ProbeOn");
            // 射线检测
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10, 1<<LayerMask.NameToLayer("Concealment")))
            {
                lastHit = hit.collider.gameObject;
                // Debug.Log(lastHit.name+" 被点中");
                ChangeOutline(lastHit,true);
            }
            else
            {
                // Debug.Log("没有东西被检测到");
                if(lastHit!=null)
                {
                    // Debug.Log(lastHit.name+" 鼠标离开");
                    ChangeOutline(lastHit,false);
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.P)) // 抬起
        {
            // 解锁视角
            protagonist.GetComponent<MouseLook>().isLocked = false;
            Instance.protagCamera.GetComponent<MouseLook>().isLocked = false;
            // 探测仪消失
            protagScope.SetActive(false);
            this.eventCenter.TriggerEvent("ProbeOff");
            // 可被探测的物体没有红边
            ChangeOutline(lastHit,false);
        }
        // 最后调用updateEvents中的方法
        if(updateEvents!=null)
        {
            updateEvents();
        }
    }

    public void AddUpdateFunction(UnityAction func)
    {
        updateEvents += func;
    }

    public void RemoveUpdateFunction(UnityAction func)
    {
        updateEvents -= func;
    }
    /// <summary>
    /// 改变点中物体的描边情况
    /// </summary>
    /// <param name="go">点中物体</param>
    /// <param name="isAdd">是否添加描边</param>
    private void ChangeOutline(GameObject go, bool isAdd)
    {
        // 获取物体的 MeshRenderer 组件
        MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
        // 获取物体的第一个材质（一般就一个）
        Material material0 = meshRenderer.material;
        // Debug.Log(material0.name);
        if(material0.HasProperty("_Outline")) // 安全检验
        {
            // Debug.Log("拥有Ontline属性");
            if(isAdd){material0.SetFloat("_Outline", 0.05f);}
            else{material0.SetFloat("_Outline", 0.0f);};
        }
    }

    /// <summary>
    /// 读取Json文件，返回一个对话信息类
    /// </summary>
    /// <param name="jsonName">json文件的名称</param>
    /// <returns></returns>
    public static DialogInfo LoadByJson(string jsonName)
    {
        DialogInfo info = new DialogInfo();
        string fliePath = Application.dataPath+"/Resources/Jsons/"+jsonName+".json";
        
        if(File.Exists(fliePath))
        {
            // 1.使用File中的静态方法ReadAllText读取Json文件内容，保存在字符串中
            string jsonStr = File.ReadAllText(fliePath);
            // Debug.Log(jsonStr);
            // 2.将字符串转化为对象（泛型）
            info = JsonMapper.ToObject<DialogInfo>(jsonStr);
            // Debug.Log(info.dialogs.Count);
        }
        else{Debug.Log(fliePath+" 文件不存在");}

        if(info==null){Debug.Log("读取Json信息失败");}
        return info;
    }

    /// <summary>
    /// 读取Json文件，返回一个选项信息类
    /// </summary>
    /// <param name="jsonName">json文件的名称</param>
    /// <returns></returns>
    public static OptionInfo LoadOption(string jsonName)
    {
        OptionInfo info = new OptionInfo();
        string fliePath = Application.dataPath+"/Resources/Jsons/"+jsonName+".json";

        if(File.Exists(fliePath))
        {
            // 1.使用File中的静态方法ReadAllText读取Json文件内容，保存在字符串中
            string jsonStr = File.ReadAllText(fliePath);
            // Debug.Log(jsonStr);
            // 2.将字符串转化为对象（泛型）
            info = JsonMapper.ToObject<OptionInfo>(jsonStr);
            // Debug.Log(info.dialogs.Count);
        }
        else{Debug.Log("文件不存在");}

        if(info==null){Debug.Log("读取Json信息失败");}
        return info;
    }

    /// <summary>
    /// 抬头30度角仰望天空再转回来
    /// </summary>
    public void LookUpToSky()
    {
        Vector3 originalRotation = this.transform.rotation.eulerAngles;
        if(mImgMoon!=null) // 安全检验
        {
            mImgMoon.DOFade(1.0f,1.5f).OnComplete(()=>{
                mImgMoon.DOFade(0.5f,1.5f).SetDelay(1.0f);
            });
        }
        protagCamera.transform.DORotate(new Vector3(-30,0,0),1.5f).OnComplete(()=>{
            protagCamera.transform.DORotate(originalRotation,1.5f).SetDelay(1.0f);
        });
    }

    /// <summary>
    /// 跳转到一个场景
    /// </summary>
    /// <param name="scene">需要跳转的场景的枚举</param>
    public void ChangeToScene(SCENE scene)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
        loadScene.allowSceneActivation = false; // 先不跳转
        // 播放过场动画，结束后透明度下降到0再消失，然后加载新的场景
        imageLoad.gameObject.SetActive(true);
        imageLoad.DOFade(1.0f,1.0f).OnComplete(()=>{
            imageLoad.DOFade(0.0f,1.0f).OnComplete(()=>{imageLoad.gameObject.SetActive(false);loadScene.allowSceneActivation=true;});
        });
    }
}

/// <summary>
/// 事件中心类
/// </summary>
public class EventCenter
{
    private Dictionary<string,UnityAction> eventDict = new Dictionary<string, UnityAction>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">监听者听到事件后的行为（返回值为空的方法）</param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            eventDict[eventName] += action;
        }
        else
        {
            eventDict.Add(eventName,action);
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void TriggerEvent(string eventName)
    {
        if(eventDict.ContainsKey(eventName))
        {
            eventDict[eventName].Invoke(); // 执行委托
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">需要移除的方法</param>
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            eventDict[eventName] -= action;
        }
    }

    /// <summary>
    /// 清空事件中心中的所有事件
    /// </summary>
    public void ClearEvents()
    {
        eventDict.Clear();
    }
}

/// <summary>
/// 场景的枚举，表示场景序号
/// </summary>
public enum SCENE
{
    Village,
    Underground
}