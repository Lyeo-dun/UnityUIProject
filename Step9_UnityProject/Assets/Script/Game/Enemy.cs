using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State
    {
        Idle, Chasing, Attacking
    }
    State currentState = State.Chasing;

    NavMeshAgent pathfinder;
    Transform target;
    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 1.5f;// 공격을 하는 임계점
    //유니티는 미터단위
    float timeBetweenAttacks = 1;

    float nextAttackTime = 0; //다음 공격시간을 표시
    float myCollisionRadius;
    float targetCollisionRadius;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        //distance 메소드는 제곱근으로 계산하기 때문에 처리비용이 비싸다
        //실제 거리를 알 필요없이 값만 필요로 할때에는 제곱형태로 거리를 받아 제급근 연산을 사용하지 않을 수 있다.

        if(Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            //(목표지점 - 현재 자신의 지점)^2, 목표까지의 거리 제곱

            if(sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirTotarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirTotarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        while(percent <= 1)
        {            
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (((Mathf.Pow(percent, 2)) * -1) + percent) * 4;
            //보간값(interpolation), 알려진 점들의 위치를 참고하여 집합의 일정범위의 점들(선)을 새롭게 그리는 방식
            //이 값은 수식의 x가 될 예정
            // -> -4(x^2) + 4x
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            //두 벡터 사이에 비례값(0에서 1사이)으로 내분점을 반환

            yield return null;
            //이 지점에서 작업을 멈추고 update 메소드의 작업을 완전 수행한 후, 
            //다음 프레임으로 넘어갔을 때 yield키워드 아래에 있는 코드나 다음번 루프가 실행된다는 뜻
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 1;

        if (currentState == State.Chasing)
        {
            while (target != null)
            {
                Vector3 dirTotarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirTotarget * 
                    (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
