using UnityEditor;
using UnityEngine;

namespace EditorMonsterAI
{
  public abstract class BaseNode : ScriptableObject
  {
    public int windIndex;  // 窗口ID
    public string style;

    public Rect windowRect;  // 窗口
    public Vector2 editorPostion;  // 位置
    public string windowTitle = "";  // 窗口標題

    public BaseNode(int id)
    {
      windIndex = id;
    }

    public virtual void DrawWindow()
    {

    }

    public virtual void OnClick()
    {

    }
  }
}

