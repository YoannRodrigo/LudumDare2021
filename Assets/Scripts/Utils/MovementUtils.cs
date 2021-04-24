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
}
