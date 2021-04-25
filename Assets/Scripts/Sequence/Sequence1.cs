using System;
using post;

[Serializable]
public class Sequence1 : Sequence
{
   [Serializable]
   public class Step1 : Step
   {
      public Post modelPost;

      public override void Update()
      {
         if (modelPost.IsModelOpen())
         {
            ValidateStep();
         }
      }
   }
   
   [Serializable]
   public class Step2 : Step
   {
      public Post modelPost;
      private bool isHourSeen;
      private bool isPaperLeftSeen;
      private bool isPaperRightSeen;

      public override void Update()
      {
         if (isHourSeen && isPaperLeftSeen && isPaperRightSeen)
         {
            ValidateStep();
         }
      }
   }
   
   [Serializable]
   public class Step3 : Step
   {
      public Post postToFav;
      private int nbClick;

      public override void Start()
      {
         base.Start();
         postToFav.AddListenerOnFav(IncreaseNbClick);
      }

      private void IncreaseNbClick()
      {
         if(isActive)
         {
            nbClick++;
         }
      }

      public override void Update()
      {
         if (nbClick > 2)
         {
            ValidateStep();
         }
      }
   }

   public Step1 step1;
   public Step2 step2;
   public Step3 step3;

   protected override void Start()
   {
      steps.Add(step1);
      steps.Add(step2);
      steps.Add(step3);
      base.Start();
   }

   
}
