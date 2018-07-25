using System;
using UnityEngine;
using MonsterAISystem;

namespace Assets.Code.Bon.Nodes.MonsterAINode
{
  [Serializable]
  [GraphContextMenuItem("MonsterAI", "Controller")]
  public class MonsterAINode : AbstracMonsterAINode
  {
    public MonsterAINode(int id, Graph parent) : base(id, parent)
    {
      Height = 100;
      Width = 100;
    }

    public override void OnGUI()
    {
      bool aw = false;
      string m = string.Empty;

      GUIStyle style = new GUIStyle();
      style.alignment = TextAnchor.UpperLeft;

      GUILayout.BeginVertical();

      GUILayout.Label(" Condition", style);

      GUILayout.Space(7);

      GUILayout.FlexibleSpace();

      if (string.IsNullOrEmpty(enter))
      {
        aw = true;
        m = " No Have Enter Method";
      }
      else
        m = " Enter Method";

      GUILayout.Label(m, style);

      GUILayout.Space(7);

      if (string.IsNullOrEmpty(excuse))
      {
        aw = true;
        m = " No Have Excuse Method";
      }
      else
        m = " Excuse Method";

      GUILayout.Label(m, style);

      GUILayout.Space(7);

      if (string.IsNullOrEmpty(exit))
      {
        aw = true;
        m = " No Have Exit Method";
      }
      else
        m = " Exit Method";

      GUILayout.Label(m, style);

      GUILayout.FlexibleSpace();
      GUILayout.BeginVertical();

      if (aw)
        Width = 150;
      else
        Width = 100;
    }

    public override void Update()
    {
      enter = GetInputMethodString(inputEnterMethod);
      excuse = GetInputMethodString(inputExcuseMethod);
      exit = GetInputMethodString(inputExitMethod);
    }

    public override IDataBase GetDataBase()
    {
      JsonMonsterAI json = new JsonMonsterAI();

      json.bFrist = bFrist;

      json.enter = enter;
      json.excuse = excuse;
      json.exit = exit;

      json.typeID = Id;

      if (outSocket.IsConnected())
      {

      }

      json.typeName = "MonsterStateBase";

      return json;
    }
  }
}

