using UnityEngine;

namespace Common.Objects
{
    public abstract class Enemy: MonoBehaviour
    {
        public abstract void SetLocation(Vector3 location);
    }
}