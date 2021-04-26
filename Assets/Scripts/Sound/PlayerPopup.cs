using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerPopup : MonoBehaviour
{

    [SerializeField] private Vector2 _displacementAmount;
    [SerializeField] private float _movementDuration;
    [SerializeField] private SoundManager _soundManager;
    private Vector3 basePos;
    private bool _isOpen = false;
    private string _musicLoaded;
    private bool _isPlaying;

    private void Awake()
    {
        basePos = this.GetComponent<RectTransform>().anchoredPosition;
    }
    public void OpenPopup(string songToPlay)
    {
        if (_isOpen)
        {
            return;
        }
        _isOpen = true;
        GetComponent<RectTransform>().DOAnchorPosY(basePos.y + _displacementAmount.y, 1).SetEase(Ease.OutCubic);
        _musicLoaded = songToPlay;
        print(_musicLoaded);
    }

    public void ClickOnClose()
    {
        _isOpen = false;
        GetComponent<RectTransform>().DOAnchorPosY(basePos.y, 1).SetEase(Ease.OutCubic).OnComplete(() => _isOpen = false);
        if (_isPlaying)
        {
            SoundManager.StopSFX(_musicLoaded);
            _isPlaying = false;
        }
    }
    public void PlayMusic()
    {
        if (_isPlaying)
        {
            SoundManager.StopSFX(_musicLoaded);
            _isPlaying = false;
        }
        else
        {
            print(_musicLoaded);
            SoundManager.PlaySFX(_musicLoaded);
            _isPlaying = true;
        }
    }
}
