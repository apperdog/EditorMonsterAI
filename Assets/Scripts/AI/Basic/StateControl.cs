using System;
using UnityEngine;
using System.Collections.Generic;

namespace StateControl
{
  public interface IAIState<T>
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

    /// <summary>
    /// 條件檢查
    /// </summary>
    T CheckCondition();

    /// <summary>
    /// 取得條件
    /// </summary>
    List<IStateCondition> GetStateCondition { get; }
  }


  [Serializable]
  public class StateMachine<T, T2>
  {
    [SerializeField]
    private T t;

    [SerializeField]
    private IAIState<T2> currestState;  // 當前
    [SerializeField]
    private IAIState<T2> previousState;  // 上一個

    private bool bcurrestState;

    public StateMachine(T t)
    {
      this.t = t;

      bcurrestState = false;

      currestState = null;
      previousState = null;
    }

    /// <summary>
    /// 設置當前狀態
    /// </summary>
    public void SetCurrestState(IAIState<T2> state)
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
    public void ChangeState(IAIState<T2> state)
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

    public int CheckCondition()
    {
      List<IStateCondition> stateConditions = currestState.GetStateCondition;

      for(int i = 0; i < stateConditions.Count; i++)
      {
        // 取得切換狀態
        int change = stateConditions[i].CheckCondition();

        // 如果有切換狀態名稱
        if (change > -1)
          return change;
      }

      return -1;
    }
  }
}

