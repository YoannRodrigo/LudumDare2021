using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Sequence5 : Sequence
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

    protected override void Start()
    {
        steps.Add(step1);
        base.Start();
    }

    protected override void ValidateSequence()
    {
        base.ValidateSequence();
        FindObjectOfType<WebSiteManager>().ActivateWebSiteTab(3);
    }
}
