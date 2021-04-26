using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedUpdater : MonoBehaviour
{
    public List<post.PostData> Posts = new List<post.PostData>();
    public GameManager GameManager;

    public void Update(){
        this.GetComponent<post.GlitchPost>().UpdateGlitchedInfos(Posts[GameManager.GetCurrentSequenceID()]);
    }
}
