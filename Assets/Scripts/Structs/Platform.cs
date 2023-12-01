using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : InteractableObject
{
    protected void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }
}
