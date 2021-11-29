using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //** 카메라
    public Camera MinimapCamera;

    //** 흔들어야할 반경
    [SerializeField] [Range(0.01f, 0.1f)] float ShakeRadius;

    //** 종료시간
    [SerializeField] [Range(0.01f, 0.1f)] float ShakeTime;

    //** 따라다닐 타겟
    public GameObject Target;

    //** 줌 될 거리
    private float ZoomDistance;

    private void Awake()
    {
        //** 카메라를 받아옴.
        MinimapCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        //** 카메라 기본 위치를 지정함.
        this.transform.position = new Vector3(0.0f, 45.0f, 0.0f);

        //** 호출시간을 셋팅.
        ShakeTime = 0.5f;

        //** 흔들기 반경을 셋팅.
        ShakeRadius = 0.1f;

        //** 카메라와 타겟의거리.
        ZoomDistance = 0.0f;

        //** 처음 시작할때 한번 플레이어 바라보게함.
        //** 계속 바라보게 하면 카메라 좌우 움직임 불가.
        //this.transform.rotation = Quaternion.LookRotation(
             //Vector3.Normalize(Target.transform.position - this.transform.position));
    }
    private void Update()
    {
        //** A키가 입력된다면 쉐이크 실행.
        if (Input.GetKeyDown(KeyCode.A))
            InvokeRepeating("StartShake", 0f, 0.01f);

        //** S키가 입력 되었다면 쉐이크 종료.
        if (Input.GetKeyDown(KeyCode.S))
            Invoke("StopShake", 0.0f);

        //** 마우스 휠 함수 실행.
        MouseWheel();

        //** 수평 이동 함수실행. (좌우 이동)
        CameraHorizontal();

        //** 줌 값을 카메라에 적용.
        //** 줌 입력 값이 없다면 ZoomDistance 값도 0 이므로, 줌 하지 않음.
        MinimapCamera.fieldOfView = Mathf.Lerp(MinimapCamera.fieldOfView, ZoomDistance, Time.deltaTime * 4);

        //** Target의 이동에 맞춰 따라 이동함.
        MinimapCamera.transform.position = Target.transform.position - Vector3.forward + Vector3.up * 20.0f;
    }

    void MouseWheel()
    {
        //** 휠 값을 받아옴. 값을 반대로 받아오기위해 -1 을 곱해줌
        float ScrollWheel = Input.GetAxis("Mouse ScrollWheel") * -1;

        //** 받아온 값에 10의 값을 곱해준다.
        //** 휠값만 받아오면 값이 -1 부터 +1 사이값이라 적용되는 값이 작기때문에...
        ZoomDistance += (ScrollWheel * 10);

        //** 줌 최소값 셋팅.
        if (ZoomDistance < 20f)
            ZoomDistance = 20f;

        //** 줌 최대값 셋팅.
        if (ZoomDistance > 60f)
            ZoomDistance = 60f;
    }

    void CameraHorizontal()
    {
        //** 우 클릭했을때
        if (Input.GetMouseButton(1))
        {
            //** 현제 회전값을 받아옴.
            Vector3 CurrentRotate = transform.rotation.eulerAngles;

            //** 받아온 회전값에 마우스 입력값을 더해줌.
            CurrentRotate.y += Input.GetAxis("Mouse X") * 5;

            //** 쿼터니언으로 변환.
            Quaternion CurrentQuaternion = Quaternion.Euler(CurrentRotate);

            //** 사용하지 않을 각도는 0으로 셋팅.
            CurrentQuaternion.z = 0;

            //** smooth 하게 회전함.
            transform.rotation = Quaternion.Slerp(
                 transform.rotation, CurrentQuaternion, 5 * Time.deltaTime);
        }
    }

    void StartShake()
    {
        //** Random.value = 0.0f 를 포함한 값부터 1.0f 를 포함한 값 까지의 임의의 수를 반환.
        Vector3 CameraPos = new Vector3(Random.value * ShakeRadius, Random.value * ShakeRadius, 0.0f);

        //** 현재 카메라의 위치를 흔들기 위해 값을 셋팅.
        Vector3 CurrentCameraPos = new Vector3(
                this.transform.position.x + CameraPos.x,
                this.transform.position.y + CameraPos.y,
                this.transform.position.z);

        //** 실제 카메라 위치에 셋팅.
        MinimapCamera.transform.position = CurrentCameraPos;
    }

    void StopShake()
    {
        //** 실중인 함수의 값을 취소함.
        CancelInvoke("StartShake");

        //** 흔들기가 종료되었을때 시작할때 카메라의 위치로 셋팅.
        MinimapCamera.transform.position = this.transform.position;
    }
}
