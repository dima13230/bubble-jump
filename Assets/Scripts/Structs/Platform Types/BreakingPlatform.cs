using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : Platform
{
    public int interactionsToBreak = 1;
    int interactCount = 0;

    public override void Interact()
    {
        if (interactCount == interactionsToBreak)
            Destroy(gameObject);
        interactCount++;
    }
}
