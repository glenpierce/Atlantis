using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int score = 0;
    public int weight = 50;
    public int drag = 5;
    public static float speedNormalizer = 1000.0f;
    public float speed = 4f * speedNormalizer;
    public float normalSpeed = 4f * speedNormalizer;
    public float dashSpeed = 9f * speedNormalizer;
    public float turnSpeed = 3f;
    public IControls Controls;
    public GameState gameState;
    public Animator animator;
    private float _holdCooldownSeconds = 3f;
    private bool _inHoldCooldown = false;
    private float _holdCooldownExpiry;
    //public GameObject myCamera;//var 'camera' is reserved. myCamera instead
    //public SplitScreenCamera myCamera;

    private int _playerNumber;
    public int PlayerNumber
    {
        get { return _playerNumber; }
        set
        {
            _playerNumber = value;
            int realController = value;
            if (gameState.playerControllerMap != null)
            {
                realController = gameState.playerControllerMap[value];
            }
            Controls.SetPlayerNumber(realController-1);
            MakePlayer();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }

    public void StopHolding()
    {
        _inHoldCooldown = true;
        _holdCooldownExpiry = Time.time + _holdCooldownSeconds;
    }

    // Use this for initialization
    void Awake()
    {
        Controls = gameObject.AddComponent<Controls>();
        //myCamera = gameObject.AddComponent<SplitScreenCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inHoldCooldown)
        {
            animator.SetBool("IsHolding", false);

            if (_holdCooldownExpiry > Time.time)
            {
                _inHoldCooldown = false;
            }
        }


        //myCamera.cameraGameObject.transform.position = this.transform.position + new Vector3(0.0f, -1.0f, -3.0f);
    }

    void MakePlayer()
    {
        var material = gameObject.GetComponent<Renderer>().material;
        material.color = Utils.GetNextColor();
        Utils.Log("Player {0} is {1}", PlayerNumber.ToString(), material.color.ToString());

    }

}
