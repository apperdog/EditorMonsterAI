using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace EditorMonsterAI
{
  public class MonsterStateNode : BaseInputNode
  {
    // 視窗高寬
    private int width, height, height2;

    // 方法編輯
    public UnityEvent exit;
    public UnityEvent enter;
    public UnityEvent execute;

    private SerializedObject sObj;
    private SerializedObject sObj2;
    private SerializedObject sObj3;

    public MonsterStateNode(int id, Vector2 postion) : base(id)
    {
      width = 400;
      height = 400;
      height2 = height;
      editorPostion = postion;

      windowTitle = "MonsterState";
      windowRect = new Rect(editorPostion.x, editorPostion.y, width, height);

      sObj = new SerializedObject(this);
      sObj2 = new SerializedObject(this);
      sObj3 = new SerializedObject(this);
    }

    public override void DrawWindow()
    {
      // 方法編輯
      SerializedProperty sProperty = sObj.FindProperty("enter");
      SerializedProperty sProperty2 = sObj.FindProperty("execute");
      SerializedProperty sProperty3 = sObj.FindProperty("exit");

      EditorGUILayout.PropertyField(sProperty, new GUIContent("進入該狀態時執行的方法"), true);
      EditorGUILayout.PropertyField(sProperty2, new GUIContent("在該狀態時執行的方法"), true);
      EditorGUILayout.PropertyField(sProperty3, new GUIContent("離開該狀態時執行的方法"), true);

      sObj.ApplyModifiedProperties();
      sObj2.ApplyModifiedProperties();
      sObj3.ApplyModifiedProperties();

      // 調整窗體寬度
      int newHeight = height2 + 
        (enter.GetPersistentEventCount() + execute.GetPersistentEventCount() + exit.GetPersistentEventCount()) * 30;

      if(height != newHeight)
      {
        height = newHeight;
        windowRect = new Rect(editorPostion.x, editorPostion.y, width, height);
      }
    }
  }
}

