
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_bahavior : MonoBehaviour
{
    new Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rotation_value = 80f;
    [SerializeField] float thrust_value = 80f;
    [SerializeField] float delay = 1f;
    int level_index;
    const int max_level = 2;
    enum state {Alive,Dead,Half_Dead};
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

        if (current_state == state.Dead) return;
        
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
        if (current_state == state.Dead) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":

                if (max_level==level_index) Invoke("LoadScene", delay);
                else {
                    level_index++;
                    Invoke("LoadScene", delay);
                }
                break;
            default:
                print("you died");
                current_state = state.Dead;
                level_index = 0;
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
