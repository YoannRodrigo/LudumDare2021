using System;
using System.Collections.Generic;
using post;
using UnityEngine;

[Serializable]
public class Sequence2 : Sequence
{
   [Serializable]
    public class Step1 : Step
    {
       public GameObject popUp;

       public override void Start()
       {
          base.Start();
          popUp.SetActive(true);
       }

       public override void Update()
       {
          ValidateStep();
       }
    }
    
    [Serializable]
    public class Step2 : Step
    {
       public RectTransform footer;

       public override void Update()
       {
          if (footer.IsVisibleFrom(GameObject.FindWithTag("MainCamera").GetComponent<Camera>()))
          {
             ValidateStep();
          }
       }
    }
    
    [Serializable]
    public class Step3 : Step
    {
       public ConsoleController consoleController;

       public override void Update()
       {
          if (consoleController.ConsolePanel.activeSelf)
          {
             ValidateStep();
          }
       }
    }
    
    [Serializable]
    public class Step4 : Step
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
             foreach (Post post in postToUnlock)
             {
                post.ActivatePost();
             }
             consoleCommand.CallbackEvent.RemoveListener(CommandEnter);
             ValidateStep();
          }
       }
    }
    
    public Step1 step1;
    public Step2 step2;
    public Step3 step3;
    public Step4 step4;
    
    protected override void Start()
    {
       steps.Add(step1);
       steps.Add(step2);
       steps.Add(step3);
       steps.Add(step4);
       base.Start();
    }
}
