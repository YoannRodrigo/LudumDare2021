using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{

    [HideInInspector]
    public float Length, StartPos;
    [Range(0f, 0.9f)]
    public float ParallaxFactor;
    public GameObject Camera;
    public float PixelsPerUnit;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.StartPos = transform.position.x;
        this.Length = this.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    protected virtual void FixedUpdate(){
        float dt = Time.deltaTime;

        float tmpDist = this.Camera.transform.position.x * (1 - this.ParallaxFactor);
        float distance = this.Camera.transform.position.x * this.ParallaxFactor;

        Vector3 newPos = new Vector3(this.StartPos + distance, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;

        if(tmpDist > this.StartPos + (this.Length /2)){
            this.StartPos += this.Length;
        }else if(tmpDist < this.StartPos - (this.Length / 2)){
            this.StartPos -= this.Length;
        }
    }

    private Vector3 PixelPerfectClamp(Vector3 _locationVector, float _pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(_locationVector.x * _pixelsPerUnit), Mathf.CeilToInt(_locationVector.y * _pixelsPerUnit), Mathf.CeilToInt(_locationVector.z * _pixelsPerUnit));
        return vectorInPixels / _pixelsPerUnit;
    }
}
