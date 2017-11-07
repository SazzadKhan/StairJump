using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other )
	{
		if (other.gameObject.tag == "obstacles") {
			GameObject.Find ("p1").GetComponent<ChargedJump> ().target = other.transform.position;
            Debug.Log("Front trigger position" + other.transform.position);
		}
	}
}
