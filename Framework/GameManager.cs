
using System.Runtime.CompilerServices;
using System;
using System.IO;
using LitJson;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public InputManager inputManager;
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

        if(eventCenter==null){ eventCenter = new EventCenter(); }// 实例化一个事件中心类（如果没有）
        if(inputManager==null){ inputManager = new InputManager(); }// 实例化一个输入管理器类（如果没有）

        // else{ eventCenter.ClearEvents(); } // 每次过场景调用Awake方法时都会清空事件中心中的事件

        protagonist = GameObject.Find("Protagonist");
        protagCamera = protagonist.transform.Find("Camera").gameObject;
        protagScope = protagCamera.transform.Find("scope").gameObject;
        mImgMoon = GameObject.Find("Canvas_Moon").transform.GetComponentInChildren<Image>();
        imageLoad = transform.Find("Canvas_Load").Find("Panel_Load").GetComponent<Image>();
    }

    void Start()
    {
        protagScope.SetActive(false); // 一开始不出现探测仪
        imageLoad.gameObject.SetActive(false); // 一开始不出现过场黑幕

        // 添加输入检测（按键）的监听事件
        eventCenter.AddEventListener<KeyCode>("Key", KeyHold);
        eventCenter.AddEventListener<KeyCode>("KeyUp", KeyUp);
    }

    private void KeyHold(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.P: // 如果按下的是P，就执行以下操作
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
                    // if(lastHit!=null){ ChangeOutline(lastHit,true); }
                    ChangeOutline(lastHit,true);
                }
                else
                {
                    ChangeOutline(lastHit,false);
                }
                break;
            default:
                break;
        }
    }
    
    private void KeyUp(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.P:
                // 解锁视角
                protagonist.GetComponent<MouseLook>().isLocked = false;
                Instance.protagCamera.GetComponent<MouseLook>().isLocked = false;
                // 探测仪消失
                protagScope.SetActive(false);
                this.eventCenter.TriggerEvent("ProbeOff");
                // 可被探测的物体没有红边
                ChangeOutline(lastHit,false);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        // if(Input.GetMouseButtonDown(0)) // 点击鼠标不符合交互逻辑，要改成直接（悬浮）检测
        # region(舍弃，因为输入检测部分已经用InputManager实现)
        // if(Input.GetKey(KeyCode.P)) // 按住P时开启“探测”（Probe)，同时要视角锁定
        // {
        //     // 锁定视角
        //     protagonist.GetComponent<MouseLook>().isLocked = true;
        //     Instance.protagCamera.GetComponent<MouseLook>().isLocked = true;
        //     // 显示探测仪
        //     protagScope.SetActive(true);
        //     // 播放镜头特效
        //     this.eventCenter.TriggerEvent("ProbeOn");
        //     // 射线检测
        //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out hit, 10, 1<<LayerMask.NameToLayer("Concealment")))
        //     {
        //         lastHit = hit.collider.gameObject;
        //         // Debug.Log(lastHit.name+" 被点中");
        //         ChangeOutline(lastHit,true);
        //     }
        //     else
        //     {
        //         // Debug.Log("没有东西被检测到");
        //         if(lastHit!=null)
        //         {
        //             // Debug.Log(lastHit.name+" 鼠标离开");
        //             ChangeOutline(lastHit,false);
        //         }
        //     }
        // }
        // else if(Input.GetKeyUp(KeyCode.P)) // 抬起
        // {
        //     // 解锁视角
        //     protagonist.GetComponent<MouseLook>().isLocked = false;
        //     Instance.protagCamera.GetComponent<MouseLook>().isLocked = false;
        //     // 探测仪消失
        //     protagScope.SetActive(false);
        //     this.eventCenter.TriggerEvent("ProbeOff");
        //     // 可被探测的物体没有红边
        //     ChangeOutline(lastHit,false);
        // }
        # endregion
        
        inputManager.CheckInputUpdate(); // 每一帧都执行检测输入的方法，并触发事件，如下所示例
        # region(舍弃，原本是CheckInputUpdate中执行的语句)
        // if(Input.GetKeyDown(key))
        // {
        //     GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("KeyDown",key);
        // }
        // if(Input.GetKey(key))
        // {
        //     GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("Key",key);
        // }
        // if(Input.GetKeyUp(key))
        // {
        //     GameManager.Instance.eventCenter.TriggerEvent<KeyCode>("KeyUp",key);
        // }
        # endregion
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
        if(go==null){return;} // 如果lastHit为空，直接返回（安全校验）
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
        // 使用 streamingAssetsPath 来拼接文件路径
        string fliePath = Application.streamingAssetsPath + "/Jsons/" + jsonName + ".json";
        
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
        // 使用 streamingAssetsPath 来拼接文件路径
        string fliePath = Application.streamingAssetsPath + "/Jsons/" + jsonName + ".json";

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
        // 开启协程加载新的场景
        StartCoroutine(LoadSceneAsync(scene,()=>{
            imageLoad.DOFade(0.0f,1.0f); // 加载完成之后执行的委托是画面变亮
            if(eventCenter!=null){ eventCenter.ClearEvents(); } // 且清空事件中心中所有的事件
        }));
        // 先播放过场动画（画面变黑），动画时间可能比加载时间短
        imageLoad.gameObject.SetActive(true);
        imageLoad.DOFade(1.0f,0.5f);
    }

    private IEnumerator LoadSceneAsync(SCENE scene, UnityAction callback)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
        loadScene.allowSceneActivation = true;
        // 循环检查加载进度
        while (loadScene.isDone==false)
        {
            // 其他逻辑，如更新进度条
            // 挂起
            yield return loadScene.progress;
        }
        yield return new WaitForSeconds(0.5f);
        callback(); // 执行委托
    }
}

/// <summary>
/// 场景的枚举，表示场景序号
/// </summary>
public enum SCENE
{
    Enter = 0,
    Village = 1,
    Underground = 2
}