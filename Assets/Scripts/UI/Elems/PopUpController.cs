using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{

    public float MovementValue;
    public float MovementDuration;
    public Vector2 TopBotXRange;
    
    private Vector3 basePos;



    public void OnEnable(){

        basePos = this.GetComponent<RectTransform>().anchoredPosition;  

        MovementUtils.MoveY(this.gameObject.GetComponent<RectTransform>(), this.MovementValue, this.MovementDuration);
    }

    public void ClickOnClose(){
        MovementUtils.MoveY(this.gameObject.GetComponent<RectTransform>(), -this.MovementValue, this.MovementDuration / 1.5f, Relocate);
    }

    public void Deactivate(){
        this.gameObject.SetActive(false);
        
    }

    public void Relocate(){
        this.Deactivate();
        this.ApplyRandomX();
        this.gameObject.SetActive(true);
    }

    public void ApplyRandomX(){
        Vector3 newPos = new Vector3(Random.Range(this.TopBotXRange.x, this.TopBotXRange.y), this.basePos.y, this.basePos.z);
        this.GetComponent<RectTransform>().anchoredPosition = newPos;
    }
}
