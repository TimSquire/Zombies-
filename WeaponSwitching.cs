using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponSwitching : MonoBehaviour
{
    //Variables
    public int selectedWeapon = 0;
    public bool shotgun;
    public bool AR;
    private bool switching;
    public GameObject[] weapons;
    public GameObject GC;
    public GameController script;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.Find("GameController");
        script = GC.GetComponent<GameController>();
        shotgun = false;
        switching = false;
        AR = false;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        switching = CrossPlatformInputManager.GetButtonDown("Y");
        //Check if player is switching weapon
        if(switching == true){
            if(selectedWeapon >= 2){
                selectedWeapon = 0;
            } else {
                selectedWeapon++;
            }
            switching = false;
            //Check if player has other weapons
            if(selectedWeapon == 1 && shotgun == false){
                selectedWeapon++;
            }
            if(selectedWeapon == 2 && AR == false){
                selectedWeapon = 0;
            }
            SelectWeapon();
        }
    }
    //Switch Weapon
    void SelectWeapon (){
        for(int i = 0; i < 3; i++){
            if(i == selectedWeapon){
                weapons[i].gameObject.SetActive(true);
            } else {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
