//using Assets.Code.Bon;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
//using Assets.Code.Bon.Nodes.MonsterAINode;

public class Test : MonoBehaviour
{
  public string path;
  public Text text;

  public TestData testData;
  public TestData2 testData2;

	void Start () {
    //testData = new TestData();
    //testData.enter = "u";
    //testData.enter2 = "hh";
    //testData.yo = new List<string>();
    //testData.yo.Add("ghf");

    //testData2 = new TestData2();
    //testData2.enter = 2;
    //testData2.enter2 = 2;

    //Data d = new Data();
    //d.Set(testData);
    //d.Set(testData2);

    //Save(path, d);

    //Data data;

    //if (File.Exists(path))
    //{
    //  var file = File.OpenText(path);
    //  var json = file.ReadToEnd();
    //  file.Close();
    //  data = JsonUtility.FromJson<Data>(json);
    //}

    //JsonDataList jsonData;

    //if (File.Exists(path))
    //{
    //  var file = File.OpenText(path);
    //  var json = file.ReadToEnd();
    //  file.Close();
    //  jsonData = JsonUtility.FromJson<JsonDataList>(json);
    //}


    //for (int i = 0; i < dataBases.Count; i++)
    //{
    //  JsonMonsterAI json = (JsonMonsterAI)dataBases[i];
    //  Type type = Type.GetType("MonsterAISystem." + json.typeName);

    //  MonsterStateBase d = (MonsterStateBase)Activator.CreateInstance(type);

    //  Debug.Log(d);
    //}

    Debug.Log("");
  }

  // Update is called once per frame
  void Update () {
		
	}

  public static bool Save(string fileName, Data graph)
  {
    var file = File.CreateText(fileName);
    file.Write((string)graph.ToJson());
    file.Close();
    return true;
  }
}

public class SerializationDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
  [SerializeField]
  private List<TKey> keys;
  [SerializeField]
  private List<TValue> values;

  private Dictionary<TKey, TValue> target;
  public Dictionary<TKey, TValue> ToDictionary() { return target; }

  public SerializationDictionary(Dictionary<TKey, TValue> target)
  {
    this.target = target;
  }

  public void OnBeforeSerialize()
  {
    keys = new List<TKey>(target.Keys);
    values = new List<TValue>(target.Values);
  }

  public void OnAfterDeserialize()
  {
    var count = Math.Min(keys.Count, values.Count);
    target = new Dictionary<TKey, TValue>(count);
    for (var i = 0; i < count; ++i)
    {
      target.Add(keys[i], values[i]);
    }
  }
}

[Serializable]
public class Data : ISerializationCallbackReceiver
{
  [NonSerialized]
  public List<IDataBase> dataBases = new List<IDataBase>();
  [SerializeField]
  public List<JsonData> saveString = new List<JsonData>();

  public void OnAfterDeserialize()
  {
    for(int i = 0; i < saveString.Count; i++)
    {
      JsonData jsonData = saveString[i];

      Type type = Type.GetType(saveString[i].type);
      IDataBase d = (IDataBase)Activator.CreateInstance(type);

      JsonUtility.FromJsonOverwrite(saveString[i].data, d);

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

      saveString.Add(jsonData);
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
}

[Serializable]
public class TestData : IDataBase
{
  [SerializeField] public string enter, enter2;
  [SerializeField] public List<string> yo = new List<string>();

  public void Destory()
  {

  }

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}

[Serializable]
public class TestData2 : IDataBase
{
  [SerializeField] public int enter, enter2;

  public void Destory()
  {

  }

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }
}