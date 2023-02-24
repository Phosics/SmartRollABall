using UnityEngine;

public class S01PostProcessManager : MonoBehaviour
{
    public const int TimeForEffect = 2;
    public GameObject volumn;

    private float timeLeft = 0;

    public void ActiveEffect()
    {
        volumn.SetActive(true);
        timeLeft += TimeForEffect;
    }

    public void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0)
        {
            timeLeft = 0;
            volumn.SetActive(false);
        }
    }
}
