using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        player.StartClimbingLadder();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        player.StopClimbingLadder();
    }
}
