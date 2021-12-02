using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectKey
{
    Bullet
};

public class ObjectPool
{
    private static ObjectPool _instance = null;

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ObjectPool();

            return _instance;
        }
    }

    private Dictionary<ObjectKey, List<GameObject>> EnableList = new Dictionary<ObjectKey, List<GameObject>>();
    public Dictionary<ObjectKey, List<GameObject>> GetEnable
    {
        get
        {
            return EnableList;
        }
    }

    private Dictionary<ObjectKey, List<GameObject>> DisableList = new Dictionary<ObjectKey, List<GameObject>>();
    public Dictionary<ObjectKey, List<GameObject>> GetDisable
    {
        get
        {
            return DisableList;
        }
    }

    public void CreateObject(ObjectKey _ObjectKeyValue, GameObject _obj)
    {
        switch(_ObjectKeyValue)
        {
            case ObjectKey.Bullet:
                if (_obj.GetComponent<BulletControler>() == null)
                    _obj.AddComponent<BulletControler>();
                break;
        }

        _obj.transform.SetParent(GameObject.Find("Objectpool/DisableList").transform);
        _obj.GetComponent<Collider>().isTrigger = true;
        _obj.gameObject.SetActive(false);

        AddObject(false, _ObjectKeyValue, _obj);
    }

    public void AddObject(bool _Enable, ObjectKey _ObjectKeyValue, GameObject _obj)
    {
        if(_Enable)
        {
            if (EnableList.ContainsKey(_ObjectKeyValue))
            {
                EnableList[_ObjectKeyValue].Add(_obj);
            }
            else
            {
                EnableList.Add(_ObjectKeyValue, new List<GameObject>());
                EnableList[_ObjectKeyValue].Add(_obj);
            }
        }
        else
        {
            if (DisableList.ContainsKey(_ObjectKeyValue))
            {
                DisableList[_ObjectKeyValue].Add(_obj);
            }
            else
            {
                DisableList.Add(_ObjectKeyValue, new List<GameObject>());
                DisableList[_ObjectKeyValue].Add(_obj);
            }
        }
    }
}
