using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("카메라 이동 포인트")]
    public Transform PositionTop;
    public Transform PositionBottom;
    public Transform PositionLeft;
    public Transform PositionRight;
    public Transform PositionFront;
    public Transform PositionQuater;

    [Header("카메라 이동 속도")]
    public float CameraMoveSpeed;
    public float CameraRotateSpeed;
    public float GizmoMoveTime;

    bool isMoving;
    float lerpSpeed;

    Transform CurrCameraPosition;
    Transform DestCameraPosition;

    Vector2 CurrMousePosition;
    Vector2 MovedMousePosition;

    Vector3 CurrCameraRotation;
    Vector3 MovedCameraRotation;


    private void Awake()
    {
        isMoving = false;
        CurrCameraPosition = gameObject.transform;

        AutoCtrlCameraViewport();
    }

    private void Update()
    {
        if (isMoving) return;

        //마우스 처음 눌렀을때 위치 저장
        if (Input.GetMouseButtonDown(1))
        {
            CurrMousePosition = Input.mousePosition;
            CurrCameraRotation = CurrCameraPosition.localEulerAngles;
            CurrCameraRotation.z = 0f;
        }

        //오른쪽 클릭을 하고 있는 상태에서,
        if (Input.GetMouseButton(1))
        {
            MovedMousePosition = Input.mousePosition;
            MovedCameraRotation.x = CurrCameraRotation.x - (MovedMousePosition.y - CurrMousePosition.y) * CameraRotateSpeed;
            MovedCameraRotation.y = CurrCameraRotation.y + (MovedMousePosition.x - CurrMousePosition.x) * CameraRotateSpeed;
            MovedCameraRotation.z = 0f;

            CurrCameraPosition.localEulerAngles = MovedCameraRotation;


            //전방
            if (Input.GetKey(KeyCode.W))
            {
                CurrCameraPosition.localPosition += CurrCameraPosition.forward * CameraMoveSpeed;
            }
            //후방
            else if (Input.GetKey(KeyCode.S))
            {
                CurrCameraPosition.localPosition -= CurrCameraPosition.forward * CameraMoveSpeed;
            }

            //오른쪽
            if (Input.GetKey(KeyCode.D))
            {
                CurrCameraPosition.localPosition += CurrCameraPosition.right * CameraMoveSpeed;
            }
            //왼쪽
            else if (Input.GetKey(KeyCode.A))
            {
                CurrCameraPosition.localPosition -= CurrCameraPosition.right * CameraMoveSpeed;
            }

            //위
            if (Input.GetKey(KeyCode.Q))
            {
                CurrCameraPosition.localPosition += CurrCameraPosition.up * CameraMoveSpeed;
            }
            //아래
            else if (Input.GetKey(KeyCode.E))
            {
                CurrCameraPosition.localPosition -= CurrCameraPosition.up * CameraMoveSpeed;
            }
        }
    }

    void AutoCtrlCameraViewport()
    {
        float assetOptionPanelWidth = 360;

        float titlebarPanelHeight = 30;
        float contentBoxPanelHeight = 60;
        float timeLinePanelHeight = 340;

        float viewportX = assetOptionPanelWidth / Screen.width;
        float viewportY = timeLinePanelHeight / Screen.height;
        float viewportWidth = 1 - viewportX;
        float viewportHeight = 1 - (titlebarPanelHeight + contentBoxPanelHeight + timeLinePanelHeight) / Screen.height;

        Camera.main.rect = new Rect(viewportX, viewportY, viewportWidth, viewportHeight);
    }

    public void OnClick_MoveCamera(string dest)
    {
        if (isMoving) return;

        switch(dest)
        {
            case "TOP":
                DestCameraPosition = PositionTop;
                break;
            case "BOTTOM":
                DestCameraPosition = PositionBottom;
                break;
            case "LEFT":
                DestCameraPosition = PositionLeft;
                break;
            case "RIGHT":
                DestCameraPosition = PositionRight;
                break;
            case "FRONT":
                DestCameraPosition = PositionFront;
                break;
            case "QUATER":
                DestCameraPosition = PositionQuater;
                break;
            default:
                break;
        }

        StartCoroutine(MoveTransform());
    }

    IEnumerator MoveTransform()
    {
        isMoving = true;

        Vector3 currPos = CurrCameraPosition.localPosition;
        Vector3 currRot = CurrCameraPosition.localEulerAngles;

        float currMovingTime = 0;
        lerpSpeed = 0;

        while(true)
        {
            lerpSpeed += Time.deltaTime / GizmoMoveTime;
            currMovingTime += Time.deltaTime;

            CurrCameraPosition.localPosition = Vector3.Slerp(currPos, DestCameraPosition.localPosition, lerpSpeed);
            CurrCameraPosition.localEulerAngles = Vector3.Slerp(currRot, DestCameraPosition.localEulerAngles, lerpSpeed);

            if (currMovingTime > GizmoMoveTime)
            {
                CurrCameraPosition.localPosition = DestCameraPosition.localPosition;
                CurrCameraPosition.localEulerAngles = DestCameraPosition.localEulerAngles;

                isMoving = false;

                yield break;
            }

            yield return null;
        }
    }
}
