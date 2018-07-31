using System;
using Assets.Code.Bon.Socket;
using Assets.Code.Bon.Interface;

namespace Assets.Code.Bon.Nodes
{
  public abstract class AbstracStateConditionNode : Node, IStateConditionNode
  {
    [NonSerialized] public OutputSocket outSocket;
    [NonSerialized] public InputSocket inputSocket01;

    public AbstracStateConditionNode(int id, Graph parent) : base(id, parent)
    {
      inputSocket01 = new InputSocket(this, typeof(AbstracMonsterAINode));
      outSocket = new OutputSocket(this, typeof(AbstracStateConditionNode));

      Sockets.Add(outSocket);
      Sockets.Add(inputSocket01);
    }

    public int GetOutputAiId
    {
      get
      {
        if (outSocket.IsConnected())
        {
          AbstracMonsterAINode node = (AbstracMonsterAINode)outSocket.GetConnectedSocket(0).Parent;
          return node.Id;
        }

        return -1;
      }
    }

    public int GetInputAiId
    {
      get
      {
        if (inputSocket01.IsConnected())
        {
          AbstracMonsterAINode node = (AbstracMonsterAINode)inputSocket01.GetConnectedSocket().Parent;
          return node.Id;
        }

        return -1;
      }
    }
  }
}


