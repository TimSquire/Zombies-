using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    //Variables
    public float health;
    public float maxHealth;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  
    }

    // Update is called once per frame
    //Updating in-game Health Text
    void Update()
    {
        text.text = ("" + health);
    }
}
