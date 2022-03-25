using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerRotator))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private bool _canChangeGravity = true;



    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
        _playerRotator.OnGravityStopReversing += SetReverseGravityAvailible;
    }

    private void OnDisable()
    {
        _playerRotator.OnGravityStopReversing -= SetReverseGravityAvailible;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ReverseGravity();

    }

    public void Init(Camera camera)
    {
        _playerRotator.Init(camera);
    }

    public void ReverseGravity()
    {
        if (!_canChangeGravity || !_playerMover.IsGrounded || _playerMover.moverState != PlayerMoverStates.moving) return;
        _playerMover.ReverseGravity();
        _playerRotator.ReverseGravity();
        _canChangeGravity = false;
    }
    public void StartClimbingLadder()
    {
        _playerMover.StartClimbingLadder();
    }

    public void StopClimbingLadder()
    {
        _playerMover.StopClimbingLadder();
    }

    private void SetReverseGravityAvailible()
    {
        _canChangeGravity = true;
    }
}
