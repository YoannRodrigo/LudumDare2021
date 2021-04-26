using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    [SerializeField] private string _songName;
    [SerializeField] private PlayerPopup _popup;

    public void OpenPopup()
    {
        _popup.OpenPopup(_songName);
    }
}
