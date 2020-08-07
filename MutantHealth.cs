using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHealth : MonoBehaviour
{
    //Variables
    public float Health;
    
    private CapsuleCollider capsule;
    public float ShotDistance;
    public float dist;
    public Weapon script;
    private float damage;
    Animator anim;
    public GameObject Player;
    public bool alive;
    public GameObject zombie;
    public GameObject GC;
    public GameController GCscript;
    public GameObject grenade;
    public float grenadeDamage;
    public bool explode;
    private AudioSource audioSource;
    public float checkgd;

    // Start is called before the first frame update
    void Start()
    {
        anim = zombie.GetComponent<Animator>();
        GC = GameObject.Find("GameController");
        GCscript = GC.GetComponent<GameController>();
        Player = GameObject.FindGameObjectWithTag("ShotSpawn");   
        alive = true;
        Health = 500;
        explode = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       script = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
       checkgd = script.givenD;
       //Check if zombie is dead
        if(Health <= 0 && alive == true){
            audioSource.Play ();
            alive = false;
            anim.SetBool("Swiping", false);
            anim.SetBool("Smashing", false);
            anim.SetBool("Running", false);
            anim.SetBool("Dead", true);
            GCscript.XP += 200;
            GCscript.mKillCount ++;
            StartCoroutine("Die");
        }
        if(Health <= 300){
            anim.SetBool("Running", true);
        }
        dist = Vector3.Distance(Player.transform.position, zombie.transform.position);
    }
    //If zzombie is hit by bullet, lower its health
    public void OnTriggerEnter(Collider coll){
        if(coll.tag == "Bullet"){
            if(Health > 0){
                if(dist >= 10){
                    damage = script.damage / dist;
                    Health -= damage;
                } else {
                    damage = script.givenD;
                    Health -= damage;
                }
            }
            //Check if zombie is hit by grenade
        } else if(coll.tag == "Explosion"){
                Health -= grenadeDamage;
        }
    } 
    //Zombie death sequence
    IEnumerator Die(){
        yield return new WaitForSeconds(5);
        Destroy(zombie);
        StopCoroutine("Die");
    }
}
