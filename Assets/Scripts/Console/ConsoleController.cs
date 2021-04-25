using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{

    public TMPro.TMP_InputField InputField;
    public ConsoleHistory ConsoleHistory;
    public GameObject ConsolePanel;

    protected virtual void Awake(){
        InputField.onSubmit.AddListener(PressSumbmit);
    }

    //Inputs

    public void OnConsole(){
        ConsolePanel.SetActive(!ConsolePanel.activeSelf);
        ConsolePanel.GetComponentInChildren<TMPro.TMP_InputField>().Select();

    }

    public void PressSumbmit(string _text){
        ConsoleHistory.AddMessage(_text);
    }
}
