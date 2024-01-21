using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GetStart);
    }

    void GetStart()
    {
        GameManager.Instance.SetIsEnterGameScene(true);
        button.interactable = false;
    }
}
