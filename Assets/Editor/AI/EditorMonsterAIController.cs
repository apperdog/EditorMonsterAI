using System;
using UnityEditor;
using UnityEngine;
using MonsterAISystem;
using System.Collections.Generic;

namespace EditorMonsterAI
{
  public class EditorMonsterAIController : EditorWindow
  {
    // node
    private List<BaseNode> windows = new List<BaseNode>();

    // variable to store our mousePos
    private Vector2 mousePos;

    #region 滑鼠拖曳視窗
    float PanY;
    float PanX;

    private bool scrollWindow = false;
    private Vector2 scrollStartMousePos;
    #endregion

    #region 當前編譯的 MonsterAI

    private static bool selectAI;
    private static MonsterAI monsterAI;

    #endregion

    [@MenuItem("Window/怪物AI狀態機")]
    static void main()
    {
      selectAI = false;
      monsterAI = null;

      EditorMonsterAIController stateMachine = GetWindow<EditorMonsterAIController>(false, "变形动画状态机");
      stateMachine.minSize = new Vector2(800, 600);  // 視窗大小
      stateMachine.titleContent = new GUIContent(EditorGUIUtility.IconContent("Mirror"));  // 視窗標頭圖案
      stateMachine.titleContent.text = "怪物AI狀態機";  // 視窗標頭文字
      stateMachine.autoRepaintOnSceneChange = true;
      stateMachine.Show();
    }

    private void Update()
    {
      if (!selectAI)
      {
        // 如果單點選取一個GameObject
        if (Selection.objects.Length == 1 && Selection.objects[0].GetType() == typeof(GameObject))
        {
          GameObject go = Selection.objects[0] as GameObject;
          MonsterAI ai = go.GetComponent<MonsterAI>();

          // 選取的物件包含 MonsterAI
          if (!ReferenceEquals(ai, null))
            SelectMonsterAI(ai);
        }
      }
    }


    private void OnGUI()
    {
      // check event
      Event e = Event.current;

      // check mouse position
      mousePos = e.mousePosition;

      #region 滑鼠右鍵事件

      if (e.button == 1)
      {
        // 點下
        if (e.type == EventType.MouseDown)
        {
          bool clickedOnWindow = false;
          int selectedIndex = -1;

          // 不是點下 Node
          if (!clickedOnWindow)
          {
            GenericMenu menu = new GenericMenu();

            if (!selectAI)
              menu.AddItem(new GUIContent("建立新的 AI"), false, ContextCallback, "CreateAI");

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("建立新的 State"), false, ContextCallback, "CreateState");

            menu.ShowAsContext();
            e.Use();
          }
        }
      }

      #endregion

      //
      GUI.BeginGroup(new Rect(PanX, PanY, 100000, 100000));

      // draw the actual windows
      BeginWindows();

      for (int i = 0; i < windows.Count; i++)
      {
        if (!string.IsNullOrEmpty(windows[i].style))
          windows[i].windowRect = GUI.Window(windows[i].windIndex, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle, windows[i].style);
        else
          windows[i].windowRect = GUI.Window(windows[i].windIndex, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
      }

      EndWindows();

      GUI.EndGroup();

      #region 滑鼠拖曳視窗

      // 判定當前滑鼠事件是否為拖曳視窗
      if (e.keyCode == KeyCode.A && e.type == EventType.KeyDown)
      {
        if (scrollWindow == true)
        {
          scrollWindow = false;
        }
        else
        {
          scrollStartMousePos = e.mousePosition;
          scrollWindow = true;
        }
      }

      // 判定當前滑鼠事件是否為拖曳視窗
      if (e.button == 2)
      {
        if (e.type == EventType.MouseDown)
        {
          scrollStartMousePos = e.mousePosition;
          scrollWindow = true;
        }
        else if (e.type == EventType.MouseUp)
        {
          scrollWindow = false;
        }
      }

      // 紀錄滑鼠偏移量
      if (scrollWindow)
      {
        Vector2 mouseDiff = e.mousePosition - scrollStartMousePos;
        PanX += mouseDiff.x / 100;
        PanY += mouseDiff.y / 100;
      }

      #endregion
    }

    private void OnInspectorUpdate()
    {

    }

    private void SelectMonsterAI(MonsterAI ai)
    {
      selectAI = true;
      windows.Clear();

      monsterAI = ai;
      MonsterAINode node = new MonsterAINode(windows.Count, ai.monsterAI, ai.monsterAI.EditorPostion);

      windows.Add(node);
    }

    private void DrawNodeWindow(int id)
    {
      windows[id].DrawWindow();
      GUI.DragWindow();

      if (Event.current.GetTypeForControl(id) == EventType.Used)
        windows[id].OnClick();
    }

    #region 左鍵建立視窗

    // 左鍵建立 Node小視窗回調
    private void ContextCallback(object obj)
    {
      string clb = obj.ToString();

      switch (clb)
      {
        case "CreateAI":
          CreateNewAI();
          break;

        case "CreateState":
          CreateState();
          break;
      }
    }

    private void CreateNewAI()
    {
      string path = "Assets/MonsterAI.prefab";
      GameObject obj = new GameObject();
      MonsterAI ai = obj.AddComponent<MonsterAI>();
      ai.monsterAI = new MonsterAIBehaviour();
      ai.monsterAI.EditorPostion = mousePos;

      SelectMonsterAI(ai);

      PrefabUtility.CreatePrefab(path, obj);
    }

    private void CreateState()
    {
      MonsterStateNode monsterState = new MonsterStateNode(windows.Count, mousePos);
      windows.Add(monsterState);
    }

    #endregion
  }
}

