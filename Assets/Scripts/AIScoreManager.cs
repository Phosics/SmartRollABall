public class AIScoreManager
{
    private int _maxScore;
    private int _score;

    public AIScoreManager(int maxScore)
    {
        _maxScore = maxScore;
        _score = 0;
    }

    public bool Increase()
    {
        return ++_score >= _maxScore;
    }

    public void Reset()
    {
        _score = 0;
    }
}