using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


    public Animator anim;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //-----Collider Trigger----//

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("p1"))
        {

            GameObject.Find("p1").GetComponent<ChargedJump>().scoreCounter++;
           
            GameObject.Find("p1").transform.Find("Canvas").GetComponentInChildren<Animator>().SetBool("ShouldPointSpawn", true);
            StartCoroutine("WaitForAnimComplete");

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("p1"))
        {

            Destroy(gameObject);
            

        }


    }



    IEnumerator WaitForAnimComplete()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("p1").transform.Find("Canvas").GetComponentInChildren<Animator>().SetBool("ShouldPointSpawn", false);
        StopCoroutine("WaitForAnimComplete");
    }   
    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("p1").transform.Find("Canvas").GetComponentInChildren<Animator>().SetBool("ShouldPointSpawn", true);
        StopCoroutine("WaitForAnimComplete");
    }

}
