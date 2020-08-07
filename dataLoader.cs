using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataLoader : MonoBehaviour
{
    public string[] score;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW scores = new WWW("http://localhost/scores.php");
        yield return scores;
        string dataString = scores.text;
        print (dataString);
        score = dataString.Split(';');
        print(GetDataValue(score[0], "Date:"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Searching Database for Highscores
    string GetDataValue(string data, string index){
        string value = data.Substring(data.IndexOf(index)+index.Length);
        if(value.Contains("|")){
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
}
