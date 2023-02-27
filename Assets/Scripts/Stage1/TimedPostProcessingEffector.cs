using Common;
using UnityEngine;

public class TimedPostProcessingEffector : PostProcessingEffector
{
    public int timeForEffect = 2;
    private float _timeLeft = 0;

    public override void ToggleEffect()
    {
        base.EnableEffect();
        _timeLeft += timeForEffect;
    }

    public void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            return;
        }
        
        _timeLeft = 0;
        base.StopEffect();
    }
}
