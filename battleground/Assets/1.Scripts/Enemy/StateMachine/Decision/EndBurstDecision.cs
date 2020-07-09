using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 한번 사격이 시작되면 재장전까지 쏠 수 있는 총알의 수를 판단.
/// </summary>
[CreateAssetMenu(menuName ="PluggableAI/Decisions/End Burst")]
public class EndBurstDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        // 사격을 가진 총알보다 많이했으면 true - 재장전 (Wait)
        return controller.variables.currentShots >= controller.variables.shotsInRounds;
    }
}
