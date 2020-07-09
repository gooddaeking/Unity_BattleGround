using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

/// <summary>
/// 타겟이 멀리있고 엄폐물에서 최소 한타임 정도는 공격을 기다린 후에
/// 다음 엄폐물로 이동할지 판단
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Advance Cover")]
public class AdvanceCoverDecision : Decision
{
    public int waitRounds = 1;

    [Header("Extra Decision")]
    [Tooltip("플레이어가 가까이 있는지 판단")]
    public FocusDecision targetNear;

    public override void OnEnableDecision(StateController controller)
    {
        controller.variables.waitRounds += 1;
        //다음 장애물을 얻을지 말지 랜덤하게 판단
        controller.variables.advanceCoverDecision = Random.Range(0.0f, 1.0f) < controller.classStats.ChangeCoverChance / 100.0f;

    }
    public override bool Decide(StateController controller)
    {
        if(controller.variables.waitRounds <= waitRounds)
        {
            return false;
        }
        controller.variables.waitRounds = 0;
        return controller.variables.advanceCoverDecision && !targetNear.Decide(controller);
    }
}
