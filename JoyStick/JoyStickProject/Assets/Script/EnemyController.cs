using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject ParentObj;

    private List<GameObject> NodeList = new List<GameObject>();
    private Transform TargetNode = null;
    private int TargetNumber = 0;

    private void Awake()
    {
        ParentObj = GameObject.Find("Parent");

        Rigidbody Rigid = transform.GetComponent<Rigidbody>();
        Rigid.useGravity = false;

        BoxCollider Coll = transform.GetComponent<BoxCollider>();
        Coll.center = new Vector3(0.0f, 0.5f, 0.0f);
        Coll.isTrigger = true;
    }

    void Start()
    {
        TargetNumber = 0;

        GetNodes();
    }
    
    void Update()
    {
        Vector3 Direction = (TargetNode.transform.position - transform.position).normalized;

        transform.position += Direction * 2.5f * Time.deltaTime;
        
        transform.LookAt(TargetNode);

        Debug.DrawLine(this.transform.position, TargetNode.position, Color.red);
    }

    void GetNodes()
    {
        NodeList.Clear();

        for (int i = 0; i < ParentObj.transform.childCount; ++i)
            NodeList.Add(ParentObj.transform.GetChild(i).gameObject);

        TargetNode = NodeList[TargetNumber].transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == ("Node " + TargetNumber))
        {
            TargetNumber++;

            if (NodeList.Count <= TargetNumber)
            {
                if (NodeList.Count < ParentObj.transform.childCount)
                    GetNodes();

                TargetNumber = 0;
            }
            
            TargetNode = NodeList[TargetNumber].transform;
        }
    }
}
