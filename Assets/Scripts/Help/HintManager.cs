using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HintManager : MonoBehaviour
{
    [SerializeField] private List<Hint> hints;
    [SerializeField] private List<TextMeshProUGUI> textZone;
    private int currentZone;
    private Hint currentHint;

    public void GetCurrentHint(int sequence)
    {
        currentHint = hints.Find(hint => hint.GetIdSequence() == sequence);
    }

    public void ShowAHint()
    {
        if (currentZone < textZone.Count)
        {
            textZone[currentZone].text = currentHint.GetNextText();
            currentZone++;
        }
    }

    public void CleanHintZone()
    {
        foreach (TextMeshProUGUI textMeshProUGUI in textZone)
        {
            textMeshProUGUI.text = "";
        }
    }
}
