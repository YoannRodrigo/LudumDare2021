using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HintManager hintManager;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private int currentSequenceID;
    private float timeSinceLastHint;

    [SerializeField] private string introUrl;
    [SerializeField] private string outroUrl;
    

    [SerializeField] private List<Sequence> allSequence;
    private Sequence currentSequence;
    private const float TIME_BEFORE_FIRST_MESSAGE = 3;
    private const float TIME_BEFORE_NEXT_HINT = 100;
    private float nextTimer;
    private int isFirstFrame;
    private bool isVideoOver;
    private bool isWin;

    private void Start()
    {
        videoPlayer.url = introUrl;
        videoPlayer.Play();
        hintManager.GetCurrentHint(currentSequenceID+1);
        currentSequence = allSequence[0];
        nextTimer = TIME_BEFORE_FIRST_MESSAGE;
    }

    private void Update()
    {
        if(isFirstFrame > 5)
        {
            if (!videoPlayer.isPlaying && !isVideoOver)
            {
                isVideoOver = true;
            }
        }

        if (isVideoOver && !isWin)
        {
            videoPlayer.gameObject.SetActive(false);
            timeSinceLastHint += Time.deltaTime;
            if (timeSinceLastHint > nextTimer)
            {
                ShowPlayerNeedAHint();
                nextTimer += TIME_BEFORE_NEXT_HINT;
            }
        }
        
        isFirstFrame++;
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

    public void OnWin()
    {
        if(!isWin)
        {
            print("win");
            isWin = true;
            hintManager.enabled = false;
            videoPlayer.url = outroUrl;
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();
        }
    }
}
