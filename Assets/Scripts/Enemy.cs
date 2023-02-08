using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Enemy: MonoBehaviour
    {
        public abstract void SetStartLocation(Vector3 location);
    }
}