using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour {

    /// <summary>
    /// List to keep track of all our logs
    /// </summary>
    public List<string> Logs = new List<string>();

    /// <summary>
    /// The Text Object
    /// </summary>
    public Text LogTextObject;
    private string logText = "";

    /// <summary>
    /// Log the event to the List and then update the game log text
    /// </summary>
    /// <param name="eventString">Event text to add to game log</param>
    public void Log(string eventString)
    {
        Logs.Add(eventString);

        logText = "";
        int idx = 1;
        Logs.ForEach(gameEvent => {
            logText += idx.ToString() + " ";
            logText += gameEvent;
            logText += "\n";
            ++idx;
        });

        LogTextObject.text = logText;
    }
}
