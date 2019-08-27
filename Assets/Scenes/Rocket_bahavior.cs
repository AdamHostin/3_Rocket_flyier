using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_bahavior : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        const int rotation_value = 25;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!audioSource.isPlaying) audioSource.Play();
            rigidbody.AddRelativeForce(Vector3.up);
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.back, rotation_value * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rotation_value * Time.deltaTime);



    }
}
