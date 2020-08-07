using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ChestScript : MonoBehaviour
{
    //Variables
    public RawImage background;
    public RawImage Xlogo;
    public Text text;
    public Text text1;
    public Text ammo;
    public Text ammoPrice;
    public RawImage ammoBackground;
    public RawImage ammoLogo;
    public GameObject Player;
    public float dist;
    public bool activated;
    public float hold = 2f;
    public GameController script;
    public GameObject gc;
    public GameObject FPC;
    public WeaponSwitching script2;
    public int buyType;
    public Healing script3;
    public int speedUpgrade;
    public Weapon script4;
    public Weapon script5;
    public GameObject AR;
    public GameObject Shotgun;
    public bool boughtShotgunAmmo;
    public bool boughtARAmmo;
    private AudioSource audioSource;
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
        script3 = Player.GetComponent<Healing>();
        script4 = AR.GetComponent<Weapon>();
        script5 = Shotgun.GetComponent<Weapon>();
        audioSource = GetComponent<AudioSource>();
        speedUpgrade = 1;
        boughtShotgunAmmo = false;
        boughtARAmmo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(script.GO == false){
            script4 = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
            dist = Vector3.Distance(transform.position, Player.transform.position);
            //Display prompt if player is close to chest
            if(Vector3.Distance(transform.position, Player.transform.position) < 3 && activated == false){
                if(buyType == 0 && script2.shotgun == false){
                background.enabled = true;
                Xlogo.enabled = true;
                text.enabled = true;
                text1.enabled = true;
                } else if(buyType == 0 && script2.shotgun == true){
                    ammoBackground.enabled = true;
                    ammoLogo.enabled = true;
                    ammo.enabled = true;
                    ammoPrice.enabled = true;
                }
                else if(buyType == 1 && script2.AR == false){
                    background.enabled = true;
                    Xlogo.enabled = true;
                    text.enabled = true;
                    text1.enabled = true;
                } else if(buyType == 1 && script2.AR == true){
                    ammoBackground.enabled = true;
                    ammoLogo.enabled = true;
                    ammo.enabled = true;
                    ammoPrice.enabled = true;
                } else if(buyType == 2 && activated == false){
                background.enabled = true;
                Xlogo.enabled = true;
                text.enabled = true;
                text1.enabled = true; 
                } else if(buyType == 3 && activated == false){
                    background.enabled = true;
                Xlogo.enabled = true;
                text.enabled = true;
                text1.enabled = true;
                }
                //Check if player holds X for pickup
                if(CrossPlatformInputManager.GetAxis("X") != 0){
                    hold -= Time.deltaTime;
                    if(hold <= 1){
                        //Charge player for purchase and give them weapon/upgrade
                        if(buyType == 0 && script.XP >= 2000 && script2.shotgun == false){
                            script2.shotgun = true;
                            script.XP -= 2000;
                            background.enabled = false;
                            Xlogo.enabled = false;
                            text.enabled = false;
                            text1.enabled = false;
                            audioSource.Play ();
                        } else if(buyType == 0 && script.XP >= 1000 && script2.shotgun == true && script5.stock < script5.maxStock){
                            boughtShotgunAmmo = true; 
                            script.XP -= 1000;        
                            audioSource.Play ();              
                        }else if(buyType == 1 && script.XP >= 3000 && script2.AR == false){
                            script2.AR = true;
                            script.XP -= 3000;
                            background.enabled = false;
                            Xlogo.enabled = false;
                            text.enabled = false;
                            text1.enabled = false;
                            audioSource.Play ();
                        } else if(buyType == 1 && script.XP >= 1500 && script2.AR == true && script4.stock < script4.maxStock){
                            boughtARAmmo = true;
                            script.XP -= 1500;
                            audioSource.Play ();
                        } else if(buyType == 2 && script.XP >= 2500){
                            script3.maxHealth = 250;
                            script3.health = 250;
                            script.XP -= 2500;
                            activated = true;
                            audioSource.Play ();
                        } else if(buyType == 3 && script.XP >= 2500){
                            speedUpgrade = 2;
                            script.XP -= 2500;
                            activated = true;
                            audioSource.Play ();
                        }
                        hold = 2f;
                    }
                } else {
                    hold = 2f;
                }

            } else {
                background.enabled = false;
                Xlogo.enabled = false;
                text.enabled = false;
                text1.enabled = false;
                ammo.enabled = false;
                ammoPrice.enabled = false;
                ammoBackground.enabled = false;
                ammoLogo.enabled = false;
            }
        }
    }
}
