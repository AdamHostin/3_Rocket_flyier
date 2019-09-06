//https://freesound.org/people/jacksonacademyashmore/sounds/414209/ death sound
//https://freesound.org/people/FunWithSound/sounds/456966/ success fanfare
//https://freesound.org/people/GabrielAraujo/sounds/242501/ success point given
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_bahavior : MonoBehaviour
{
    new Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rotation_value = 80f;
    [SerializeField] float thrust_value = 80f;
    [SerializeField] float thrust_sound_value = 0.75f;
    [SerializeField] float delay = 1f;

    [SerializeField] AudioClip Thrust_sound;
    [SerializeField] AudioClip Game_win_sound;
    [SerializeField] AudioClip Level_win_sound;
    [SerializeField] AudioClip Death_sound;

        int level_index;
    const int max_level = 2;
    enum state {Alive,Dead,ChangingLevel};
    state current_state;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        level_index = 0;
        current_state = state.Alive;
    }

    // Update is called once per frame
    void Update()
    {

        if (current_state != state.Alive) return;

        ThrustHandling();
        RotationHandling();

    }

    private void ThrustHandling()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(Thrust_sound, thrust_sound_value);
            rigidbody.AddRelativeForce(Vector3.up * thrust_value);
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }

    private void RotationHandling()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.back, rotation_value * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rotation_value * Time.deltaTime);

        rigidbody.freezeRotation = false;
    }

    void OnCollisionEnter (Collision collision)
    {
        if (current_state != state.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                current_state = state.ChangingLevel;                
                if (max_level == level_index)
                {
                    audioSource.PlayOneShot(Game_win_sound);
                    Invoke("LoadScene", delay);
                }
                else
                {
                    level_index++;
                    audioSource.PlayOneShot(Level_win_sound);
                    Invoke("LoadScene", delay);
                }
                break;
            default:
                print("you died");
                current_state = state.Dead;
                level_index = 0;
                if (audioSource.isPlaying) audioSource.Stop();
                audioSource.PlayOneShot(Death_sound);
                Invoke("LoadScene", delay);

                break;
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene(level_index);
        current_state = state.Alive;
    }

}
