﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// navMeshAgent에서 남은 거리가 아주 작거나
/// 경로를 검색중이 아니라면 true
/// </summary>
[CreateAssetMenu(menuName ="PluggableAI/Decisions/Reached Point")]
public class ReachedPointDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Application.isPlaying == false)
        {
            return false;
        }
        if(controller.nav.remainingDistance <= controller.nav.stoppingDistance && !controller.nav.pathPending)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
