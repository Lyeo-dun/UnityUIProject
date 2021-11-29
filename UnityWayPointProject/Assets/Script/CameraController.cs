using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //** ī�޶�
    public Camera MinimapCamera;

    //** ������� �ݰ�
    [SerializeField] [Range(0.01f, 0.1f)] float ShakeRadius;

    //** ����ð�
    [SerializeField] [Range(0.01f, 0.1f)] float ShakeTime;

    //** ����ٴ� Ÿ��
    public GameObject Target;

    //** �� �� �Ÿ�
    private float ZoomDistance;

    private void Awake()
    {
        //** ī�޶� �޾ƿ�.
        MinimapCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        //** ī�޶� �⺻ ��ġ�� ������.
        this.transform.position = new Vector3(0.0f, 45.0f, 0.0f);

        //** ȣ��ð��� ����.
        ShakeTime = 0.5f;

        //** ���� �ݰ��� ����.
        ShakeRadius = 0.1f;

        //** ī�޶�� Ÿ���ǰŸ�.
        ZoomDistance = 0.0f;

        //** ó�� �����Ҷ� �ѹ� �÷��̾� �ٶ󺸰���.
        //** ��� �ٶ󺸰� �ϸ� ī�޶� �¿� ������ �Ұ�.
        //this.transform.rotation = Quaternion.LookRotation(
             //Vector3.Normalize(Target.transform.position - this.transform.position));
    }
    private void Update()
    {
        //** AŰ�� �Էµȴٸ� ����ũ ����.
        if (Input.GetKeyDown(KeyCode.A))
            InvokeRepeating("StartShake", 0f, 0.01f);

        //** SŰ�� �Է� �Ǿ��ٸ� ����ũ ����.
        if (Input.GetKeyDown(KeyCode.S))
            Invoke("StopShake", 0.0f);

        //** ���콺 �� �Լ� ����.
        MouseWheel();

        //** ���� �̵� �Լ�����. (�¿� �̵�)
        CameraHorizontal();

        //** �� ���� ī�޶� ����.
        //** �� �Է� ���� ���ٸ� ZoomDistance ���� 0 �̹Ƿ�, �� ���� ����.
        MinimapCamera.fieldOfView = Mathf.Lerp(MinimapCamera.fieldOfView, ZoomDistance, Time.deltaTime * 4);

        //** Target�� �̵��� ���� ���� �̵���.
        MinimapCamera.transform.position = Target.transform.position - Vector3.forward + Vector3.up * 20.0f;
    }

    void MouseWheel()
    {
        //** �� ���� �޾ƿ�. ���� �ݴ�� �޾ƿ������� -1 �� ������
        float ScrollWheel = Input.GetAxis("Mouse ScrollWheel") * -1;

        //** �޾ƿ� ���� 10�� ���� �����ش�.
        //** �ٰ��� �޾ƿ��� ���� -1 ���� +1 ���̰��̶� ����Ǵ� ���� �۱⶧����...
        ZoomDistance += (ScrollWheel * 10);

        //** �� �ּҰ� ����.
        if (ZoomDistance < 20f)
            ZoomDistance = 20f;

        //** �� �ִ밪 ����.
        if (ZoomDistance > 60f)
            ZoomDistance = 60f;
    }

    void CameraHorizontal()
    {
        //** �� Ŭ��������
        if (Input.GetMouseButton(1))
        {
            //** ���� ȸ������ �޾ƿ�.
            Vector3 CurrentRotate = transform.rotation.eulerAngles;

            //** �޾ƿ� ȸ������ ���콺 �Է°��� ������.
            CurrentRotate.y += Input.GetAxis("Mouse X") * 5;

            //** ���ʹϾ����� ��ȯ.
            Quaternion CurrentQuaternion = Quaternion.Euler(CurrentRotate);

            //** ������� ���� ������ 0���� ����.
            CurrentQuaternion.z = 0;

            //** smooth �ϰ� ȸ����.
            transform.rotation = Quaternion.Slerp(
                 transform.rotation, CurrentQuaternion, 5 * Time.deltaTime);
        }
    }

    void StartShake()
    {
        //** Random.value = 0.0f �� ������ ������ 1.0f �� ������ �� ������ ������ ���� ��ȯ.
        Vector3 CameraPos = new Vector3(Random.value * ShakeRadius, Random.value * ShakeRadius, 0.0f);

        //** ���� ī�޶��� ��ġ�� ���� ���� ���� ����.
        Vector3 CurrentCameraPos = new Vector3(
                this.transform.position.x + CameraPos.x,
                this.transform.position.y + CameraPos.y,
                this.transform.position.z);

        //** ���� ī�޶� ��ġ�� ����.
        MinimapCamera.transform.position = CurrentCameraPos;
    }

    void StopShake()
    {
        //** ������ �Լ��� ���� �����.
        CancelInvoke("StartShake");

        //** ���Ⱑ ����Ǿ����� �����Ҷ� ī�޶��� ��ġ�� ����.
        MinimapCamera.transform.position = this.transform.position;
    }
}
