using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{


    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private Transform _footTransform;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _climbingLadderSpeed = 3f;

    private PlayerMoverStates _playerState = PlayerMoverStates.moving;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _justStartedClimbing = false;
    private bool _isUpsideDown = false;
    private bool _justStartedChangingGravity = false;



    public PlayerMoverStates moverState => _playerState;
    private int UpsideDownMultiplier => _isUpsideDown ? -1 : 1;
    public bool IsGrounded => Physics.CheckSphere(_footTransform.position, _groundDistance, _groundMask);
    private Vector2 GetInputAxis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        switch (_playerState)
        {
            case PlayerMoverStates.climbing:
                ClimbLadder();
                break;
            case PlayerMoverStates.moving:
                Move();
                break;
            case PlayerMoverStates.changingGravity:
                CheckingIfIsGrounded();
                break;
        }
    }

    private void ClimbLadder()
    {
        float z2 = GetInputAxis.y;
        if (_isUpsideDown) z2 *= -1;
        _velocity.y = _climbingLadderSpeed;

        _controller.Move(_velocity * Time.deltaTime * z2);

        if (IsGrounded && !_justStartedClimbing)
            StopClimbingLadder();

    }

    private void Move()
    {
        if (!_isUpsideDown && IsGrounded && _velocity.y < 0f)
            _velocity.y = -2f;
        else if (_isUpsideDown && IsGrounded && _velocity.y > 0f)
            _velocity.y = 2f;

        Vector2 direction = GetInputAxis;
        float x = direction.x;
        float z = direction.y;

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity * UpsideDownMultiplier) * UpsideDownMultiplier;
        }


        ApplyGravity();

    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void StartClimbingLadder()
    {
        _playerState = PlayerMoverStates.climbing;
        StartCoroutine(SetClimbingLadder());
    }

    private IEnumerator SetClimbingLadder()
    {
        _justStartedClimbing = true;
        yield return new WaitForSeconds(0.2f);
        _justStartedClimbing = false;
    }

    public void StopClimbingLadder()
    {
        _playerState = PlayerMoverStates.moving;
    }

    public void ReverseGravity()
    {
        _velocity.y = 0;
        _gravity *= -1f;
        _isUpsideDown = !_isUpsideDown;
        _playerState = PlayerMoverStates.changingGravity;
        StartCoroutine(SetGravityChange());

    }

    private void CheckingIfIsGrounded()
    {
        if (IsGrounded && !_justStartedChangingGravity)
        {
            _playerState = PlayerMoverStates.moving;
        }

        ApplyGravity();

    }

    private IEnumerator SetGravityChange()
    {
        _justStartedChangingGravity = true;
        yield return new WaitForSeconds(0.2f);
        _justStartedChangingGravity = false;
    }

    public void ReverseVelocity()
    {
        _velocity *= -1;
        if (_playerState == PlayerMoverStates.changingGravity)
            _playerState = PlayerMoverStates.moving;
    }


}

public enum PlayerMoverStates
{
    idle,
    moving,
    climbing,
    changingGravity
}
