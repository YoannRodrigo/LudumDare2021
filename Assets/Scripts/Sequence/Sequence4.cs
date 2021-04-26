using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Sequence4 : Sequence
{
    [Serializable]
    public class StepSimon : Step
    {
        [SerializeField] private List<Button> buttonSequences;
        [SerializeField] private List<Button> otherButtons;
        [SerializeField] private List<bool> buttonPressed = new List<bool>();
        private List<UnityAction> delegates = new List<UnityAction>();
        private int lastIdBool;
        private bool buttonCanUpdate;
        public override void Start()
        {
            base.Start();
            for(int i = 0; i < buttonSequences.Count; i++)
            {
                buttonPressed.Add(false);
                int i1 = i;
                void Func()
                {
                    ActivateButton(i1);
                }
                delegates.Add(Func);
                buttonSequences[i].onClick.AddListener(Func);
            }
            foreach (Button button in otherButtons)
            {
                button.onClick.AddListener(FailSequence);
            }
        }

        private void ActivateButton(int i)
        {
            if(buttonCanUpdate)
            {
                if (buttonPressed[i])
                {
                    FailSequence();
                }
                else
                {
                    buttonPressed[i] = true;
                }
            }
        }

        private void FailSequence()
        {
            lastIdBool = 0;
            for(int i = 0; i < buttonPressed.Count; i++)
            {
                buttonPressed[i] = false;
            }
        }

        private bool ValidateSequence()
        {
            int nbPressed = buttonPressed.Count(press => press);
            int lastId = 0;
            
            
            if (nbPressed == buttonPressed.Count)
            {
                return true;
            }

            for (int i = 0; i < buttonPressed.Count; i++)
            {
                if (buttonPressed[i])
                {
                    lastId = i;
                }
            }

            if (lastId - lastIdBool > 1)
            {
                FailSequence();
            }
            else
            {
                lastIdBool = lastId;
            }

            return false;

        }

        public override void Update()
        {
            base.Update();
            buttonCanUpdate = true;
            if (ValidateSequence())
            {
                Debug.Log("Validate Simon " + buttonSequences.Count);
                ValidateStep();
                for(int i = 0; i < buttonSequences.Count; i++)
                {
                    int i1 = i;
                    buttonSequences[i].onClick.RemoveListener(delegates[i]);
                }
                foreach (Button button in otherButtons)
                {
                    button.onClick.RemoveListener(FailSequence);
                }
            }
        }
    }
   
    
    public StepSimon step1;
    public StepSimon step2;
    public StepSimon step3;
    public StepSimon step4;
    public StepSimon step5;
    public StepSimon step6;
    [SerializeField] private List<GameObject> postToUnlock;

    protected override void Start()
    {
        steps.Add(step1);
        steps.Add(step2);
        steps.Add(step3);
        steps.Add(step4);
        steps.Add(step5);
        steps.Add(step6);
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
