using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class MovementUtils
{
    public static void MoveY(GameObject target, float value, float duration, Action callback = null){

        target.transform.DOMoveY(target.transform.position.y + value, duration).SetEase(Ease.OutCubic).OnComplete(()=>{
            callback?.Invoke();
        });
    }

    public static void MoveX(GameObject target, float value, float duration, Action callback = null){

        target.transform.DOMoveX(target.transform.position.x + value, duration).SetEase(Ease.OutCubic).OnComplete(()=>{
            callback?.Invoke();
        });
    }
    
    public static void MoveY(RectTransform target, float value, float duration, Action callback = null){

        target.DOAnchorPosY(target.transform.position.y + value, duration).SetEase(Ease.OutCubic).OnComplete(()=>{
            callback?.Invoke();
        });
    }
    
    public static void MoveX(RectTransform target, float value, float duration, Action callback = null){

        target.DOAnchorPosX(target.transform.position.x + value, duration).SetEase(Ease.OutCubic).OnComplete(()=>{
            callback?.Invoke();
        });
    }
}
