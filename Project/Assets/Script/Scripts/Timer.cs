using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {


    public GameObject timesUp;
    public Text timertext;
    public Text scoretext;
    public float timeremaining = 10;            // time target for each level/difficulties

    float timeRemainingDuplicate;               // just a dummy variable

    Animator anim;
    int level;

   
    void Start () {
        anim = GameObject.FindGameObjectWithTag("TimerCanvas").GetComponent<Animator>();

        timeRemainingDuplicate = timeremaining;

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            level = 1;
        }
        if (SceneManager.GetActiveScene().name == "MainScene-2")
        {
            level = 2;
        }
    }
	
	void Update () {


        if (level == 1) {
            timeremaining -= Time.deltaTime;

            if (timeremaining > 0)
            {
                timertext.text = "Time Left: " + timeremaining.ToString("f2");
                if (timeremaining <= (timeRemainingDuplicate / 3))
                {
                    anim.SetTrigger("hurryUp");
                }
            }
            else
            {
                timertext.text = "";
                timesUp.SetActive(true);
            }
        }
        if (level == 2)
        {
            //scoretext.text = ColorChange.score.ToString();
        }
    }
}

