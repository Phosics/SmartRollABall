using UnityEngine;

public class ScoreManager
{
    private readonly int _targetScore;
    public int Score { get; private set; } = 0;

    public ScoreManager(int targetScore)
    {
        _targetScore = targetScore;
    }


    public bool Increase()
    {
        return ++Score >= _targetScore;
    }

    public void Reset()
    {
        Score = 0;
    }
}