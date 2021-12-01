using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickManager : MonoBehaviour
{
    static JoyStickManager _instance;
    static public JoyStickManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private JoyStickManager()
    { }

    public Vector3 MoveDirection = Vector3.zero;
    public float MoveValue = 0;

    public Vector3 ShootingDirection = Vector3.zero;
    public float ShootingValue = 0;

    public GameObject Player;

    private void Awake()
    {
        if (_instance == null)
            _instance = new JoyStickManager();
        else
        {
            if(_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void PlayerAimming(bool _value, Vector3 Dir)
    {
        if (Player)
            Player.GetComponent<PlayerController>().SetPlayerAim(_value, Dir);
    }
}
