using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntriesTransformList;
    private Highscores highscores;
    private string stageName;

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
    }

    private void OnEnable()
    {
        // Load saved Highscore
        string stageNumber = PlayerPrefs.GetString("stage_number");
        stageName = "highscoreTable_" + stageNumber;
        string jsonString = PlayerPrefs.GetString(stageName);
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        for (var i = 0; i < entryContainer.childCount; i++)
        {
            var child = entryContainer.GetChild(i);
            child.gameObject.SetActive(false);
        }

        highscoreEntriesTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntriesTransformList);
        }

        Highscores Highscores = new Highscores { highscoreEntryList = highscores.highscoreEntryList };
        string json = JsonUtility.ToJson(Highscores);
        // PlayerPrefs.SetString("highscoreTable", json);
        // PlayerPrefs.Save();
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
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        float gametime = highscoreEntry.time;
        string name = highscoreEntry.name;
        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = Timer.TimeToString(gametime);
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        entryTransform.Find("BackgroundScore").gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(float time, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { name = name, time = time };

        // Add new entry to Highscore
        highscores.highscoreEntryList.Add(highscoreEntry);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].time < highscores.highscoreEntryList[i].time)
                {
                    // swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        while (highscores.highscoreEntryList.Count > 10)
        {
            highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count - 1);
        }

        // save updated Highscore
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(stageName, json);
        PlayerPrefs.Save();
    }

    public void ResetScoreBoard()
    {
        Debug.Log("reseting the highscore board of stage " + stageName);
        PlayerPrefs.SetString(stageName, "");
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
    }
}
