using UnityEngine;
using MonsterAISystem;

namespace EditorMonsterAI
{
  public class MonsterAINode : BaseNode
  {
    public MonsterAIBehaviour monsterAI;

    public MonsterAINode(int id, MonsterAIBehaviour monster, Vector2 postion) : base(id)
    {
      style = "flow node 3";
      monsterAI = monster;
      windowTitle = "MonsterAI";
      monsterAI.EditorPostion = postion;
      windowRect = new Rect(postion.x, postion.y, 200, 50);
    }
  }
}

