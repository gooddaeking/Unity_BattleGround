using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// state -> actions update -> transition (decision) check
/// state에 필요한 기능들. 애니메이션 콜백.
/// 시야 체크, 찾아놓은 엄폐물 장소 중 가장 가까운 위치를 찾는 기능.
/// </summary>
public class StateController : MonoBehaviour
{
    public GeneralStats generalStats;
    public ClassStats statsData;
    public string classID;              // pistol, rifle, ak,

    public ClassStats.Param classStats
    {
        get
        {
            foreach(ClassStats.Sheet sheet in statsData.sheets)
            {
                foreach(ClassStats.Param param in sheet.list)
                {
                    if(param.ID.Equals(this.classID))
                    {
                        return param;
                    }
                }
            }
            return null;
        }
    }

    public State currentState;
    public State remainState;

    public Transform aimTarget;

    public List<Transform> patrolWaypoints;

    public int bullets;
    [Range(0,50)]
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    [Range(0,25)]
    public float perceptionRadius;

    [HideInInspector]public float nearRadius;
    [HideInInspector]public NavMeshAgent nav;
    [HideInInspector]public int wayPointIndex;
    [HideInInspector]public int maximumBurst = 7;
    [HideInInspector]public float BlindEngageTime = 10f;
    [HideInInspector]public bool targetInSight;             // 플레이어가 시야에 있나?
    [HideInInspector]public bool focusSight;
    [HideInInspector]public bool reloading;
    [HideInInspector]public bool hadClearShot;              // before
    [HideInInspector]public bool haveClearShot;             // now
    [HideInInspector]public int coverHash = -1;             // 장애물을 쓰고있으면 다른곳으로 가도록
    [HideInInspector]public EnemyVariables variables;
    [HideInInspector]public Vector3 personalTarget = Vector3.zero;

    private int magBullets;                                 // 잔탄량
    private bool aiActive;                                  // 에너미가 살아있나?
    private static Dictionary<int, Vector3> coverSpot;      // static <해시코드, 위치>
    private bool strafing;                                  // 플레이어가 움직이면서 쏘는 중인가?
    private bool aiming;                                    // 조준 중인가?
    private bool checkedOnLoop, blockedSight;               // 시야가 막혀있나? 막혀있다면 특정행동을 루프

    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public CoverLookUp coverLookUp;

    public Vector3 CoverSpot
    {
        get { return coverSpot[this.GetHashCode()]; }
        set { coverSpot[this.GetHashCode()] = value; }
    }
    public void TransitionToState(State nextState, Decision decision)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
        }
    }

    public bool Strafing
    {
        get => strafing;
        set
        {
            enemyAnimation.anim.SetBool("Strafe", value);
            strafing = value;
        }
    }
    public bool Aiming
    {
        get => aiming;
        set
        {
            if(aiming != value)
            {
                enemyAnimation.anim.SetBool("Aim", value);
                aiming = value;
            }
        }
    }

    public IEnumerator UnstuckAim(float delay)
    {
        yield return new WaitForSeconds(delay * 0.5f);
        Aiming = false;
        yield return new WaitForSeconds(delay * 0.5f);
        Aiming = true;
    }
}
