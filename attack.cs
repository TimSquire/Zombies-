using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class attack : MonoBehaviour
{
    //Variables
    public Healing script;
    public zombieHelath script2;
    public float attackDamage;
    public bool attacking;
    public GameObject zombieHips;
    public GameObject zombie;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Player").GetComponent<Healing>();
        script2 = zombieHips.GetComponent<zombieHelath>();
        anim = zombie.GetComponent<Animator>();
        attackDamage = 25;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if zombie is attacking
        if(anim.GetBool("Punch") == true){
            attacking = true;
        } else {
            attacking = false;
        }
    }
    void OnTriggerEnter(Collider coll){
        //Checking if zombie attacks player
        if(coll.tag == "Player" && script2.alive == true){
            script.health -= attackDamage;
            GamePad.SetVibration(0,0.25f,0.25f);
        }
    }
}
