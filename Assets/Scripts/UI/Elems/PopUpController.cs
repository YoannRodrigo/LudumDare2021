using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{

    public float MovementValue;
    public float MovementDuration;
    
    private Vector3 basePos;



    public void OnEnable(){

        basePos = this.transform.position;  

        MovementUtils.MoveY(this.gameObject, this.MovementValue, this.MovementDuration);
    }

    public void ClickOnClose(){
        MovementUtils.MoveY(this.gameObject, -this.MovementValue, this.MovementDuration / 1.5f, Deactivate);
    }

    public void Deactivate(){
        this.gameObject.SetActive(false);
        this.transform.position = basePos;
    }
}
