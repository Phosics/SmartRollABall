using UnityEngine;

namespace Common
{
    public abstract class TrainLogicable: MonoBehaviour
    {
        protected bool trainingMode = false;

        public virtual void EnterTrainingMode()
        {
            trainingMode = true;
        }
    }
}