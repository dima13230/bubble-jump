using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : Platform
{

    public override void Interact()
    {
        Destroy(gameObject);
    }
}
