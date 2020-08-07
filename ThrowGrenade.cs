using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.UI;

public class ThrowGrenade : MonoBehaviour
{
    //Variables
    public Transform GrenadeSpawn;
    public float gFireRate;
    private float gNextFire;
    public GameObject grenade;
    public GameObject bulletRotation;
    public int gStock;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If player presses rb, instantiate grenade
        text.text = ("x " + gStock);
        if(CrossPlatformInputManager.GetAxis("RightBumper") != 0 && Time.time > gNextFire && gStock >= 1){
            gNextFire = Time.time + gFireRate;
                Instantiate (grenade, GrenadeSpawn.position, bulletRotation.transform.rotation);
                gStock--;
        }
    }
}
