using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    Animator menuController;
    private bool isStartPressed = false;
    // Use this for initialization
    void Start() {
        menuController = GetComponent<Animator>();
        menuController.Play("AllButtonsAnim");
    }

    // Update is called once per frame
    void Update() {
        

}

    public void Play() {
        isStartPressed = true;
        menuController.Play("AllButtonsAnimRev");
    }
}
