using UnityEngine;

public abstract class S01ApplicationManagerBase : MonoBehaviour
{
    public abstract void Pause();
    public abstract void Resume();
    public abstract void Retry();
}