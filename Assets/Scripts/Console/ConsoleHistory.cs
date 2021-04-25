using System.Collections;
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
        GameObject g = Instantiate(ConsoleLinePrefab, Vector3.zero,Quaternion.identity);
        g.GetComponent<TextMeshProUGUI>().text = this.NewLineChar + " " + _message;

        if(ContentRectHolder.rect.height <= (ConsoleLineHolder.childCount * g.GetComponent<RectTransform>().rect.height)){
            ContentRectHolder.sizeDelta = new Vector2(0, ContentRectHolder.rect.height + g.GetComponent<RectTransform>().rect.height);
            ConsoleScroll.verticalNormalizedPosition = -0.01f;
        }

        g.transform.SetParent(ConsoleLineHolder);
    }
}
