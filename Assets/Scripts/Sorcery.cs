using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Sorcery : MonoBehaviour
{

    public GameObject PostProcess;

    private Volume pp;
    private Vignette vignette;

    public void Start(){
        pp = PostProcess.GetComponent<Volume>();
    }

    public void Light(){
        if (PostProcess.GetComponent<Volume>().profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0;
        }
    }

    public void TimeSpellMin(){
        Time.timeScale = .1f;
    }

    public void TimeSpell(){
        Time.timeScale = 1;
    }
}
