using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// 写一个空接口利用LSP充当字典的键
/// </summary>
public interface IEventInfo
{

}

/// <summary>
/// 用一个泛型类把委托包装起来
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventInfo<T>: IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action) // 构造函数，实例化时传进来一个泛型委托UnityAction<T>
    {
        actions += action;
    }
}

/// <summary>
/// 和前面的普通类是两个类，不能算重载（因为只有同名函数才算），但它们继承了同一个空接口IEventInfo
/// </summary>
public class EventInfo: IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action) // 构造函数，实例化时传进来一个泛型委托UnityAction<T>
    {
        actions += action;
    }
}

/// <summary>
/// 事件中心模块，在GameManager中实例化
/// </summary>
public class EventCenter
{
    // 注：UnityAction是<T0>是有一个参数且没有返回值的委托，这里把类型写成了万物之父object
    private Dictionary<string,IEventInfo> eventDict = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">监听者听到事件后的行为（有一个参数且返回值为空的方法）</param>
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            (eventDict[eventName] as EventInfo<T>).actions += action; // 里氏替换原则，将父接口转换成子类
        }
        else
        {
            eventDict.Add(eventName,new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// 重载添加事件监听的方法
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">监听者听到事件后的行为（没有参数且返回值为空的方法）</param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            (eventDict[eventName] as EventInfo).actions += action; // 里氏替换原则，将父接口转换成子类
        }
        else
        {
            eventDict.Add(eventName,new EventInfo(action));
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="obj">传入的参数</param>
    /// <typeparam name="T">参数类型</typeparam>
    public void TriggerEvent<T>(string eventName, T obj)
    {
        if(eventDict.ContainsKey(eventName))
        {
            if((eventDict[eventName] as EventInfo<T>).actions!=null) // 委托容器不为空时
            {
                (eventDict[eventName] as EventInfo<T>).actions(obj); // 传入参数并执行委托
            }
        }
    }

    /// <summary>
    /// 重载触发事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void TriggerEvent(string eventName)
    {
        if(eventDict.ContainsKey(eventName))
        {
            if((eventDict[eventName] as EventInfo).actions!=null) // 委托容器不为空时
            {
                (eventDict[eventName] as EventInfo).actions(); // 传入参数并执行委托
            }
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">需要移除的方法，是一个泛型委托</param>
    public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            (eventDict[eventName] as EventInfo<T>).actions -= action;
        }
    }

    /// <summary>
    /// 重载移除事件监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">需要移除的函数</param>
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if(eventDict.ContainsKey(eventName))
        {
            (eventDict[eventName] as EventInfo).actions -= action;
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
