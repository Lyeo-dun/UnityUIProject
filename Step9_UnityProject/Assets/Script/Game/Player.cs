using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))] 
//현재의 스크립트를 다른 오브젝트에 추가할 때 PlayerController 또한
//같이 있기를 강조, 강요함(에러 방지용)
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    public float moveSpeed = 5;

    Camera viewCamera;
    PlayerController controller;

    GunController gunController;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        //스크립트가 같은 오브젝트에 붙은 것을 전제로 함
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        //이동 입력
        Vector3 moveInput = 
            new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //GetAxis는 smoothing이 적용된 상태, GetAxisRaw는 방향만 가져온다
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;//방향 * 속도
        controller.Move(moveVelocity);

        //바라보는 방향
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //파라미터에 해당하는 것의 화면상 위치를 반환
        //레이는 무한정으로 뻗어나감
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //플레인을 사용하여 레이가 무한정 뻗어나가는 것을 막음
        //하늘을 바라보는 평면의 바닥
        float rayDistance = 0;

        if(groundPlane.Raycast(ray, out rayDistance)) //값이 true이면 현재 쏜 ray와 바닥이 부딪힌 것
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //현재 rayDistance의 값은 Raycast 메소드를 통해 카메라에서 ray가 부딪힌 지점까지의 거리가 저장됨
            //결론적으로 point에는 plane을 통해 만들어낸 바닥 상의 마우스의 위치가 반환됨

            //Debug.DrawLine(ray.origin, point, Color.red);
            //제대로 되고 있는지 시각적으로 보기 위해 레이를 쏜 시점부터 마우스의 위치까지 붉은 선을 그린다.
            //여기가 플레이어가 바라봐야할 교차지점
            controller.LookAt(point);
        }

        //무기조작 입력
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
