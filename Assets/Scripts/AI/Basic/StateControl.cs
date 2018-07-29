using System;
using UnityEngine;

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


  [Serializable]
  public class StateMachine<T>
  {
    [SerializeField]
    private T t;

    [SerializeField]
    private IAIState currestState;  // 當前
    [SerializeField]
    private IAIState previousState;  // 上一個
    [SerializeField]
    private IAIState allState;

    private bool bAllState;
    private bool bcurrestState;

    public StateMachine(T t)
    {
      this.t = t;

      bAllState = false;
      bcurrestState = false;

      allState = null;
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
    /// 設置全域狀況
    /// </summary>
    public void SetAllStata(IAIState state)
    {
      allState = state;
      allState.Enter();

      if (allState != null)
        bAllState = true;
      else
        bAllState = false;
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
        if (bAllState)
          allState.Execute();
        if (bcurrestState)
          currestState.Execute();
      }
      catch (Exception e)
      {
        Debug.Log(e.Message);
        throw;
      }
    }
  }
}

