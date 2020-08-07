using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour
{
    //Variables
    public GameObject a;
    public Timer script;
    public Vector3 startPos;
    private AudioSource audioSource;
    public bool gameOver;
    public bool played;
    public int lightType;
    
    // Start is called before the first frame update
    void Start()
    {
        played = false;
        audioSource = GetComponent<AudioSource>();
        script = a.GetComponent<Timer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player EndGame Music (I Had to put it on sun, couldn't on GameController)
        if(gameOver == true && played == false && lightType == 1){
            audioSource.Play ();
            played = true;
        }
        //Initiate sunset if time is less than 2 minutes
        if(script.time > 120){
            if(transform.position.y < 495){
               transform.RotateAround(Vector3.zero, Vector3.right, 60f * Time.deltaTime);
               transform.LookAt(Vector3.zero);
            }
            //Send sun back to start
        } else {
            transform.RotateAround(Vector3.zero, Vector3.right, 1f * Time.deltaTime);
            transform.LookAt(Vector3.zero);  
        }
        //Start EndGame if time is up
        if(script.time <= 0){
            gameOver = true;
        }
    }
}