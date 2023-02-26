using TMPro;
using UnityEngine;

namespace Common
{
    public class HighScorePlayerMenu : MonoBehaviour
    {
        [Space(5)]
        [Header("Player Stats")]
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerTime;
        public TextMeshProUGUI playerNumber;
        
        [Space(5)]
        [Header("Rank")]
        public int rank;

        public void Start()
        {
            playerNumber.SetText("#" + rank);
        }
    }
}