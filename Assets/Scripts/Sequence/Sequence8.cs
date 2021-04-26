using System;
using System.Collections.Generic;
using post;
using UnityEngine;
[Serializable]
public class Sequence8 : Sequence
{
    [Serializable]
    public class Step1 : Step
    {
        public ConsoleCommand consoleCommand;
        [SerializeField] private List<Post> postToUnlock;
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
    
    
}
