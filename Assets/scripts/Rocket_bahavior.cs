//https://freesound.org/people/jacksonacademyashmore/sounds/414209/ death sound
//https://freesound.org/people/FunWithSound/sounds/456966/ success fanfare
//https://freesound.org/people/GabrielAraujo/sounds/242501/ success point given

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Rocket_bahavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;


    [FormerlySerializedAs("RotationValue")] [SerializeField] float rotationValue = 80f;
    [FormerlySerializedAs("ThrustValue")] [SerializeField] float thrustValue = 80f;
    [FormerlySerializedAs("ThrustSoundValue")] [SerializeField] float thrustSoundValue = 0.75f;
    [FormerlySerializedAs("Delay")] [SerializeField] float delay = 1f;


    [FormerlySerializedAs("ThrustSound")] [SerializeField] AudioClip thrustSound;
    [FormerlySerializedAs("GameWinSound")] [SerializeField] AudioClip gameWinSound;
    [FormerlySerializedAs("LevelWinSound")] [SerializeField] AudioClip levelWinSound;
    [FormerlySerializedAs("DeathSound")] [SerializeField] AudioClip deathSound;

    [FormerlySerializedAs("DeathEffect")] [SerializeField] ParticleSystem deathEffect;
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] ParticleSystem successEffect;

    enum State { Alive, Dead, ChangingLevel };

    private bool _collissionFlag = true;
    State _currentState;
    public static int Score = 0;






    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _currentState = State.Alive;

    }

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild) Debug_handler();

        if (_currentState != State.Alive) return;

        ThrustHandling();
        RotationHandling();

    }

    private void Debug_handler()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
        }

        if (Input.GetKeyDown(KeyCode.C)) _collissionFlag = !_collissionFlag;
    }

    private void ThrustHandling()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!_audioSource.isPlaying) _audioSource.PlayOneShot(thrustSound, thrustSoundValue);
            _rigidbody.AddRelativeForce(Vector3.up * thrustValue * Time.deltaTime);
            thrustEffect.Play();
        }
        else
        {
            if (_audioSource.isPlaying) _audioSource.Stop();
            thrustEffect.Stop();
        }
    }

    private void RotationHandling()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RocketRotation(-rotationValue);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RocketRotation(rotationValue);
        }
    }

    private void RocketRotation(float rotateThisMuch)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward, rotateThisMuch * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }


    void OnCollisionEnter (Collision collision)
    {
        if (_currentState != State.Alive || _collissionFlag==false) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                _currentState = State.ChangingLevel;
                FinishHandling(levelWinSound);
                break;
            default:
                DeathHandling();
                break;
        }
    }

    private void DeathHandling()
    {
        print("you died");
        _currentState = State.Dead;
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.PlayOneShot(deathSound);
        deathEffect.Play();
        Invoke("LoadScene", delay);
    }

    private void FinishHandling(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
        successEffect.Play();
        Invoke("LoadScene", delay);
    }

    void LoadScene()
    {
        if (_currentState == State.Dead)
        {
            Score = 0;
            SceneManager.LoadScene(0);
        }
        else
        {
            Score++;
            //LoadScene               With (active scene build index +1) mod number of scenes in build setting
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings); // load next scene
        }

        _currentState = State.Alive;
    }

}
