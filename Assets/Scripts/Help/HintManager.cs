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
    [SerializeField] private GameObject notif;
    
    private int currentZone;
    [SerializeField] private Hint currentHint;
    private bool isFull;

    public void GetCurrentHint(int sequence)
    {
        currentHint = hints.Find(hint => hint.GetIdSequence() == sequence);
    }

    public void ShowAHint()
    {
        if (currentZone < textZone.Count && currentHint)
        {
            string textToAdd = currentHint.GetNextText();
            SoundManager.PlaySFX("New_Message");
            notif.SetActive(true);
            textZone[currentZone].transform.parent.gameObject.SetActive(true);
            textZone[currentZone].text = textToAdd;
            currentZone++;
        }
    }

    public void CleanHintZone()
    {
        currentZone = 0;
        foreach (TextMeshProUGUI textMeshProUGUI in textZone)
        {
            textMeshProUGUI.transform.parent.gameObject.SetActive(false);
            textMeshProUGUI.text = "";
        }
    }

    private void Update()
    {
        if (isFull)
        {
            notif.SetActive(false);
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
