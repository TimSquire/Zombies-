using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class moveCrosshairs : MonoBehaviour
{
  //Variables
    public Vector3 aimDownSight;
    public Vector3 hipFire;
    public Vector3 hipScale;
    public Vector3 aimScale;

    // Update is called once per frame
    void Update()
    {
      //ADS
      if(CrossPlatformInputManager.GetAxis("Left Trigger") != 0){
        transform.localPosition =  Vector3.Slerp(transform.localPosition, aimDownSight, 10 * Time.deltaTime);
        transform.localScale =  Vector3.Slerp(transform.localScale, aimScale, 10 * Time.deltaTime);
      } else {
        //HipFire
         transform.localPosition =  Vector3.Slerp(transform.localPosition, hipFire, 10 * Time.deltaTime);
         transform.localScale =  Vector3.Slerp(transform.localScale, hipScale, 10 * Time.deltaTime);
      } 

    }
}
