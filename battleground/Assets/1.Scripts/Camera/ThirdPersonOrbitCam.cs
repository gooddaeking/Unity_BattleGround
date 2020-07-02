﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//카메라 속성 중 중요 속성 하나는 카메라로부터 위치 오프셋 벡터, 피봇 오프셋 벡터
//위치 오프셋 벡터는 충돌 처리용, 피봇 오프셋 벡터는 시선이동에 사용
//충돌체크 : 이중 충돌체크 기능 ( 캐릭터로부터 카메라, 카메라로부터 케릭터 사이 )
//사격 반동을 위한 기능
//FOV 변경 기능
[RequireComponent(typeof(Camera))]
public class ThirdPersonOrbitCam : MonoBehaviour
{
    public Transform player; // player transform
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);

    public float smooth = 10.0f;                // 카메라 반응속도
    public float horizontalAimingSpeed = 6.0f;  // 수평 회전 속도
    public float verticalAimingSpeed = 6.0f;    // 수직 회전 속도
    public float maxVerticalAngle = 30.0f;      // 수직 최대 각도
    public float minVerticalAngle = -60.0f;     // 수직 최소 각도
    public float recoilAngleBound = 5.0f;       // 사격 반동 바운스 값
    private float angleH = 0.0f;                // 마우스 이동에 따른 카메라 수평이동 수치
    private float angleV = 0.0f;                // 마우스 이동에 따른 카메라 수동이동 수치
    private Transform cameraTransform;          // 트랜스폼 캐싱
    private Camera myCamera;
    private Vector3 relCameraPos;               // 플레이어로부터 카메라까지의 벡터
    private float relCameraPosMag;              // 플레이어로부터 카메라사이의 거리
    private Vector3 smoothPivotOffset;          // 카메라 피봇 보간용 벡터
    private Vector3 smoothCamOffset;            // 카메라 위치 보간용 벡터
    private Vector3 targetPivotOffset;          // 카메라 피봇 보간용 벡터
    private Vector3 targetCamOffset;            // 카메라 위치 보간용 벡터
    private float defaultFOV;                   // 기본 시야값
    private float targetFOV;                    // 타겟 시야값
    private float targetMaxVerticalAngle;       // 카메라 수직 최대 각도
    private float recoilAngle = 0.0f;           // 사격 반동 각도

    public float GetH
    {
        get => angleH;
        //get
        //{
        //    return angleH;
        //}
    }

    private void Awake()
    {
        //캐싱
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();
        //카메라 기본 포지션 세팅
        cameraTransform.position = player.position + Quaternion.identity * pivotOffset
            + Quaternion.identity * camOffset;
        cameraTransform.rotation = Quaternion.identity;

        //카메라와 플레이건의 상대 벡터, 충돌체크를 위해 사용함
        relCameraPos = cameraTransform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        //기본 세팅
        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFOV = myCamera.fieldOfView;
        angleH = player.eulerAngles.y;

        ResetTargetOffsets();
        ResetFOV();
        ResetMaxVerticalAngle();
    }

    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetCamOffset = camOffset;
    }
    public void ResetFOV()
    {
        this.targetFOV = defaultFOV;
    }
    public void ResetMaxVerticalAngle()
    {
        targetMaxVerticalAngle = maxVerticalAngle;
    }
    public void BounceVertical(float degree)
    {
        recoilAngle = degree;
    }
    public void SetTargetOffset(Vector3 newPivotOffset, Vector3 newCamOffset)
    {
        targetPivotOffset = newPivotOffset;
        targetCamOffset = newCamOffset;
    }
    public void SetFOV(float customFOV)
    {
        this.targetFOV = customFOV;
    }

    bool ViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight)
    {
        Vector3 target = player.position + (Vector3.up * deltaPlayerHeight);
        if(Physics.SphereCast(checkPos, 0.2f, target - checkPos, out RaycastHit hit, relCameraPosMag))
        {
            if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }
    bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
    {
        Vector3 origin = player.position + (Vector3.up * deltaPlayerHeight);
        if(Physics.SphereCast(origin, 0.2f, checkPos - origin, out RaycastHit hit, maxDistance))
        {
            if(hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }
    bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
    {
        float playerFocusHeight = player.GetComponent<CapsuleCollider>().height * 0.75f;
        return ViewingPosCheck(checkPos, playerFocusHeight) && ReverseViewingPosCheck(checkPos, playerFocusHeight, offset);
    }

    private void Update()
    {
        // 마우스 이동 값
        angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f) * horizontalAimingSpeed;
        angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1.0f, 1.0f) * verticalAimingSpeed;
        // 수직 이동 제한
        angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);
        // 수직 카메라 바운스
        angleV = Mathf.LerpAngle(angleV, angleV + recoilAngle, 10.0f * Time.deltaTime);

        // 카메라 회전
        Quaternion camYRotation = Quaternion.Euler(0.0f, angleH, 0.0f);
        Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0.0f);
        cameraTransform.rotation = aimRotation;

        // set FOV
        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime);

        Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
        Vector3 noCollisionOffset = targetCamOffset; // 
    }

}
