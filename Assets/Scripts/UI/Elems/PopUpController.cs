using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{

    public float MovementValue;
    public float MovementDuration;

    public void Awake(){
        MovementUtils.MoveY(this.gameObject, this.MovementValue, this.MovementDuration);
    }
}
