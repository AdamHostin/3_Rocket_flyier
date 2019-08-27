using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_bahavior : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rotation_value = 80f;
    [SerializeField] float thrust_value = 80f;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!audioSource.isPlaying) audioSource.Play();
            rigidbody.AddRelativeForce(Vector3.up * thrust_value);
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.back, rotation_value * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rotation_value * Time.deltaTime);

        rigidbody.freezeRotation = false;
    }

    void OnCollisionEnter (Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                break;
            default:
                print("you died");
                break;
        }
    }

}
