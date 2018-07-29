using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
  public MonsterAI monsterAI;

	// Use this for initialization
	void Start () {
    monsterAI.CreateAI();
	}
	
	// Update is called once per frame
	void Update () {
    monsterAI.monsterAI.OnUpdate();
	}
}
