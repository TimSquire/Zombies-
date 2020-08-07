using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    //Variables
    public GameObject FinishCamSpawn;
    public Canvas Pause;
    public bool pause;
    public bool GO;
    public bool scoreUploaded;
    public Text GameOver;
    public Canvas EndCanvas;
    public Text finalTime;
    public Text zKills;
    public Text mKills;
    public GameObject FinishCam;
    public GameObject[] hazards;
    public GameObject[] spawn;
    public int hazardCount;
    public float spawnWait;
    public int activeCount;
    public int wave;
    public Text XPText;
    public bool started;
    public bool bonus;
    public GameObject timeBonus;
    public GameObject timeSpawn;
    public GameObject Player;
    public GameObject[] left;
    private int timebonusseconds;
    public Text time;
    public Timer timeScript;
    private int timerint;
    public GameObject TB;
    public GameObject HB;
    public GameObject AB;
    public pickUpTimeBonus tbScript;
    public pickUpTimeBonus hbScript;
    public pickUpTimeBonus abScript;
    public bool taken;
    public float zombieHealth;
    public GameObject Mutant;
    public int XP;
    public Text waveText;
    public GameObject perkSpawn;
    public GameObject Pistol;
    public GameObject Shotgun;
    public GameObject AR;
    public Weapon wscript;
    public Weapon wscript2;
    public Weapon wscript3;
    public bool healthBonus;
    public bool ammoBonus;
    public Canvas canvas;
    public float finalMin;
    public float finalSeconds;
    public int zKillCount;
    public int mKillCount;
    private AudioSource audioSource;
    public string[] score; 
    public int hs1;
    public int hs2;
    public int hs3;
    public int h1secs;
    public int h2secs;
    public int h3secs;
    public string date1;
    public string date2;
    public string date3;
    public Text HS1;
    public Text HS2;
    public Text HS3;
    public Canvas HSCanvas;

    private string day;
    string CreateUserURL = "http://localhost/insertScore.php";
    // Start is called before the first frame update
    void Start()
    { 
       Pause.enabled = false;
       GO = false;
       scoreUploaded = false;
       day = System.DateTime.Now.ToString("MM/dd/yyyy");
       GameOver.enabled = false;
       EndCanvas.enabled = false;
       HSCanvas.enabled = false;
       activeCount = 0; 
       wave = 1;
       started = false;
       bonus = false;
       healthBonus = false;
       ammoBonus = false;
       Player = GameObject.FindGameObjectWithTag("Player");
       left = GameObject.FindGameObjectsWithTag("Enemy");
       timebonusseconds = 120;
       timeScript = time.GetComponent<Timer>();
       tbScript = TB.GetComponent<pickUpTimeBonus>();
       hbScript = HB.GetComponent<pickUpTimeBonus>();
       abScript = AB.GetComponent<pickUpTimeBonus>();
       wscript = Pistol.GetComponent<Weapon>();
       wscript2 = Shotgun.GetComponent<Weapon>();
       wscript3 = AR.GetComponent<Weapon>();
       audioSource = GetComponent<AudioSource>();
       taken = false;
       zombieHealth = 90f;
       XP = 1000;
       zKillCount = 0;
       mKillCount = 0;
       audioSource.Play ();

    }

    // Update is called once per frame
    void Update()
    {
        //MenuButon
        pause = CrossPlatformInputManager.GetButtonDown("Start");
        if(pause == true && GO == false){
            Pause.enabled = true;
        }
        //FinishCam
        if(timeScript.time <= 0 && scoreUploaded == false){
            CreateScore(timeScript.totalTime, day);
            Instantiate (FinishCam, FinishCamSpawn.transform.position, FinishCamSpawn.transform.rotation);
            scoreUploaded = true;
        }
        //EndGame Text
        zKills.text = ("Zombie Kills: " + zKillCount);
        mKills.text = ("Mutant Kills: " + mKillCount);
        finalMin = Mathf.Floor(timeScript.totalTime / 60);
        finalSeconds = timeScript.totalTime % 60;
        if(finalSeconds < 10){
            finalTime.text = ("Time Survived   " + finalMin + ":" + "0" + finalSeconds);
        } else {
            finalTime.text = ("Time Survived   " + finalMin + ":" + finalSeconds);
        }
        //Initiate EndGame
        if(timeScript.time <= 0 && GO == false){
            GO = true;
            StartCoroutine("EndGame");
            Destroy(Player);
            audioSource.Stop ();
        }
        //Picking up ammo bonus
        if(ammoBonus == true){
            wscript.stock = wscript.maxStock;
            wscript2.stock = wscript2.maxStock;
            wscript3.stock = wscript3.maxStock;
            ammoBonus = false;
        }
        //WaveText
        if(wave == 1){
            waveText.text = "I";
        } else if(wave == 2){
            waveText.text = "II";
        } else if(wave == 3){
            waveText.text = "III";
        } else if(wave == 4){
            waveText.text = "IV";
        } else if(wave == 5){
            waveText.text = "V";
        } else if(wave == 6){
            waveText.text = "VI";
        } else if(wave == 7){
            waveText.text = "VII";
        } else if(wave == 8){
            waveText.text = "VIII";
        } else if(wave == 9){
            waveText.text = "IX";
        } else if(wave == 10){
            waveText.text = "X";
        } else if(wave == 11){
            waveText.text = "XI";
        } else if(wave == 12){
            waveText.text = "XII";
        } else if(wave == 13){
            waveText.text = "XIII";
        } else if(wave == 14){
            waveText.text = "XIV";
        } else if(wave == 15){
            waveText.text = "XV";
        } else if(wave == 16){
            waveText.text = "XVI";
        } else if(wave == 17){
            waveText.text = "XVII";
        } else if(wave == 18){
            waveText.text = "XVIII";
        } else if(wave == 19){
            waveText.text = "XIX";
        } else if(wave == 20){
            waveText.text = "XX";
        } 
        //Increase ZombieHealth
        zombieHealth = 90f + (10f * wave);
        if(taken == true){
            timeScript.time += timebonusseconds;
            taken = false;
        }
        //Increase TimeBonus Value
        timebonusseconds = 120 + hazardCount;
        //Initiate Zombie Waves
        left = GameObject.FindGameObjectsWithTag("Enemy");
        if(activeCount == 4){
            StartCoroutine (SpawnWaves ());
            activeCount = 0;
        }
        XPText.text = ("$" + XP);
        //Spawning Bonuses
        if(left.Length == 0 && started == true && bonus == false){
            Instantiate (timeBonus, timeSpawn.transform.position, timeBonus.transform.rotation);
            bonus = true;
            wave += 1;
            if(wave % 2 == 0){
                Instantiate (HB, perkSpawn.transform.position, HB.transform.rotation);
            } else {
                Instantiate (AB, perkSpawn.transform.position, AB.transform.rotation);
            }
        }

    }
    //Sending Scores to DataBase;
    public void CreateScore(int score, string oday){
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("date", oday);

        WWW www = new WWW(CreateUserURL, form);
    }
    //WaveSpawner
    IEnumerator SpawnWaves (){
        for(int i = 0; i < hazardCount; i++){
            GameObject hazard = hazards[Random.Range(0,hazards.Length)];
            int ind = Random.Range(0,spawn.Length);
            Vector3 spawnPos = spawn[ind].transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate (hazard, spawnPos, spawnRotation);
            yield return new WaitForSeconds (spawnWait);
        }
        if(wave % 3 == 0){
            int ind = Random.Range(0,spawn.Length);
            Vector3 spawnPos = spawn[ind].transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate (Mutant, spawnPos, spawnRotation);
        }
        hazardCount+=5;
        started = true;
        bonus = false;
        StopCoroutine("SpawnWaves");
    } 
    //EndGame Display (GameOver, Scores, Highscores)
    IEnumerator EndGame(){
        StartCoroutine("HighScore");
        GameOver.enabled = true;
        FinishCam.SetActive(true);
        Destroy(canvas);
        Destroy(Pause);
        yield return new WaitForSeconds(10);
        Destroy(GameOver);
        EndCanvas.enabled = true;
        yield return new WaitForSeconds(10);
        Destroy(EndCanvas);
        HSCanvas.enabled = true;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene (0);

    }  
    //Finding HighScores
    IEnumerator HighScore()
    {
        WWW scores = new WWW("http://localhost/scores.php");
        yield return scores;
        string dataString = scores.text;
        score = dataString.Split(';');
        hs1 = int.Parse(GetDataValue(score[0], "TimeSurvived:"));
        hs2 = int.Parse(GetDataValue(score[1], "TimeSurvived:"));
        hs3 = int.Parse(GetDataValue(score[2], "TimeSurvived:"));
        date1 = GetDataValue(score[0], "Date:");
        date2 = GetDataValue(score[1], "Date:");
        date3 = GetDataValue(score[2], "Date:");
        h1secs = hs1 % 60;
        //HighScore Text
        if(h1secs < 10){
            HS1.text = ("" + Mathf.Floor(hs1 / 60) + ":0" + h1secs + "                 " + date1);
        } else {
            HS1.text = ("" + Mathf.Floor(hs1 / 60) + ":" + h1secs + "                 " + date1);
        }
        h2secs = hs2 % 60;
        if(h2secs < 10){
            HS2.text = ("" + Mathf.Floor(hs2 / 60) + ":0" + h2secs + "                 " + date2);
        } else {
            HS2.text = ("" + Mathf.Floor(hs2 / 60) + ":" + h2secs + "                 " + date2);
        }
        h3secs = hs3 % 60;
        if(h3secs < 10){
            HS3.text = ("" + Mathf.Floor(hs3 / 60) + ":0" + h3secs + "                 " + date3);
        } else {
            HS3.text = ("" + Mathf.Floor(hs3 / 60) + ":" + h3secs + "                 " + date3);
        }
    }
    //Grabing Values From DataBase
    string GetDataValue(string data, string index){
    string value = data.Substring(data.IndexOf(index)+index.Length);
    if(value.Contains("|")){
        value = value.Remove(value.IndexOf("|"));
    }
    return value;
    }
}
