using System;
using System.Collections.Generic;

public class DataSystem
{
  private static Dictionary<Type, IDataBase> dataSystemList = new Dictionary<Type, IDataBase>();

  private DataSystem()
  {

  }

  /// <summary>
  /// 增加 class scripts
  /// </summary>
  public static void AddSystem<T>(T t) where T : IDataBase
  {
    dataSystemList.Add(typeof(T), t);
  }

  /// <summary>
  /// 取得該 class
  /// </summary>
  public static T GetSystem<T>() where T : IDataBase
  {
    IDataBase data;

    if (dataSystemList.TryGetValue(typeof(T), out data))
      return (T)data;

    return default(T);
  }

  /// <summary>
  /// 移除該 class scripts
  /// </summary>
  public static void RemoveSystem<T>() where T : IDataBase
  {
    IDataBase data;

    if (dataSystemList.TryGetValue(typeof(T), out data))
    {
      data.Destory();

      dataSystemList.Remove(typeof(T));
    }
  }
}