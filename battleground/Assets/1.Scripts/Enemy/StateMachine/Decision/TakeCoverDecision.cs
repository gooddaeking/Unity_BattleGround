using NPOI.HPSF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엄폐물로 이동할 수 있는 상황인지 판단
/// 쏠수있는 총알이 남아있거나, 엄폐물로 이동하기 전에 대기시간이 남아있거나,
/// 숨을만한 엄폐물이 없을 경우는 false;
/// 그외에는 엄폐물로 이동한다. true;
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Take Cover")]
public class TakeCoverDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        // 쏠수있는 총알이 남아있거나, 대기시간이 더 필요하거나, 엄폐물을 못찾았다면 false
        if(controller.variables.currentShots < controller.variables.shotsInRounds ||
            controller.variables.waitInCoverTime > controller.variables.coverTime ||
            Equals(controller.CoverSpot, Vector3.positiveInfinity))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
