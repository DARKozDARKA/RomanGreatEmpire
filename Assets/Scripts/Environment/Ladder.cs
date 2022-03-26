using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _currentPlayer;
    private void OnTriggerEnter(Collider other)
    {
        _currentPlayer = other.GetComponent<Player>();
    }

    private void Update()
    {
        if(_currentPlayer != null)
            _currentPlayer.StartClimbingLadder();
    }

    private void OnTriggerExit(Collider other)
    {
        _currentPlayer = other.GetComponent<Player>();
        _currentPlayer.StopClimbingLadder();
        _currentPlayer = null;
    }
}
