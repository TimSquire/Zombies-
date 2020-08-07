using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpTimeBonus : MonoBehaviour
{
    //Variables
    public bool taken;
    public bool healthBonus;
    public bool ammoBonus;
    public int perkType;
    public GameObject GC;
    public GameController script;
    public Healing script2;
    public GameObject Player;
    public GameObject FPC;
    public ThrowGrenade script3;

    // Start is called before the first frame update
    void Start()
    {
        taken = false;
        healthBonus = false;
        ammoBonus = false;
        Player = GameObject.Find("Player");
        FPC = GameObject.Find("Player/FirstPersonCharacter");
        script2 = Player.GetComponent<Healing>();
        GC = GameObject.Find("GameController");
        script3 = FPC.GetComponent<ThrowGrenade>();
        script = GC.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy pickup after player walks into it
        if(taken == true){
            script.taken = true;
            Destroy(gameObject);
        }
        if(healthBonus == true){
            Destroy(gameObject);
        }if(ammoBonus == true){
            Destroy(gameObject);
        }
    }
    //Grant player perks
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Player"){ 
            if(perkType == 0){
                taken = true;
            } else if(perkType == 1){
                healthBonus = true;
                script2.health = script2.maxHealth;
            } else if(perkType == 2){
                ammoBonus = true;
                script.ammoBonus = true;
                script3.gStock = 3;
            }
        }
    }
}
