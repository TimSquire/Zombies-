using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataInserter : MonoBehaviour
{
    //Variables
    private string day;

    string CreateUserURL = "http://localhost/insertScore.php";
    
    // Start is called before the first frame update
    void Start()
    {
        day = System.DateTime.Now.ToString("MM/dd/yy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Sending Scores to Database
    public void CreateScore(int score, string oday){
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("date", oday);

        WWW www = new WWW(CreateUserURL, form);
    }
}
