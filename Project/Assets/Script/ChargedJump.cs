using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChargedJump : MonoBehaviour {
    public GameObject iniasta;
    public GameObject lava;
    public GameObject canvas;
   // public GameObject ScoreIcrementer;
    public Text highscore;
    public Text score;
    public AudioClip charged, release, point1, death, start, Background;
    public int scoreCounter =0;

    public Animator menuController;
    private bool isStartPressed = false;

   


    private Vector3 dir; // for target 
	private bool onGround;
	private float jumpPressure;
	private float minJump;
	public float maxJumpPressure = 300f;
	private Rigidbody rb;
	private Animator anim;
	public float jumpIncrementor = 5f;
	public float angleHeight=50f;

	private Vector3 intialPosition;
	public Vector3 target;
    private bool pause;

	// Use this for initialization
	void Start () {


        pause = false;
        menuController.Play("AllButtonsAnim");

        onGround = true;
		jumpPressure = 0f;
		minJump = 2f;

        highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        intialPosition = transform.position;


       // Debug.Log(onGround);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           /* pause = !pause;
            if (!pause)
            {
                Time.timeScale = 1;
            }
            else if (pause)
            {
                Time.timeScale = 0;
            }*/
         
            canvas.SetActive(true);
            menuController.Play("AllButtonsAnim");
        }



        if (isStartPressed)
        {
            if (onGround)
            {
                // holding button
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    
                    if (jumpPressure < maxJumpPressure)
                    {
                        jumpPressure += Time.deltaTime * jumpIncrementor;

                    }
                    else
                    {
                        jumpPressure = maxJumpPressure;
                    }
                    anim.SetFloat("jumpPressure", jumpPressure + minJump);
                    anim.speed = 1f + (jumpPressure / 10);
                    GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(charged);  // sound

                    dir = (target - transform.position + new Vector3(0, angleHeight, 0)).normalized * jumpPressure;
                  // trajectory tweeikking
                   GetComponent<LineRenderer>().enabled = true;
                   UpdateTrajectory(intialPosition, dir, Physics.gravity);
                }

                // not holding Button
                else
                {
                   
                    if (jumpPressure > 0f)
                    {
                        
                        jumpPressure = jumpPressure + minJump;
                        rb.velocity = dir;
                        jumpPressure = 0f;
                        onGround = false;
                                                //--player animation--//
                        anim.SetFloat("jumpPressure", 0f);
                        anim.SetBool("onGround", onGround);
                        anim.speed = 1f;
                                                
                        GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(release);

                        GetComponent<LineRenderer>().enabled = false; // hide trajectory
                    }
                }
            }
        }
	}

    //----- Collision detection-----//
	void OnCollisionEnter(Collision other)
    {
      

        if (other.gameObject.CompareTag ("ground"))
        {
            StartCoroutine( DeathTime(1.2f));
            GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(death);
        }
        

		if(other.gameObject.name == "Ob1.1(Clone)") // obj 1
        {


            transform.Find("Cube").GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
            StartCoroutine(TraiColorWait(0.5f,other.gameObject));
            onGround = true;
			anim.SetBool ("onGround", onGround);
            Debug.Log(onGround);
           // scoreCounter++;
            // world screen ui
          //  ScoreIcrementer.SetActive(true);
           // anim.Play("pointFadeOut");
          //  StartCoroutine(ScoreAdder(.3f));

            score.text = scoreCounter.ToString();

            

            GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(point1);
            // High Score
            if (scoreCounter > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreCounter);
                highscore.text = scoreCounter.ToString();
            }
        }

        if (other.gameObject.name == "Ob2.1(Clone)") // obj2
        {
            transform.Find("Cube").GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
            StartCoroutine(TraiColorWait(1f, other.gameObject));
            onGround = true;
            anim.SetBool("onGround", onGround);
           // scoreCounter++;
            // world screen ui
          //  ScoreIcrementer.SetActive(true);
           // anim.Play("pointFadeOut");
          // StartCoroutine(ScoreAdder(.3f));

            score.text = scoreCounter.ToString();


            GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(point1);
            // High Score
            if (scoreCounter > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreCounter);
                highscore.text = scoreCounter.ToString();
            }

        }
        if (other.gameObject.name == "Ob3.1(Clone)")  // obj3
        {

            transform.Find("Cube").GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
            StartCoroutine(TraiColorWait(1f, other.gameObject));
            onGround = true;
            anim.SetBool("onGround", onGround);
           // scoreCounter++;
            // world screen ui
           // ScoreIcrementer.SetActive(true);
          ///  anim.Play("pointFadeOut");
            

            score.text = scoreCounter.ToString();


            GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(point1);  // find function

            // High Score
            if (scoreCounter > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreCounter);
                highscore.text = scoreCounter.ToString();
            }
        }
       

    }




    //-----Collider Trigger----//






   
    //---- Trajectory----//
    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps =25; // for example
		float timeDelta = 1.0f / initialVelocity.magnitude; // for example

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSteps;

		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numSteps; ++i)
		{
			lineRenderer.SetPosition(i, position);

			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
		}

    }
   
    // ------------- UI Buttons-------------//


    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    public void Play()
    {   
        isStartPressed = true;
        lava.SetActive(true);
        iniasta.SetActive(true);
        GameObject.Find("_SoundManager").GetComponent<AudioSource>().PlayOneShot(start);
        menuController.Play("AllButtonsAnimRev");
        StartCoroutine (waitUI(2));

    }
    public void mute()
    {
        GameObject.Find("_SoundManagerBackGround").SetActive(false);
    }
    public void Quit()
    {   
        Application.Quit();
    }

    //----- Wait for some time -------//

    IEnumerator waitUI(float sec)
    {
        yield return new WaitForSeconds(sec);

        canvas.SetActive(false);
    }
    IEnumerator TraiColorWait(float sec, GameObject trail)
    {
        yield return new WaitForSeconds(sec);
        GetComponent<TrailRenderer>().material.color = trail.gameObject.GetComponent<Renderer>().material.color;
    }
    IEnumerator DeathTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}