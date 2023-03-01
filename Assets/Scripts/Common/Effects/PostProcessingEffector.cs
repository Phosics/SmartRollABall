using UnityEngine;

namespace Common.Effects
{
    public class PostProcessingEffector : TrainLogicable
    {
        public GameObject volume;
    
        private void Start()
        {
            volume.SetActive(false);
        }

        public virtual void EnableEffect()
        {
            if (trainingMode)
                return;
        
            volume.SetActive(true);
        }
   
        public virtual void StopEffect()
        {
            if (trainingMode)
                return;
        
            volume.SetActive(false);
        }
    
        public virtual void ToggleEffect()
        {
            if (trainingMode)
                return;
        
            volume.SetActive(!volume.activeSelf);
        }
    }
}
