using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageSystem
{
  private static Dictionary<object, IObserverBase> mvcDictionary = new Dictionary<object, IObserverBase>();

  private MessageSystem()
  {

  }

  /// <summary>
  /// 添加監控
  /// </summary>
  public static void AddListen<T>(string typeName, IObserverBase observerBase) where T : IObserverBase
  {
    mvcDictionary.Add(typeName, observerBase);
  }

  /// <summary>
  /// 添加監控
  /// </summary>
  public static void AddListen<T>(IObserverBase observerBase) where T : IObserverBase
  {
    mvcDictionary.Add(observerBase.GetType(), observerBase);
  }

  /// <summary>
  /// 監控移除
  /// </summary>
  public static void RemoveListen<T>(string typeName) where T : IObserverBase
  {
    mvcDictionary.Remove(typeName);
  }

  /// <summary>
  /// 監控移除
  /// </summary>
  public static void RemoveListen<T>() where T : IObserverBase
  {
    Type t = typeof(T);
    mvcDictionary.Remove(t);
  }

  /// <summary>
  /// 取得類別
  /// </summary>
  public static T GetClass<T>(string typeName) where T : IObserverBase
  {
    IObserverBase mVC_Base;

    if (mvcDictionary.TryGetValue(typeName, out mVC_Base))
      return (T)mVC_Base;

    return default(T);
  }

  /// <summary>
  /// 取得類別
  /// </summary>
  public static T GetClass<T>() where T : IObserverBase
  {
    IObserverBase mVC_Base;

    if (mvcDictionary.TryGetValue(typeof(T), out mVC_Base))
      return (T)mVC_Base;

    return default(T);
  }

  /// <summary>
  /// 事件推送，啟用他類的方法
  /// </summary>
  public static void PushOnEvent(string targetClass, string eventName)
  {
    PushOnEvent pushOnEvent;
    pushOnEvent.pushData = default(IDataBase);
    pushOnEvent.eventName = eventName;

    PushEvent(targetClass, pushOnEvent);
  }

  /// <summary>
  /// 事件推送，啟用他類的方法
  /// </summary>
  public static void PushOnEvent(string targetClass, string eventName, IDataBase data)
  {
    PushOnEvent pushOnEvent;
    pushOnEvent.pushData = data;
    pushOnEvent.eventName = eventName;

    PushEvent(targetClass, pushOnEvent);
  }

  /// <summary>
  ///  向該 class推送事件名稱
  /// </summary>
  private static void PushEvent(string typeName, PushOnEvent message)
  {
    IObserverBase call;

    if (mvcDictionary.TryGetValue(typeName, out call))
      call.OnEvent(message);
    else
      Debug.Log(string.Format("System: Cant push  Event, The {0} is null", typeName));
  }
}