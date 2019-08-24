using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_bahavior : MonoBehaviour
{

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) print("up");

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) print("rotate right");

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) print("rotate Left");



    }
}
