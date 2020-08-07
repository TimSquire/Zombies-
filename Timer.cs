using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //Variables
    public int totalTime;
    public int time;
    public Text countDownText;
    private string seconds;
    private float hours;
    private int secs;
    public GameObject Player;
    public Healing script;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        script = Player.GetComponent<Healing>();
        StartCoroutine("countDown");
          secs = time % 60;
          hours = Mathf.Floor(time / 60);
         if(10 > secs){
             seconds = ("0" + secs);
         }
    }

    // Update is called once per frame
    void Update()
    {
       //If player is dead, time to zero
       if(script.health <= 0){
           time = 0;
       }
       //Seconds = remainder of time / 60
       secs = time % 60;
       //If seconds left are less then zero, add 0 in front
       if(10 > secs){
            countDownText.text = ("Time Remaining " + hours + ":" + seconds);
       } else {
           countDownText.text = ("Time Remaining " + hours + ":" + secs);
       }
        //If time has reached zero, stop counting down
       if(time <= 0){
           StopCoroutine("countDown");
       } 
    }
    //Countdown sequence
    IEnumerator countDown(){
        while(true){
             secs = time % 60;
             hours = Mathf.Floor(time / 60);
            if(10 > secs){
                 seconds = ("0" + secs);
            }
            yield return new WaitForSeconds(1);
            time--;
            totalTime++;
        }
    }
}
