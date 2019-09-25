//https://freesound.org/people/jacksonacademyashmore/sounds/414209/ death sound
//https://freesound.org/people/FunWithSound/sounds/456966/ success fanfare
//https://freesound.org/people/GabrielAraujo/sounds/242501/ success point given
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_bahavior : MonoBehaviour
{
    new Rigidbody rigidbody;
    AudioSource audioSource;

    
    [SerializeField] float RotationValue = 80f;
    [SerializeField] float ThrustValue = 80f;
    [SerializeField] float ThrustSoundValue = 0.75f;
    [SerializeField] float Delay = 1f;


    [SerializeField] AudioClip ThrustSound;
    [SerializeField] AudioClip GameWinSound;
    [SerializeField] AudioClip LevelWinSound;
    [SerializeField] AudioClip DeathSound;

    [SerializeField] ParticleSystem DeathEffect;
    [SerializeField] ParticleSystem ThrustEffect;
    [SerializeField] ParticleSystem SuccessEffect;

    static int LevelIndex =0;
    const int MaxLevelIndex = 2;
    enum state {Alive,Dead,ChangingLevel};
    state CurrentState;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        CurrentState = state.Alive;
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentState != state.Alive) return;

        ThrustHandling();
        RotationHandling();

    }

    private void ThrustHandling()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(ThrustSound, ThrustSoundValue);
            rigidbody.AddRelativeForce(Vector3.up * ThrustValue);
            ThrustEffect.Play();
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            ThrustEffect.Stop();
        }
    }

    private void RotationHandling()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.back, RotationValue * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, RotationValue * Time.deltaTime);

        rigidbody.freezeRotation = false;
    }

    void OnCollisionEnter (Collision collision)
    {
        if (CurrentState != state.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                CurrentState = state.ChangingLevel;
                
                if (MaxLevelIndex == LevelIndex)
                {
                    FinishHandling(GameWinSound);
                }
                else
                {
                    LevelIndex++;
                    FinishHandling(LevelWinSound);
                }
                break;
            default:
                DeathHandling();
                break;
        }
    }

    private void DeathHandling()
    {
        print("you died");
        CurrentState = state.Dead;
        LevelIndex = 0;
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(DeathSound);
        DeathEffect.Play();
        Invoke("LoadScene", Delay);
    }

    private void FinishHandling(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
        SuccessEffect.Play();
        Invoke("LoadScene", Delay);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(LevelIndex);
        CurrentState = state.Alive;
    }

}
