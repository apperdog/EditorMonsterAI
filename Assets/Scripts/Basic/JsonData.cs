using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class JsonData: IDataBase
{
  [SerializeField] public string type;
  [SerializeField] public string data;

  public void Destory()
  {

  }
}

[Serializable]
public class JsonBase
{
  [SerializeField] public string createType;
}

[Serializable]
public class JsonDataList : ISerializationCallbackReceiver
{
  [NonSerialized] public List<IDataBase> dataBases;
  [SerializeField] public List<JsonData> jsonDatas;

  public JsonDataList()
  {
    dataBases = new List<IDataBase>();
    jsonDatas = new List<JsonData>();
  }

  public void OnAfterDeserialize()
  {
    for (int i = 0; i < jsonDatas.Count; i++)
    {
      JsonData jsonData = jsonDatas[i];

      Type type = Type.GetType(jsonDatas[i].type);
      IDataBase d = (IDataBase)Activator.CreateInstance(type);

      JsonUtility.FromJsonOverwrite(jsonDatas[i].data, d);

      dataBases.Add(d);
    }
  }

  public void OnBeforeSerialize()
  {
    for (int i = 0; i < dataBases.Count; i++)
    {
      string s = JsonUtility.ToJson(dataBases[i]);

      JsonData jsonData = new JsonData();
      jsonData.type = dataBases[i].GetType().FullName;
      jsonData.data = s;

      jsonDatas.Add(jsonData);
    }
  }

  public void Set(IDataBase data)
  {
    dataBases.Add(data);
  }

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }

  public void Clear()
  {
    dataBases.Clear();
    jsonDatas.Clear();
  }
}
