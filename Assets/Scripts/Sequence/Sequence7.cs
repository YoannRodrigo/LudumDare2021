using System;
using UnityEngine;

public class Sequence7 : Sequence
{
    [Serializable]
    public class Step1 : Step
    {
        public ConsoleCommand consoleCommand;
        private bool isCommandEnter;

        public override void Start()
        {
            base.Start();
            consoleCommand.CallbackEvent.AddListener(CommandEnter);
        }

        private void CommandEnter()
        {
            if (isActive)
            {
                isCommandEnter = true;
            }
        }
        public override void Update()
        {
            if (isCommandEnter)
            {
                consoleCommand.CallbackEvent.RemoveListener(CommandEnter);
                ValidateStep();
            }
        }
    }

    [SerializeField] private Step1 step;

    protected override void Start()
    {
        steps.Add(step);
        base.Start();
    }

    protected override void ValidateSequence()
    {
        base.ValidateSequence();
        SoundManager.StopMusic("Main_Theme");
        SoundManager.PlayMusic("End_Theme");
        FindObjectOfType<WebSiteManager>().ActivateWebSiteTab(4);
    }
}
