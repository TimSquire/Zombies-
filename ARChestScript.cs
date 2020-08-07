using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ARChestScript : MonoBehaviour
{
    //Variables
    public RawImage background;
    public RawImage Xlogo;
    public Text text;
    public Text text1;
    public GameObject Player;
    public float dist;
    public bool activated;
    public float hold = 1f;
    public GameController script;
    public GameObject gc;
    public GameObject FPC;
    public WeaponSwitching script2;
    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
        Xlogo.enabled = false;
        text.enabled = false;
        text1.enabled = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        script = gc.GetComponent<GameController>();
        activated = false;
        script2 = FPC.GetComponent<WeaponSwitching>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance Between Chest and Player
        dist = Vector3.Distance(transform.position, Player.transform.position);
        //Displaying Prompt
        if(Vector3.Distance(transform.position, Player.transform.position) < 3 && activated == false){
            background.enabled = true;
            Xlogo.enabled = true;
            text.enabled = true;
            text1.enabled = true;
            //Checking to see if player holds X for pickup
            if(CrossPlatformInputManager.GetAxis("X") != 0){
                hold -= Time.deltaTime;
                if(hold < 0){
                    activated = true;
                    script2.AR = true;
                    script.XP -= 7500;
                    hold = 1f;
                }
            } else {
                hold = 1f;
            }

        } else {
            background.enabled = false;
            Xlogo.enabled = false;
            text.enabled = false;
            text1.enabled = false;
        }
    }
}
