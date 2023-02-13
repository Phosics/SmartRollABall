using UnityEngine;

namespace Common
{
    public abstract class Enemy: MonoBehaviour
    {
        public abstract void SetLocation(Vector3 location);
    }
}