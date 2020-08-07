using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class AimDownSights : MonoBehaviour
{
  //Variables
    public Vector3 aimDownSight;
    public Vector3 hipFire;
    public GameObject aim;
    public GameObject hip;

    // Update is called once per frame
    void Update()
    {
      //ADS
      if(CrossPlatformInputManager.GetAxis("Left Trigger") != 0){
        transform.localPosition =  Vector3.Slerp(transform.localPosition, aimDownSight, 10 * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, aim.transform.rotation, 10 * Time.deltaTime);
      } else {
        //Hip Fire
         transform.localPosition =  Vector3.Slerp(transform.localPosition, hipFire, 10 * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, hip.transform.rotation, 10 * Time.deltaTime);
      } 

    }
}
