using System;
using UnityEngine;

namespace StateControl
{
  public interface IAIState<T>
  {
    /// <summary>
    /// 進入
    /// </summary>
    void Enter(T t);

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
    private IAIState<T> currestState;  // 當前
    [SerializeField]
    private IAIState<T> previousState;  // 上一個
    [SerializeField]
    private IAIState<T> allState;

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
    public void SetCurrestState(IAIState<T> state)
    {
      currestState = state;
      currestState.Enter(t);

      if (currestState != null)
        bcurrestState = true;
      else
        bcurrestState = false;
    }

    /// <summary>
    /// 設置全域狀況
    /// </summary>
    public void SetAllStata(IAIState<T> state)
    {
      allState = state;
      allState.Enter(t);

      if (allState != null)
        bAllState = true;
      else
        bAllState = false;
    }


    /// <summary>
    /// 狀態切換
    /// </summary>
    public void ChangeState(IAIState<T> state)
    {
      currestState.Exit();
      previousState = currestState;
      currestState = state;
      currestState.Enter(t);
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

