using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson {
    public class MutantSight : MonoBehaviour {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State1{
            PATROL,
            CHASE,
            INVESTIGATE
        }
        public State1 state;
        private bool alive;

        Animator anim;
        bool ZombieAttack = false;

        //Variables for patrolling
        public GameObject[] waypoints;
        public int waypointInd;
        public float patrolSpeed = 0.5f;

        //Variables for chasing
        public float ChaseSpeed = 4.5f;
        public GameObject target;

        //Variables for investigating
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for Sight
        public float heightMultiplyer;
        public float sightDist = 10;

        private IEnumerator coroutine;
        private SphereCollider sphere;
        private bool attacking;
        public GameObject zombie;
        public MutantHealth mh;
        private int attackind;

        // Start is called before the first frame update
        void Start()
        {
           agent = GetComponent<NavMeshAgent>();
           character = GetComponent<ThirdPersonCharacter>();
           sphere = GetComponent<SphereCollider>();
           anim = gameObject.GetComponent<Animator>();
           mh = zombie.GetComponent<MutantHealth>();

           agent.updatePosition = true;
           agent.updateRotation = false;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

           state = MutantSight.State1.CHASE;

           target = GameObject.FindGameObjectWithTag("Player");

           alive = true;

           heightMultiplyer = 1.36f;
           attacking = false;
        }
        //Check mutant state
      void FixedUpdate(){
          if(state == MutantSight.State1.PATROL){
              //Patrol();
          } else if(state == MutantSight.State1.CHASE && mh.alive == true){
              Chase();
          } else if(state == MutantSight.State1.INVESTIGATE){
              //Investigate();
          }
      }
      void Update(){
          //If mutant is attacking, assign it random attack, either swipe or jump attack
          if(attacking == true){
                StartCoroutine("Attack");
                attackind = Random.Range(1,3);
                attacking = false;
          }
          //affect mutant speed based on animation state
          if(anim.GetBool("Running") == true){
              ChaseSpeed = 7.5f;
          } else {
              ChaseSpeed = 4.5f;
          }
      }
      //Chase Player
        void Chase(){
            agent.speed = ChaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        //If player is close to mutant, make mutant attack player
        void OnTriggerEnter(Collider coll){
            if(coll.tag == "Player"){
                attacking = true;
            } else {
                attacking = false;
            }
        }
        //Initiate attack sequence
        IEnumerator Attack(){
            if(attackind == 1){
                anim.SetBool("Swiping", true);
                yield return new WaitForSeconds(2);
                StopCoroutine("Attack");
                attackind = 0;
            } else if(attackind == 2){
                anim.SetBool("Smashing", true);
                yield return new WaitForSeconds(2);
                StopCoroutine("Attack"); 
                attackind = 0;
            }
            anim.SetBool("Swiping", false);
            anim.SetBool("Smashing", false);
        }
    }
}
