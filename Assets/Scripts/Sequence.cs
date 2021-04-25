using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sequence : MonoBehaviour
{
    [Serializable]
    public class Step
    {
        protected bool isValidate;
        protected bool isActive;

        public virtual void Start()
        {
            isActive = true;
        }
        public virtual void Update() {}
        
        protected void ValidateStep()
        {
            isValidate = true;
        }

        public bool IsValidate()
        {
            return isValidate;
        }
    }

    protected List<Step> steps = new List<Step>();
    protected bool sequenceValidate;
    private Step currentStep;
    private int currentStepId;

    protected virtual void ValidateSequence()
    {
        sequenceValidate = true;
    }
    
    protected virtual void Start()
    {
        currentStep = steps[0];
        currentStep.Start();
    }
    
    protected virtual void Update()
    {
        if(!sequenceValidate)
        {
            currentStep.Update();
            if (currentStep.IsValidate())
            {
                currentStepId++;
                if (currentStepId < steps.Count)
                {
                    currentStep = steps[currentStepId];
                    currentStep.Start();
                }
                else
                {
                    ValidateSequence();
                }

            }
        }
    }
}