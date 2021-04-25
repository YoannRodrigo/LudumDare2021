using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HintManager hintManager;
    private int currentSequenceID = 1;
    private float timeSinceLastHint;

    [SerializeField] private List<Sequence> allSequence;
    private Sequence currentSequence;
    private const float TIME_BEFORE_NEXT_HINT = 1;

    private void Start()
    {
        hintManager.GetCurrentHint(currentSequenceID);
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

    private void OnNextSequence()
    {
        timeSinceLastHint = 0;
        currentSequenceID++;
        hintManager.CleanHintZone();
        hintManager.GetCurrentHint(currentSequenceID);
    }
}
