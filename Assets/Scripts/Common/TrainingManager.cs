using UnityEngine;

namespace Common
{
    public class TrainingManager: MonoBehaviour
    {
        public bool trainingMode = false;

        private void Awake()
        {
            if (!trainingMode)
                return;
        
            // AudioManager.Disable();
            
            var scriptToEnterToTrain = GetComponentsInChildren<TrainLogicable>();
            foreach (var s in scriptToEnterToTrain)
                s.EnterTrainingMode();
        }
    }
}