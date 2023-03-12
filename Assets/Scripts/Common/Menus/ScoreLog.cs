using UnityEngine;
using System.IO;
using UnityEditor;

public class ScoreLog : MonoBehaviour
{
    public string fileName;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, fileName);
        if (!File.Exists(filePath))
        {
            Debug.Log("creating new log file at " + filePath);
            TextWriter textWriter = new StreamWriter(filePath, false);
            textWriter.WriteLine("MapNumber,PlayerName,Time,pickups,win");
            textWriter.Close();
        }
        else
            Debug.Log("log file exist at " + filePath);
    }

    public void WriteCSV(string map, string name, float time, int pickups, bool isWin)
    {
        int seconds = (int)time;
        Debug.Log("save to csv: " + map + "," + name + "," + seconds + "," + pickups + "," + isWin);
        TextWriter textWriter = new StreamWriter(filePath, true);
        textWriter.WriteLine(map + "," + name + "," + seconds + "," + pickups + "," + isWin);
        textWriter.Close();
        AssetDatabase.ImportAsset("Assets/" + fileName);
    }
}
