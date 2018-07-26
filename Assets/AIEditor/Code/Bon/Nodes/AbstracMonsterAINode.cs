
using System;
using UnityEngine;
using Assets.Code.Bon.Socket;
using Assets.Code.Bon.Interface;
using System.Collections.Generic;

namespace Assets.Code.Bon.Nodes
{
  public abstract class AbstracMonsterAINode : Node, IMonsterAINode
  {
    [NonSerialized] protected OutputSocket outSocket;
    [NonSerialized] protected InputSocket inputCondition;
    [NonSerialized] protected InputSocket inputEnterMethod, inputExcuseMethod, inputExitMethod;

    [SerializeField] public bool bFrist;
    [SerializeField] public string enter, excuse, exit;

    public AbstracMonsterAINode(int id, Graph parent) : base(id, parent)
    {
      bFrist = false;

      outSocket = new OutputSocket(this, typeof(AbstracMonsterAINode));
      inputEnterMethod = new InputSocket(this, typeof(AbstractStringNode));
      inputExcuseMethod = new InputSocket(this, typeof(AbstractStringNode));
      inputExitMethod = new InputSocket(this, typeof(AbstractStringNode));
      inputCondition = new InputSocket(this, typeof(AbstracStateConditionNode));
      Sockets.Add(outSocket);
      Sockets.Add(inputCondition);
      Sockets.Add(inputEnterMethod);
      Sockets.Add(inputExcuseMethod);
      Sockets.Add(inputExitMethod);
    }

    public string GetInputMethodString(InputSocket socket)
    {
      if (socket.IsConnected())
      {
        AbstractStringNode node = (AbstractStringNode)socket.GetConnectedSocket().Parent;
        return node.GetString(socket.GetConnectedSocket());
      }

      return string.Empty;
    }

    public List<int> GetConditionID()
    {
      List<int> input = new List<int>();

      if (outSocket.IsConnected())
      {
        // 遍尋所有連接的狀態條件
        for (int i = 0; i < outSocket.ConnectedCount; i++)
        {
          // 狀態條件
          AbstracStateConditionNode node = (AbstracStateConditionNode)outSocket.GetConnectedSocket(i).Parent;

          // 該狀態條件有下一個狀態
          if (node.outSocket.IsConnected())
            input.Add(node.Id);
        }
      }

      return input;
    }
  }
}

