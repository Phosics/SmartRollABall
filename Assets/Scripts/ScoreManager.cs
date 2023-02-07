using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int targetScore;

    private int _score = 0;

    public bool Increase()
    {
        return ++_score >= targetScore;
    }

    public void Reset()
    {
        _score = 0;
    }
}