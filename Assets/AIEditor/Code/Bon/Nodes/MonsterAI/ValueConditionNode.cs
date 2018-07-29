using System;
using GlobalEnum;
using UnityEngine;
using MonsterAISystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Code.Bon.Nodes.MonsterAINode
{
  [Serializable]
  [GraphContextMenuItem("MonsterAI", "Value Condition")]
  public class ValueConditionNode : AbstracStateConditionNode
  {
    [SerializeField] private float v1, v2;
    [SerializeField] private bool percentage;

    [SerializeField] private int index;
    [SerializeField] private int index2;

    [NonSerialized] private GlobalEnum.ValueType valueType;
    [NonSerialized] private ValueConditionType valueConditionType;

    [NonSerialized] private string[] options = new string[] { "Less", "Greater", "Between" };
    [NonSerialized] private string[] options2 = new string[] { "HP", "MP"};

    public ValueConditionNode(int id, Graph parent) : base(id, parent)
    {
      Height = 95;
      Width = 180;
    }

    public override void OnGUI()
    {
      EditorGUILayout.BeginHorizontal();

      GUILayout.Space(5);

      index2 = EditorGUILayout.Popup(index2, options2);
      valueType = (GlobalEnum.ValueType)index2;

      index = EditorGUILayout.Popup(index, options);
      valueConditionType = (ValueConditionType)index;

      GUILayout.Space(5);

      EditorGUILayout.EndHorizontal();

      GUILayout.Space(7);

      percentage = GUILayout.Toggle(percentage, new GUIContent("Percentage"));

      GUILayout.Space(7);

      GUILayout.BeginHorizontal();

      string s = string.Empty;

      switch (valueType)
      {
        case GlobalEnum.ValueType.HP:
          s = "HP";
          break;

        case GlobalEnum.ValueType.MP:
          s = "MP";
          break;
      }

      switch (valueConditionType)
      {
        case ValueConditionType.Less:
          GUILayout.Label(string.Format("{0} <", s));

          v1 = EditorGUILayout.FloatField(v1);

          GUILayout.Space(50);
          break;

        case ValueConditionType.Greater:
          GUILayout.Label(string.Format("{0} >", s));

          v1 = EditorGUILayout.FloatField(v1);

          GUILayout.Space(50);
          break;

        case ValueConditionType.Between:

          GUILayout.Space(7);

          v1 = EditorGUILayout.FloatField(v1);

          GUILayout.Label(string.Format("< {0} <", s));

          v2 = EditorGUILayout.FloatField(v2);

          GUILayout.Space(7);
          break;
      }

      GUILayout.EndHorizontal();
    }

    public override void Update()
    {

    }

    public override IDataBase GetDataBase()
    {
      ValueCondition condition = new ValueCondition();

      JsonHPCondition json = new JsonHPCondition();

      json.typeID = Id;

      json.hp = v1;
      json.hp2 = v2;
      json.percentage = percentage;
      json.valueType = (int)valueType;
      json.valueConditionType = (int)valueConditionType;

      json.nextConditionID = GetOutputAI();

      json.createType = condition.GetType().FullName;

      return json;
    }
  }
}

