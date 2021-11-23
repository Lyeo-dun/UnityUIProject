using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_UI : MonoBehaviour
{
    private GameObject Window;
    private bool isOpen;
    private Animator WindowAni;

    private void Awake()
    {
        Window = transform.GetChild(0).gameObject;
        WindowAni = GetComponent<Animator>();
    }
    void Start()
    {
        isOpen = false;
        Window.SetActive(isOpen);
    }

    public void ClickButton()
    {
        if(!isOpen)
        {
            isOpen = true;

            Window.SetActive(isOpen);
            WindowAni.SetBool("Open", isOpen);

            UIManager.GetInstance.CtlrOtherWindows();
        }
    }

    public void ExitButton()
    {   
        if(isOpen)
        {
            isOpen = false;

            WindowAni.SetBool("Open", isOpen);
        }
    }

    void ExitWindow()
    {
        UIManager.GetInstance.CtlrOtherWindows();
        Window.SetActive(isOpen);
    }
}
