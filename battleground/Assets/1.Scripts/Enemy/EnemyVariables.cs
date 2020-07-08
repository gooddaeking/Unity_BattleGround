using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyVariables
{
    // 사격을 위해 결정할 변수
    public bool feelAlert;                  // 위협을 느겼나?
    public bool hearAlert;                  // 소리를 들었나?

    // 커버를 위해 결정할 변수
    public bool advanceCoverDecision;       // 더 좋은 엄폐물이 있나?
    public int waitRounds;                  // 플레이어의 사격을 피하는 구간

    // 반복을 위해 결정할 변수
    public bool repeatShot;
    public float waitCoverTime;
    public float coverTime;

    // 정찰을 위해 결정할 변수
    public float patrolTimer;

    // 공격을 위해 결정할 변수
    public float shotTimer;
    public float startShootTimer;
    public float currentShots;
    public float shotsInRounds;
    public float blindEngageTimer;
}
