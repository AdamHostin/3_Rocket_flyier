
using UnityEngine;
using TMPro;

public class Score_script : MonoBehaviour
{
    private TextMeshProUGUI textMeshComponent;


    // Start is called before the first frame update
    void Start()
    {
        textMeshComponent = GetComponent<TextMeshProUGUI>();
        textMeshComponent.text = "Score: " + Rocket_bahavior.Score.ToString();

    }

}