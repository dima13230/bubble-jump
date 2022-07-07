using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, GameManager.instance.MaxPlayerHeight, -10);
    }
}
