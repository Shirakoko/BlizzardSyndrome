using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkManager : MonoBehaviour
{
    public GameObject[] landmarkGos;
    private int landmarkCount;
    private int currentIndex; // 当前激活的landmark的索引
    private static LandMarkManager instance; // 单例模式
    public static LandMarkManager Instance{get{return instance;}private set{instance = value;}}
    void Awake()
    {
        instance = this; // 饿汉式加载，类加载的时候就实例化（Mono脚本通过挂载在场景物体上来实例化）
        landmarkCount = transform.childCount;
        landmarkGos = new GameObject[landmarkCount]; // 实例化数组
        for (int i = 0; i<landmarkCount; i++) // 把子物体赋值进数组里
        {
            landmarkGos[i] = transform.GetChild(i).gameObject;
            landmarkGos[i].SetActive(false); // 先全部不可见
        }
    }

    void Start()
    {
        // 第0个可见
        landmarkGos[0].SetActive(true);
    }

    /// <summary>
    /// 显示下一个landmark
    /// </summary>
    public void ShowNextLandmark()
    {
        landmarkGos[currentIndex].SetActive(false); // 当前的消失
        if(currentIndex+1<landmarkCount)
        {
            landmarkGos[currentIndex+1].SetActive(true); // 显示下一个
        }
        currentIndex++;
    }
}
