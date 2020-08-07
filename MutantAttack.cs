using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MutantAttack : MonoBehaviour
{
    //Variables
    public Healing script;
    public MutantHealth script2;
    public float attackDamage;
    public bool attacking;
    public GameObject zombieHips;
    public GameObject zombie;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Player").GetComponent<Healing>();
        script2 = zombieHips.GetComponent<MutantHealth>();
        anim = zombie.GetComponent<Animator>();
        attackDamage = 25;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if mutant is attacking
        if(anim.GetBool("Swiping") == true){
            attacking = true;
        } else {
            attacking = false;
        }
    }
    //If mutant attacks player, lower players health
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Player" && script2.alive == true){
            script.health -= attackDamage;
            GamePad.SetVibration(0,0.25f,0.25f);
        }
    }
}
