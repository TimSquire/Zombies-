using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PillarScript : MonoBehaviour
{
    //Variables
    public RawImage background;
    public RawImage Xlogo;
    public Text text;
    public GameObject Player;
    public float dist;
    public bool activated;
    public float hold = 1f;
    public GameController script;
    public GameObject gc;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
        Xlogo.enabled = false;
        text.enabled = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        script = gc.GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset pillar to unactivated
        if(script.activeCount == 0){
            activated = false;
        }
        dist = Vector3.Distance(transform.position, Player.transform.position);
        //If player is close to pillar, display prompt
        if(Vector3.Distance(transform.position, Player.transform.position) < 4 && activated == false && script.left.Length == 0){
            background.enabled = true;
            Xlogo.enabled = true;
            text.enabled = true;
            //Vheck if player holds X for pickup
            if(CrossPlatformInputManager.GetAxis("X") != 0){
                hold -= Time.deltaTime;
                if(hold < 0){
                    activated = true;
                    script.activeCount+=1;
                    audioSource.Play ();
                    hold = 1f;
                }
            } else {
                hold = 1f;
            }

        } else {
            background.enabled = false;
            Xlogo.enabled = false;
            text.enabled = false;
        }
    }
}
