using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHelath : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        anim = zombie.GetComponent<Animator>();
        GC = GameObject.Find("GameController");
        GCscript = GC.GetComponent<GameController>();
        Player = GameObject.FindGameObjectWithTag("ShotSpawn");   
        alive = true;
        Health = GCscript.zombieHealth;
        explode = false;
    }

    // Update is called once per frame
    void Update()
    {
       script = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
       //Check if zombie is dead
        if(Health <= 0 && alive == true){
            alive = false;
            anim.SetBool("Punch", false);
            anim.SetBool("Dead", true);
            GCscript.XP += 50;
            GCscript.zKillCount ++;
            StartCoroutine("Die");
        }
        dist = Vector3.Distance(Player.transform.position, zombie.transform.position);
    }
    //If zzombie is hit by bullet, lower its health
    public void OnTriggerEnter(Collider coll){
        if(coll.tag == "Bullet"){
            if(Health > 0){
                ShotDistance = Vector3.Distance(Player.transform.position, zombie.transform.position);
                if(ShotDistance >= 10){
                    damage = script.damage / ShotDistance;
                    Health -= damage;
                } else {
                    Health -= script.givenD;
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
