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

    private GameObject ScrollViewUI;
    private bool isScrollViewUI;
    public bool OpenScroll
    {
        set
        {
            isScrollViewUI = value;
        }
        get
        {
            return isScrollViewUI;
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
        ScrollViewUI = GameObject.Find("ScrollUI");
    }
    void Start()
    {
        OpenSlide = false;
        OpenScroll = false;
    }

    public void CtlrOtherWindows()
    {
        if(OpenSlide)
        {
            SlidingPopup.GetComponent<Slide_Image>().PopUpDraw();
            OpenSlide = true;
        }

        if(OpenScroll)
        {
            ScrollViewUI.GetComponent<ScrollUICtrl>().ClickButton();
            OpenScroll = true;
        }
    }
}
