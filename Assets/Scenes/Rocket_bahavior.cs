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
        const int rotation_value = 25;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) rigidbody.AddRelativeForce(Vector3.up);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.back, rotation_value * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rotation_value * Time.deltaTime);



    }
}
