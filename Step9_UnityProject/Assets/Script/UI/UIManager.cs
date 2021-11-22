using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager GetInstance
    {
        get
        {
            return _instance;
        }
    }

    private GameObject SlidingPopup;

    private bool isOpenSlideWindow;
    public bool OpenSlide
    {
        set
        {
            isOpenSlideWindow = value;
        }
        get
        {
            return isOpenSlideWindow;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }

        SlidingPopup = GameObject.Find("Sliding Popup").transform.GetChild(0).gameObject;
    }
    void Start()
    {
        isOpenSlideWindow = false;
    }

    public void CtlrOtherWindows()
    {
        if(isOpenSlideWindow)
        {
            SlidingPopup.GetComponent<Slide_Image>().PopUpDraw();
            isOpenSlideWindow = true;
        }
    }
    void Update()
    {
    }
}
