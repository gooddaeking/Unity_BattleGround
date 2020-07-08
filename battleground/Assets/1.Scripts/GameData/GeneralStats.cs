using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/GeneralStats")]
public class GeneralStats : ScriptableObject
{
    [Header("General")]
    [Tooltip("NPC 정찰 속도 clear state")]
    public float patrolSpeed = 2.0f;
    [Tooltip("npc 추격 속도 warning state")]
    public float chaseSpeed = 5.0f;
    [Tooltip("npc 회피 속도 engage state")]
    public float evadeSpeed = 15.0f;
    [Tooltip("웨이포인트에서 대기하는 시간")]
    public float patrolWaitTime = 2.0f;
    [Header("Animation")]
    [Tooltip("장애물 레이어 마스크")]
    public LayerMask obstacleMask;
    [Tooltip("조준시 깜빡임을 피하기 위한 최소 확정 앵글")]
    public float angleDeadZone = 5.0f;
    [Tooltip("속도 댐핑 시간")]
    public float speedDampTime = 0.4f;
    [Tooltip("각속도 댐핑 시간")]
    public float angularSpeedDampTime = 0.2f;
    [Tooltip("각속도안에서 각도 회전에 따른 댐핑 시간")]
    public float angleResponseTime = 0.2f;
    [Header("Cover")]
    [Tooltip("장애물에 숨을 때 고려할 최소 높이값")]
    public float aboveCoverHeight = 1.5f;
    [Tooltip("장애물 레이어 마스크")]
    public LayerMask coverMask;
    [Tooltip("사격 레이어 마스크")]
    public LayerMask shotMask;
    [Tooltip("타겟 레이어 마스크")]
    public LayerMask targetMask;

}
