using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectOnInput : MonoBehaviour
{
    //Variables
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool ButtonSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //Select Button
    void Update()
    {
        if(Input.GetAxisRaw ("Vertical") != 0 && ButtonSelected == false){
            eventSystem.SetSelectedGameObject(selectedObject);
            ButtonSelected = true;
        }
    }
    private void OnDisable(){
        ButtonSelected = false;
    }
}
