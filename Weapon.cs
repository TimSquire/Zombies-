using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //Variables
    public GameObject shot;
    public Transform ShotSpawn;
    public float fireRate;
    private float nextFire;
    public GameObject bulletRotation;
    public GameObject muzzleflare;
    public GameObject FlareSpawn;
    public int clipCapacity;
    public int clip;
    public float reloadSpeed;
    public int stock;
    public int maxStock;
    public bool reloading;
    private bool outOfAmmo;
    public Text reloadText;
    public Text cliptext;
    public Text stocktext;
    public int damage;
    public bool reload;
    public GameObject chest;
    public ChestScript cs;
    public GameObject chest1;
    public ChestScript cs1;
    public GameObject chest2;
    public ChestScript cs2;
    public int gunId;
    public bool UserReload;
    public int diff;
    private AudioSource audioSource;
    public float givenD;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextFire = 0;
        cs = chest.GetComponent<ChestScript>();
        cs1 = chest1.GetComponent<ChestScript>();
        cs2 = chest2.GetComponent<ChestScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UserReload = CrossPlatformInputManager.GetButtonDown("X");
        //If player wants to manually reload, fill clip
        if(UserReload == true){
            if(clip < clipCapacity){
                reload = true;
                reloading = true;
                if(stock >= clipCapacity){
                    diff = clipCapacity - clip;
                    clip += diff;
                    stock -= diff;
                } else {
                    clip = 0;
                    outOfAmmo = true;
                }
            }
        }
        //Restock ammo if player buys ammo
        if(cs1.boughtShotgunAmmo == true && gunId == 1){
            stock = maxStock;
            cs1.boughtShotgunAmmo = false;
        } 
        if(cs2.boughtARAmmo == true && gunId == 2){
            stock = maxStock;
            cs2.boughtARAmmo = false;
        }
        //If out of ammo, display out of ammo in-game
        if(clip <= 0 && stock <= 0){
            outOfAmmo = true;
        } else {
            outOfAmmo = false;
        }
        //If no ammo in clip, automatically reload for player
        if(clip <= 0){
            reload = true;
            reloading = true;
            if(stock >= clipCapacity){
                clip += clipCapacity;
                stock -= clipCapacity;
                reloading = true;
            } else if(stock > 0 && stock < clipCapacity) {
                clip += stock;
                stock = 0;
                reloading = true;
            } else {
                clip = 0;
                outOfAmmo = true;
            }
        }
        //Initiate reload sequence
        if(reload == true){
            StartCoroutine("Reloading");
            reload = false;
        }
        if(reloading == true && Time.deltaTime < 2){
            StartCoroutine("Reloading");
        }
        //Reload Text
        if(reloading == true){
            reloadText.text = "Reloading";
        } else if(reloading == false && outOfAmmo == false){
            reloadText.text = "";
        }  
        if(outOfAmmo == true){
            reloadText.text = "Out of Ammo";
        } else if(outOfAmmo == false && reloading == false) {
            reloadText.text = "";
        }
        //Clip and Stcok Text
        cliptext.text = ("x " + clip);
        stocktext.text = ("x " + stock);
        //If player presses RT, fire bullet and take one from clip
        if(CrossPlatformInputManager.GetAxis("RightTrigger") != 0 && Time.time > nextFire && reloading == false && clip > 1){
                nextFire = Time.time + fireRate;
                Instantiate (shot, ShotSpawn.position, bulletRotation.transform.rotation);
                Instantiate (muzzleflare, FlareSpawn.transform.position, bulletRotation.transform.rotation);
                clip--;
                audioSource.Play ();
        } else if(CrossPlatformInputManager.GetAxis("RightTrigger") != 0 && Time.time > nextFire && clip == 1){
            Instantiate (shot, ShotSpawn.position, bulletRotation.transform.rotation);
            clip--;
            audioSource.Play ();
            StartCoroutine("Reloading");
        }
    }

        IEnumerator Reloading (){
            yield return new WaitForSeconds(reloadSpeed / cs.speedUpgrade);
            reloading = false;
            StopCoroutine("Reloading");
        } 
    }
