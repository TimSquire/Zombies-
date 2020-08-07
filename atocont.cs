using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class atocont : MonoBehaviour
{
    //Variables
    public bool continuebutton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //If Player presses A, send them to game
        continuebutton = CrossPlatformInputManager.GetButtonDown("A");
        if(continuebutton == true){
            SceneManager.LoadScene (1);

        }
    }
}
