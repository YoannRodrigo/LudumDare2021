using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleHistory : MonoBehaviour
{

    public string WelcomeMessage;
    public string NewLineChar;
    public List<string> History = new List<string>();
    public GameObject ConsoleLinePrefab;
    public Transform ConsoleLineHolder;
    public RectTransform ContentRectHolder;
    public ScrollRect ConsoleScroll;

    public void AddMessage(string _message){
        GameObject g = Instantiate(ConsoleLinePrefab, Vector3.zero,Quaternion.identity,ConsoleLineHolder);
        g.GetComponent<TextMeshProUGUI>().text = this.GetStringTime() + " " + this.NewLineChar + " " + _message;
        g.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Scale(g.GetComponent<RectTransform>().anchoredPosition3D, new Vector3(1, 1, 0));
        if(ContentRectHolder.rect.height <= (ConsoleLineHolder.childCount * g.GetComponent<RectTransform>().rect.height))
        {
            ContentRectHolder.sizeDelta = new Vector2(0, ContentRectHolder.rect.height + g.GetComponent<RectTransform>().rect.height);
            ConsoleScroll.verticalNormalizedPosition = 0;
        }

    }

    private string GetStringTime(){
        return "[" + DateTime.Now.ToString("HH':'mm':'ss.fff") + "]";
    }
}
