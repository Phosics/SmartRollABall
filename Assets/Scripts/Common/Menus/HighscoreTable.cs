using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace Common.Menus
{
    public class HighscoreTable : MonoBehaviour
    {
        private Transform entryContainer;
        private Transform entryTemplate;
        private List<Transform> highscoreEntriesTransformList;
        private Highscores highscores;
        private string stageName = "highscoreTable_";

        private void OnEnable()
        {
            entryContainer = transform.Find("HighscoreEntryContainer");
            Assert.IsNotNull(entryContainer);
            entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
            Assert.IsNotNull(entryTemplate);

            // Load saved Highscore
            var stageNumber = PlayerPrefs.GetString("stage_number", "");
            Assert.AreNotEqual(stageNumber, "");

            var jsonString = PlayerPrefs.GetString(stageName + stageNumber, "");
            highscores = JsonUtility.FromJson<Highscores>(jsonString) ?? new Highscores { highscoreEntryList = new List<HighscoreEntry>() };

            for (var i = 0; i < entryContainer.childCount; i++)
            {
                var child = entryContainer.GetChild(i);
                child.gameObject.SetActive(false);
            }

            highscoreEntriesTransformList = new List<Transform>();
            foreach (var highscoreEntry in highscores.highscoreEntryList)
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntriesTransformList);
        }

        private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 30f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH";
                    break;

                case 1: rankString = "1ST";
                    entryTransform.Find("IconFirstPlace").gameObject.SetActive(true);
                    break;
                case 2: rankString = "2ND";
                    entryTransform.Find("IconSeconPlace").gameObject.SetActive(true);
                    break;
                case 3: rankString = "3RD";
                    entryTransform.Find("IconThirdPlace").gameObject.SetActive(true);
                    break;
            }

            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;
            entryTransform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = Timer.TimeToString(highscoreEntry.time);
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = highscoreEntry.name;
            entryTransform.Find("BackgroundScore").gameObject.SetActive(rank % 2 == 1);
            entryTransform.Find("IconAIBrain").gameObject.SetActive(highscoreEntry.isAI);
            entryTransform.Find("IconPlayer").gameObject.SetActive(!highscoreEntry.isAI);
            transformList.Add(entryTransform);
        }

        public void AddHighscoreEntry(float time, string name, bool isAI)
        {
            // Create HighscoreEntry
            HighscoreEntry highscoreEntry = new HighscoreEntry { name = name, time = time, isAI = isAI };

            // Add new entry to Highscore
            highscores.highscoreEntryList.Add(highscoreEntry);

            highscores.highscoreEntryList.Sort((s1, s2) => s1.time.CompareTo(s2.time));

            while (highscores.highscoreEntryList.Count > 10)
                highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count - 1);

            // save updated Highscore
            string json = JsonUtility.ToJson(highscores);
            var stageNumber = PlayerPrefs.GetString("stage_number", "");
            Assert.AreNotEqual(stageNumber, "");

            PlayerPrefs.SetString(stageName + stageNumber, json);
            PlayerPrefs.Save();
        }

        public void ResetScoreBoard()
        {
            var stageNumber = PlayerPrefs.GetString("stage_number", "");
            Assert.AreNotEqual(stageNumber, "");

            Debug.Log("reseting the highscore board of stage " + stageName);
            PlayerPrefs.SetString(stageName + stageNumber, "");
            OnEnable();
        }

        private class Highscores
        {
            public List<HighscoreEntry> highscoreEntryList;
        }

        /*
         * Represents a single Highscore entry
         */
        [System.Serializable]
        private class HighscoreEntry
        {
            public float time;
            public string name;
            public bool isAI;
        }
    }
}
