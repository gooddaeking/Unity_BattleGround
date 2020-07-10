using FC;
using NPOI.HPSF;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 숨을수있는 엄폐물이 없다면 그대로 서있는다.
/// 새로운 엄폐물이 있고 기존 엄폐물보다 가깝다면 새것으로 변경
/// 총알 장전도 여기서 한다
/// </summary>
[CreateAssetMenu(menuName ="PluggableAI/Actions/FindCover")]
public class FindCoverAction : Action
{
    public override void OnReadyAction(StateController controller)
    {
        controller.focusSight = false;
        controller.enemyAnimation.AbortPendingAim();
        controller.enemyAnimation.anim.SetBool(AnimatorKey.Crouch, false);
        ArrayList nextCoverData = controller.coverLookUp.GetBestCoverSpot(controller);
        Vector3 potentialCover = (Vector3)nextCoverData[1];
        if(Vector3.Equals(potentialCover, Vector3.positiveInfinity))
        {
            controller.nav.destination = controller.transform.position;
            return;
        }
        else if((controller.personalTarget - potentialCover).sqrMagnitude <
            (controller.personalTarget - controller.CoverSpot).sqrMagnitude &&
            !controller.IsNearOtherSpot(potentialCover, controller.nearRadius))
        {
            controller.coverHash = (int)nextCoverData[0];
            controller.CoverSpot = potentialCover;
        }
        controller.nav.destination = controller.CoverSpot;
        controller.nav.speed = controller.generalStats.evadeSpeed;
        controller.variables.currentShots = controller.variables.shotsInRounds;
    }
    public override void Act(StateController controller)
    {

    }
}
