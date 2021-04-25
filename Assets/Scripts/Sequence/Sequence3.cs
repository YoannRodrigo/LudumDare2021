using System;

[Serializable]
public class Sequence3 : Sequence
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
   
    [Serializable]
    public class Step2 : Step
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
    
    public Step1 step1;
    public Step2 step2;
    
    protected override void Start()
    {
        steps.Add(step1);
        steps.Add(step2);
        base.Start();
    }
    
    protected override void ValidateSequence()
    {
        base.ValidateSequence();
        FindObjectOfType<WebSiteManager>().ActivateWebSiteTab(2);
    }
}
