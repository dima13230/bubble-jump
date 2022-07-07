using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    public enum MovementDirection
    {
        Horizontal,
        Vertical
    }

    public MovementDirection direction = MovementDirection.Horizontal;
    public float MovementSpeed = 2;
    public float MovementAmplitude = 3;

    Vector3 initialPosition;
    Vector3 currentOffset = Vector2.zero;

    int individualMovementSeed;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        individualMovementSeed = Random.Range(-10, 10);
        direction = (MovementDirection)Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == MovementDirection.Horizontal)
            currentOffset.x = Mathf.Sin(Time.time * MovementSpeed + individualMovementSeed) * MovementAmplitude;
        else
            currentOffset.y = Mathf.Sin(Time.time * MovementSpeed + individualMovementSeed) * MovementAmplitude;

        transform.position = initialPosition + currentOffset;
    }
}
