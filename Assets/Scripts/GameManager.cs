using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HintManager hintManager;
    [SerializeField] private int currentSequenceID;
    private float timeSinceLastHint;

    [SerializeField] private List<Sequence> allSequence;
    private Sequence currentSequence;
    private const float TIME_BEFORE_NEXT_HINT = 1;

    private void Start()
    {
        hintManager.GetCurrentHint(currentSequenceID+1);
        currentSequence = allSequence[0];
        //currentSequence.OnSequenceEnd().AddListener(OnNextSequence);
    }

    private void Update()
    {
        timeSinceLastHint += Time.deltaTime;
        if (timeSinceLastHint > TIME_BEFORE_NEXT_HINT)
        {
            timeSinceLastHint = 0;
            ShowPlayerNeedAHint();
        }
    }

    private void ShowPlayerNeedAHint()
    {
        hintManager.ShowAHint();
    }

    public void OnNextSequence()
    {
        timeSinceLastHint = 0;
        currentSequence.gameObject.SetActive(false);
        currentSequenceID++;
        if(currentSequenceID < allSequence.Count)
        {
            currentSequence = allSequence[currentSequenceID];
            currentSequence.gameObject.SetActive(true);
        }
        hintManager.CleanHintZone();
        hintManager.GetCurrentHint(currentSequenceID);
    }
}
