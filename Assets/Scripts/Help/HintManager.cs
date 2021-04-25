using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HintManager : MonoBehaviour
{
    [SerializeField] private List<Hint> hints;
    [SerializeField] private List<TextMeshProUGUI> textZone;

    [SerializeField] private RectTransform hintZone;
    [SerializeField] private RectTransform fullTargetTransform;
    [SerializeField] private RectTransform reduceTargetTransform;
    
    private int currentZone;
    [SerializeField] private Hint currentHint;
    private bool isFull;

    public void GetCurrentHint(int sequence)
    {
        currentHint = hints.Find(hint => hint.GetIdSequence() == sequence);
    }

    public void ShowAHint()
    {
        if (currentZone < textZone.Count)
        {
            string textToAdd = currentHint.GetNextText();
            textZone[currentZone].transform.parent.gameObject.SetActive(true);
            textZone[currentZone].text = textToAdd;
            currentZone++;
        }
    }

    public void CleanHintZone()
    {
        foreach (TextMeshProUGUI textMeshProUGUI in textZone)
        {
            textMeshProUGUI.transform.parent.gameObject.SetActive(false);
            textMeshProUGUI.text = "";
        }
    }

    public void OnReduceButton()
    {
        isFull = !isFull;
        hintZone.anchoredPosition = isFull ? fullTargetTransform.anchoredPosition : reduceTargetTransform.anchoredPosition;
    }

    private void Start()
    {
        foreach (Hint hint in hints)
        {
            hint.ResetScriptable();
        }
    }
}
