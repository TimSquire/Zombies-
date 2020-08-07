using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson {
    public class EnemySight : MonoBehaviour {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State{
            PATROL,
            CHASE,
            INVESTIGATE
        }
        public State state;
        private bool alive;

        Animator anim;
        bool ZombieAttack = false;

        //Variables for patrolling
        public GameObject[] waypoints;
        public int waypointInd;
        public float patrolSpeed = 0.5f;

        //Variables for chasing
        public float ChaseSpeed = 1f;
        public GameObject target;

        //Variables for investigating
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for Sight
        public float heightMultiplyer;
        public float sightDist = 10;

        //Other Variables
        private IEnumerator coroutine;
        private SphereCollider sphere;
        private bool attacking;
        public GameObject zombie;
        public zombieHelath zh;
        public GameObject GC;
        public GameController gcscript;
        public bool run;
        public float dist;
        public bool played;
        private AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
           audioSource = GetComponent<AudioSource>();
           played = false;
           run = false;
           agent = GetComponent<NavMeshAgent>();
           character = GetComponent<ThirdPersonCharacter>();
           sphere = GetComponent<SphereCollider>();
           anim = gameObject.GetComponent<Animator>();
           zh = zombie.GetComponent<zombieHelath>();
           GC = GameObject.Find("GameController");
           gcscript = GC.GetComponent<GameController>();

           agent.updatePosition = true;
           agent.updateRotation = false;

           waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
           waypointInd = Random.Range(0, waypoints.Length);

           state = EnemySight.State.CHASE;

           target = GameObject.FindGameObjectWithTag("Player");

           alive = true;

           heightMultiplyer = 1.36f;
           attacking = false;
        }
      void FixedUpdate(){
          //Check Zombie State
          if(state == EnemySight.State.PATROL){
              //Patrol();
          } else if(state == EnemySight.State.CHASE && zh.alive == true){
              Chase();
          } else if(state == EnemySight.State.INVESTIGATE){
              //Investigate();
          }
      }
      void Update(){
          dist = Vector3.Distance(transform.position, target.transform.position);
          //Play sound if zombie gets close to player
          if(dist <= 10 && played == false){
              audioSource.Play ();
              played = true;
          }
          //If its wave 4 or later, make zombie run
          if(gcscript.wave >= 4 && run == false){
              anim.SetBool("run", true);
              run = true;
          }
          OnTriggerEnter(sphere);
          if(gcscript.wave <= 5){
              ChaseSpeed = gcscript.wave;
          } else {
              ChaseSpeed = 5;
          } 
      }
      //Chase Player
        void Chase(){
            agent.speed = ChaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        //If player is close to zombie, make zombie attack
        void OnTriggerEnter(Collider coll){
            if(coll.tag == "Player"){
                coroutine = Attack();
                StartCoroutine(coroutine);
                attacking = true;
            } else {
                attacking = false;
            }
        }
        //Attack sequence
        IEnumerator Attack(){
            anim.SetBool("Punch", true);
            if(zh.alive == false){ 
                anim.SetBool("Punch", false);
                StopCoroutine(coroutine);
            }
            yield return new WaitForSeconds(2.167f);
            anim.SetBool("Punch", false);
            StopCoroutine(coroutine);
        }
    }
}
