using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Best_score_script : MonoBehaviour
{
    private TextMeshProUGUI textMeshComponent;
    private static int Best = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Rocket_bahavior.Score > Best)
        {
            Best = Rocket_bahavior.Score;
        }
        textMeshComponent = GetComponent<TextMeshProUGUI>();
        textMeshComponent.text = "Best: " + Best.ToString();

    }

}