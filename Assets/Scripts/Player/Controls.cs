using System.Collections;
using UnityEngine;

public interface IControls
{
    void SetPlayerNumber(int number);
}

public class Controls : MonoBehaviour, IControls
{
    private string _moveXAxisName;
    private string _moveYAxisName;
    private string _lookXAxisName;
    private string _lookYAxisName;
    private string _aButtonAxisName;
    private string _bButtonAxisName;
    private string _dashButtonAxisName;
    private int _playerNumber;
    private Player _player;

    private float _moveX;
    private float _moveY;
    private float _lookX;
    private float _lookY;
    private float _aButton;
    private float _bButton;
    private float _dashButtonInputValue;

    public float _rotationY;
    public float _rotationX;

    public bool _isDashing;
    private float _dashFuel = 12;
    private float _nextInkTime;

    private bool _isSwimming;

    public static readonly int MaxDashFuel = 12;
    public static readonly int DashFuelConsumeRate = 1;
    public static readonly int DashFuelReplenishRate = 1;
    public static readonly float InkIntervalSeconds = 0.25f;

    private bool init = false;
    Rigidbody myRigidBody;
    private Animator _animator;

    public void Start()
    {
        myRigidBody = GetComponentInParent<Rigidbody>();

        myRigidBody.useGravity = true;
        myRigidBody.drag = _player.drag;
    }

    public void SetPlayerNumber(int number)
    {
        Debug.Log("setting player number for controls " + number);
        _playerNumber = number;
        _moveXAxisName = string.Format("Player{0}MoveX", _playerNumber);
        _moveYAxisName = string.Format("Player{0}MoveY", _playerNumber);
        _lookXAxisName = string.Format("Player{0}LookX", _playerNumber);
        _lookYAxisName = string.Format("Player{0}LookY", _playerNumber);
        _aButtonAxisName = string.Format("Player{0}AButton", _playerNumber);
        _bButtonAxisName = string.Format("Player{0}BButton", _playerNumber);
        _dashButtonAxisName = string.Format("Player{0}RBumper", _playerNumber);
        _player = gameObject.GetComponent<Player>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        init = true;
    }

    public void Update()
    {
        myRigidBody.mass = _player.weight;
    }

    public void FixedUpdate()
    {
        if (!init)
        {
            return;
        }

        UpdateInput();

        if (_isDashing)
        {
            _player.speed = _player.dashSpeed;
        }
        else
        {
            _player.speed = _player.normalSpeed;
        }


        var xMovement = _moveX;
        var zMovement = _moveY;
        var yMovement = (_aButton) - (_bButton);
        var movementVector = new Vector3(xMovement, yMovement, zMovement) * _player.speed;
        myRigidBody.AddRelativeForce(movementVector);

        _isSwimming = (xMovement != 0) || (zMovement != 0);


        _animator.SetBool("IsSwimming", _isSwimming && !_isDashing);
        _animator.SetBool("IsDashing", _isDashing);

        _rotationX = _rotationX + (_lookY * _player.turnSpeed);
        _rotationY = _rotationY + (_lookX * _player.turnSpeed);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY);

        //Debug.Log(myRigidBody.velocity);
        //Debug.Log(myRigidBody.velocity.magnitude);
        

        HandleDash();
    }

    private void HandleDash()
    { 
        // if the button is pressed
        if (DashButtonPressed())
        {
            // if we have Fuel left
            if (_dashFuel > 0)
            {
                _isDashing = true;
                _dashFuel -= Time.deltaTime * DashFuelConsumeRate;
                FartOutInk();
            }
            else
            {
                _isDashing = false;
                _dashFuel = 0;
            }

        }
        else
        {
            _isDashing = false;
            _dashFuel += Time.deltaTime * DashFuelReplenishRate;
            if (_dashFuel > MaxDashFuel)
            {
                _dashFuel = MaxDashFuel;
            }
        }
	}

    private void FartOutInk()
    {
        if (Time.time > _nextInkTime)
        {
            _nextInkTime = Time.time + InkIntervalSeconds;
            var ink = GameObject.Instantiate(Resources.Load("Ink")) as GameObject;
            ink.transform.position = transform.position;
        }
    }

    private bool DashButtonPressed()
    {
        return _dashButtonInputValue > 0;
    }

    private void UpdateInput()
    {
        _moveX = Input.GetAxis(_moveXAxisName);
        _moveY = Input.GetAxis(_moveYAxisName);
        _lookX = Input.GetAxis(_lookXAxisName);
        _lookY = Input.GetAxis(_lookYAxisName);
        _aButton = Input.GetAxis(_aButtonAxisName);
        _bButton = Input.GetAxis(_bButtonAxisName);
        _dashButtonInputValue = Input.GetAxis(_dashButtonAxisName);
    }
}