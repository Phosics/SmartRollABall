using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreLog : MonoBehaviour
{
    public string fileName;
    // Start is called before the first frame update
    void Start()
    {
        fileName = Path.Combine(Application.dataPath, fileName);
        if (File.Exists(fileName))
        {
            Debug.Log("creating new log file at " + fileName);
            TextWriter textWriter = new StreamWriter(fileName, false);
            textWriter.WriteLine("MapNumber,PlayerName,Time,pickups,win");
            textWriter.Close();
        }
        else
            Debug.Log("log file exist at " + fileName);
    }

    public void WriteCSV(string map, string name, float time, int pickups, bool isWin)
    {
        int seconds = (int)time;
        Debug.Log("save to csv: " + map + "," + name + "," + seconds + "," + pickups + "," + isWin);
        TextWriter textWriter = new StreamWriter(fileName, true);
        textWriter.WriteLine(map + "," + name + "," + seconds + "," + pickups + "," + isWin);
        textWriter.Close();
    }
}
