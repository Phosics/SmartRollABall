using UnityEngine;
using TMPro;

namespace Common.Menus
{
   public class Timer : MonoBehaviour
   {
       public float TimeInGame { get; private set; }
       public bool timerIsRunning = false;
       public TextMeshProUGUI timeText;
       private void Start()
       {
           // Starts the timer automatically
           StartTimer();
       }
       void Update()
       {
           if (timerIsRunning)
           {
               TimeInGame += Time.deltaTime;
               DisplayTime(TimeInGame);
           }
       }
   
       public void StopTimer()
       {
           timerIsRunning = false;
       }
   
       public void ResumeTimer()
       {
           timerIsRunning = true;
       }
   
       public void ResetTimer()
       {
           TimeInGame = 0;
       }
   
       public void StartTimer()
       {
           ResetTimer();
           ResumeTimer();
       }
   
       public static string TimeToString(float timeToDisplay)
       {
           float minutes = Mathf.FloorToInt(timeToDisplay / 60);
           float seconds = Mathf.FloorToInt(timeToDisplay % 60);
           return string.Format("{0:00}:{1:00}", minutes, seconds);
       }
   
       private void DisplayTime(float timeToDisplay)
       {
           string time = TimeToString(timeToDisplay);
           timeText.text = time;
       }
   } 
}