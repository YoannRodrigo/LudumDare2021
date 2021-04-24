using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject Layers;

    protected virtual void Awake(){
        foreach (Transform layer in Layers.transform)
        {
            this.TripleLayer(layer.gameObject);
        }
    }

    private void TripleLayer(GameObject _layer){

        float length = _layer.GetComponent<SpriteRenderer>().bounds.size.x;

        //left
        GameObject left = Instantiate(_layer, new Vector3(-length, 0,0), Quaternion.identity);
        //right
        GameObject right = Instantiate(_layer, new Vector3(length, 0,0), Quaternion.identity);

        left.GetComponent<ParallaxLayer>().enabled = false;
        right.GetComponent<ParallaxLayer>().enabled = false;

        left.transform.SetParent(_layer.transform);
        right.transform.SetParent(_layer.transform);
    }
}
