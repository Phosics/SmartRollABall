using UnityEngine;

namespace Common
{
    public class TrainingManager: MonoBehaviour
    {
        public bool trainingMode = false;

        [Space(3)]
        public GameObject[] gameObjects;
        
        private void Awake()
        {
            if (!trainingMode)
                return;
            
            foreach (var gameObjectToDisable in gameObjects)
                gameObjectToDisable.SetActive(false);
        }
    }
}