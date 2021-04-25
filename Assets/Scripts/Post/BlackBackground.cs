using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackBackground : MonoBehaviour
{
    [SerializeField] GameObject _background;
    private Action callOnClose;

    private void Awake()
    {
        CloseBackground();
    }

    public void OpenBackground(Action callback = null)
    {
        callOnClose = callback;
        _background.SetActive(true);
    }
    public void CloseBackground()
    {
        _background.SetActive(false);
        callOnClose?.Invoke();
        callOnClose = null;
    }
}
