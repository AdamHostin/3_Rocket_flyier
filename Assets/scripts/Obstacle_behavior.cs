using System;
using UnityEngine;

[DisallowMultipleComponent]

public class Obstacle_behavior : MonoBehaviour
{ 
    [SerializeField] Vector3 ObstacleVector;
    [SerializeField] [Range(-Mathf.PI/2, Mathf.PI/2)] float MovmentOfset = 0.0f;
    [SerializeField] float MovmentSpeed = 2f;
    float MovmentPossition;


    private Vector3 StartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / MovmentSpeed;
        if (Double.IsNaN(cycle) || Double.IsInfinity(cycle)) return;

        MovmentPossition = Mathf.Sin(cycle * 2f * Mathf.PI + MovmentOfset); //output (-1,+1)
        MovmentPossition = MovmentPossition/2 + 0.5f; //output (0,+1)

        Vector3 offset = ObstacleVector * MovmentPossition;
        transform.position = StartingPosition + offset;
    }
}
