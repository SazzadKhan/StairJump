using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenarator : MonoBehaviour {

    // Use this for initialization
    public GameObject p1;
	public GameObject[] obj = new GameObject[3]; // three game object for three grid

   // public static ColorChange;

    public Transform[] brick;
	public int[] choice = {1,0};// Grid To x axis

	private int result;
    private int finalresult;
	void Start ()
    {
		       
                Transform test2 = Instantiate(brick [2], new Vector3(5, 5, -20), Quaternion.identity);
                test2.localScale = new Vector3(5, 1, 5);
                Transform test3 = Instantiate(brick [0], new Vector3(0, 10, -20), Quaternion.identity);
                test3.localScale = new Vector3(3, 1, 3);
                Transform test4 = Instantiate(brick [1], new Vector3(-5, 15, -20), Quaternion.identity); // First four default Platform
                test4.localScale = new Vector3(5, 1, 5);

    }
	
	// Update is called once per frame
	void Update () {
		int length= choice.Length; // get choice array length 
		int randIndex = Random.Range(0,length); 
	    result = choice[randIndex];
        
        transform.position = transform.position + new Vector3(0, .1f, 0);


    }

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "obstacles") {
            Vector3 position = new Vector3(p1.transform.position.x + result / 2, other.gameObject.transform.position.y + UnityEngine.Random.Range(7, 8), -20);

            Debug.Log("Position from genarator"+position);
            Instantiate (obj[ Random.Range (0, 3)],position, Quaternion.identity);
            
        }

	}


}
