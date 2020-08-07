using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson {
    public class AIBasic : MonoBehaviour {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State{
            PATROL,
            CHASE
        }
        public State state;
        private bool alive;

        //Variables for patrolling
        public GameObject[] waypoints;
        public int waypointInd;
        public float patrolSpeed = 0.5f;

        //Variables for chasing
        public float ChaseSpeed = 1f;
        public GameObject target;

        // Start is called before the first frame update
        void Start()
        {
           agent = GetComponent<NavMeshAgent>();
           character = GetComponent<ThirdPersonCharacter>();

           agent.updatePosition = true;
           agent.updateRotation = false;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

           state = AIBasic.State.CHASE;

            target = GameObject.FindGameObjectWithTag("Player");
           alive = true;
        }

      void FixedUpdate(){
          if(state == AIBasic.State.PATROL){
              Patrol();
          } else if(state == AIBasic.State.CHASE){
              Chase();
          } else {

          }
      }
        void Patrol(){
            agent.speed = patrolSpeed;
            if (Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) >= 1){
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move (agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) < 1){
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else {
                character.Move(Vector3.zero, false, false);
            }
        }
        void Chase(){
            agent.speed = ChaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        //void OnTriggerEnter (Collider coll){
         //   if(coll.tag == "Player"){
          //      state = AIBasic.State.CHASE;
          //      target = coll.gameObject;
          //  }
        //}
    }
}
