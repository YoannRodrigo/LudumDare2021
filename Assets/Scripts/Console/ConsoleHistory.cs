using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleHistory : MonoBehaviour
{

    public string WelcomeMessage;
    public string NewLineChar;
    public List<string> History = new List<string>();
    public GameObject ConsoleLinePrefab;
    public Transform ConsoleLineHolder;

    public void AddMessage(string _message){
        GameObject g = Instantiate(ConsoleLinePrefab, Vector3.zero,Quaternion.identity);
        g.GetComponent<TextMeshProUGUI>().text = this.NewLineChar + " " + _message;
        g.transform.SetParent(ConsoleLineHolder);
    }
}
