using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerRenderer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        /**
        * Somehow need to be able to render objects to be in background/foreground
        * based on Y-axis.
        * Solution 1: Dynamically update the Z-Axis of the player as their Y-position changes.
        * Objects need to have their current Z-Axis reflect their current Y-position.
        * The anchoring point of each object needs to be unified.
        * Solution 2: Dynamically update the Order in Layer of the play er as their Y-position changes.
        * Same stuff as solution 1, but for Order in Layer instead of Z-axis.
        * For some reason, the Order in Layer of a 3D model does not seem to render behind objects.
        **/
        // A higher positive number will push the object/player into the foreground.
        // A higher negative number will push the object/player into the background.
    }
}
