using System;
using UnityEngine;
using System.Collections.Generic;

namespace StateControl
{
  public interface IAIState
  {
    /// <summary>
    /// 進入
    /// </summary>
    void Enter();

    /// <summary>
    /// 開始執行
    /// </summary>
    void Execute();

    /// <summary>
    ///  離開
    /// </summary>
    void Exit();
  }


  public class StateMachine
  {
    private IAIState currestState;  // 當前
    private IAIState previousState;  // 上一個

    private bool bcurrestState;

    public StateMachine()
    {
      bcurrestState = false;

      currestState = null;
      previousState = null;
    }

    /// <summary>
    /// 設置當前狀態
    /// </summary>
    public void SetCurrestState(IAIState state)
    {
      currestState = state;
      currestState.Enter();

      if (currestState != null)
        bcurrestState = true;
      else
        bcurrestState = false;
    }

    /// <summary>
    /// 狀態切換
    /// </summary>
    public void ChangeState(IAIState state)
    {
      currestState.Exit();
      previousState = currestState;
      currestState = state;
      currestState.Enter();
    }

    /// <summary>
    /// 執行當前狀態
    /// </summary>
    public void StateUpdate()
    {
      try
      {
        if (bcurrestState)
          currestState.Execute();
      }
      catch (Exception e)
      {
        Debug.Log(e.Message);
        throw;
      }
    }

    /// <summary>
    /// 當前的狀態
    /// </summary>
    public IAIState GetCurrestState { get { return currestState; } }
  }
}

