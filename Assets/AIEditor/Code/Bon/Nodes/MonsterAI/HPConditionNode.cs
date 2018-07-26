using System;
using UnityEngine;
using MonsterAISystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Code.Bon.Nodes.MonsterAINode
{
  [Serializable]
  [GraphContextMenuItem("MonsterAI", "HP Condition")]
  public class HPConditionNode : AbstracStateConditionNode
  {
    [SerializeField] private float hp, hp2;
    [SerializeField] private bool percentage;
    [SerializeField] private HPConditionType hpConditionType;

    [NonSerialized] private int index;
    [NonSerialized] private string[] options = new string[] { "Less", "Greater", "Between" };

    public HPConditionNode(int id, Graph parent) : base(id, parent)
    {
      index = 0;

      Height = 65;
      Width = 180;
    }

    public override void OnGUI()
    {
      GUILayout.BeginHorizontal();

      GUILayout.Space(7);

      percentage = GUILayout.Toggle(percentage, new GUIContent("Percentage"));

      index = EditorGUILayout.Popup(index, options);
      hpConditionType = (HPConditionType)index;

      GUILayout.Space(7);

      GUILayout.EndHorizontal();

      GUILayout.Space(7);

      GUILayout.BeginHorizontal();

      switch (hpConditionType)
      {
        case HPConditionType.Less:
          GUILayout.Label("HP <");

          hp = EditorGUILayout.FloatField(hp);

          GUILayout.Space(50);
          break;

        case HPConditionType.Greater:
          GUILayout.Label("HP >");

          hp = EditorGUILayout.FloatField(hp);

          GUILayout.Space(50);
          break;

        case HPConditionType.Between:

          GUILayout.Space(7);

          hp = EditorGUILayout.FloatField(hp);

          GUILayout.Label("< HP <");

          hp2 = EditorGUILayout.FloatField(hp2);

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
      HPCondition condition = new HPCondition();

      JsonHPCondition json = new JsonHPCondition();

      json.typeID = Id;

      json.hp = hp;
      json.hp2 = hp2;
      json.percentage = percentage;
      json.hpConditionType = (int)hpConditionType;

      json.nextConditionID = GetOutputAI();

      json.createType = condition.GetType().FullName;

      return json;
    }
  }
}

