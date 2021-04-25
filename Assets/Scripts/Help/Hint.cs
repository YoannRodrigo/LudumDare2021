using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hint", fileName = "Hint")]
public class Hint : ScriptableObject
{
    [SerializeField] private int idSequence;
    [SerializeField] private int websiteID;
    [SerializeField] private List<string> hintsTexts;
    private int currentTextId;
    
    public int GetIdSequence()
    {
        return idSequence;
    }

    public int GetTextCount()
    {
        return hintsTexts.Count;
    }

    public string GetNextText()
    {
        string toReturn = null;
        if (currentTextId < GetTextCount())
        {
            toReturn = hintsTexts[currentTextId];
            currentTextId++;
        }

        return toReturn;
    }
}
