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
        isOpen = true;

        Window.SetActive(isOpen);
        WindowAni.SetBool("Open", isOpen);

        UIManager.GetInstance.CtlrOtherWindows();
    }

    public void ExitButton()
    {
        UIManager.GetInstance.CtlrOtherWindows();
        
        isOpen = false;

        WindowAni.SetBool("Open", isOpen);
        
        if(Window.GetComponent<RectTransform>().localScale.x == 0
            && Window.GetComponent<RectTransform>().localScale.y == 0)
        {
            Window.SetActive(isOpen);
        }
    }
}
