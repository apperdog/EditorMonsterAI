using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
  public MonsterAI monsterAI;

	// Use this for initialization
	void Start () {
    MonsterDataList dataList = DataSystem.GetSystem<MonsterDataList>();

    MonsterData monsterData = new MonsterData();
    monsterData.hp = 11;
    monsterData.mp = 120;
    monsterData.MaxHP = 120;
    monsterData.MaxMP = 120;

    dataList.SetData(monsterAI.monsterID, monsterData);

    monsterAI.StartAI();
	}
	
	// Update is called once per frame
	void Update () {
    monsterAI.monsterAI.OnUpdate();
	}
}
