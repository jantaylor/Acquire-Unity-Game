using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour {

    /// <summary>
    /// List to keep track of all our logs
    /// </summary>
    public List<string> Log = new List<string>();

    /// <summary>
    /// The Text Object
    /// </summary>
    public Text LogTextObject;
    private string logText = "";

    /// <summary>
    /// Log the event to the List and then update the game log text
    /// </summary>
    /// <param name="eventString">Event text to add to game log</param>
    public void AddEvent(string eventString)
    {
        Log.Add(eventString);

        logText = "";
 
        foreach (string gameEvent in Log)
        {
            logText += logText;
            logText += "\n";
        }

        LogTextObject.text = logText;
    }
}
