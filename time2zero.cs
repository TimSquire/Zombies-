using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time2zero : MonoBehaviour
{
    //Variables
   public Text time;
   public Timer timer;
   void Start(){
       timer = time.GetComponent<Timer>();
   }
   //If player presses quit, time to zero
   public void zero(){
       timer.time = 0;
   }
}
