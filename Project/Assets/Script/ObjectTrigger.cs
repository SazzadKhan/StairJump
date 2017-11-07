using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Cube")

        {

            Debug.Log("Cube detection");
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
