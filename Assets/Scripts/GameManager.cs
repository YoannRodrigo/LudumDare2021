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
    private const float TIME_BEFORE_FIRST_MESSAGE = 3;
    private const float TIME_BEFORE_NEXT_HINT = 100;
    private float nextTimer;

    private void Start()
    {
        hintManager.GetCurrentHint(currentSequenceID+1);
        currentSequence = allSequence[0];
        nextTimer = TIME_BEFORE_FIRST_MESSAGE;
    }

    private void Update()
    {
        timeSinceLastHint += Time.deltaTime;
        if (timeSinceLastHint > nextTimer)
        {
            ShowPlayerNeedAHint();
            nextTimer += TIME_BEFORE_NEXT_HINT;
        }
    }

    private void ShowPlayerNeedAHint()
    {
        hintManager.ShowAHint();
    }

    public void OnNextSequence()
    {
        nextTimer = TIME_BEFORE_FIRST_MESSAGE;
        timeSinceLastHint = 0;
        currentSequence.gameObject.SetActive(false);
        currentSequenceID++;
        if(currentSequenceID < allSequence.Count)
        {
            currentSequence = allSequence[currentSequenceID];
            currentSequence.gameObject.SetActive(true);
        }
        hintManager.CleanHintZone();
        hintManager.GetCurrentHint(currentSequenceID+1);
    }

    public int GetCurrentSequenceID(){
        return this.currentSequenceID;
    }
}
