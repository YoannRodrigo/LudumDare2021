using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Sequence6 : Sequence
{
    [Serializable]
    public class Step1 : Step
    {
        [SerializeField] private Button linkToClick;
        private bool hasClick;

        public override void Start()
        {
            linkToClick.onClick.AddListener(ValidateClick);
            base.Start();
        }

        private void ValidateClick()
        {
            if (isActive)
            {
                hasClick = true;
            }
        }
        
        public override void Update()
        {
            base.Update();
            if (hasClick)
            {
                linkToClick.onClick.RemoveListener(ValidateClick);
                ValidateStep();
            }
        }
    }
    
    
    [SerializeField] private Step1 step1;
    [SerializeField] private Step1 step2;
    [SerializeField] private List<GameObject> postToUnlock;
    protected override void Start()
    {
        steps.Add(step1);
        steps.Add(step2);
        base.Start();
    }
    
    protected override void ValidateSequence()
    {
        foreach (GameObject post in postToUnlock)
        {
            post.SetActive(true);
        }
        base.ValidateSequence();
    }
}
