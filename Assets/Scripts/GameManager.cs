using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HintManager hintManager;
    private int currentSequence = 1;
    private float timeSinceLastHint;

    private const float TIME_BEFORE_NEXT_HINT = 1;

    private void Start()
    {
        hintManager.GetCurrentHint(currentSequence);
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
        currentSequence++;
        hintManager.CleanHintZone();
        hintManager.GetCurrentHint(currentSequence);
    }
}
