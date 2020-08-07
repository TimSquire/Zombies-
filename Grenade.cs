using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Grenade : MonoBehaviour
{
    //Variables
    public float damage;
    public float blastRadius;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Check if grenade hits ground
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Terrain"){
            StartCoroutine("Explode");
        }
    }
    //Instantiate explosion after 1 and a half seconds
    IEnumerator Explode(){
        yield return new WaitForSeconds(1.5f);
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
