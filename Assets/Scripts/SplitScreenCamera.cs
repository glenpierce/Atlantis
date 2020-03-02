using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour {

    public GameObject cameraGameObject;
    public Player player;
    public Vector3 playerPosition;
    public Rigidbody myRigidBody;
    public Controls controls;

	// Use this for initialization
	void Start () {
        this.player = gameObject.GetComponent<Player>();
        playerPosition = player.transform.position;
        cameraGameObject.transform.position = transform.position + new Vector3(0f, 1.3f, -4f);

        myRigidBody = gameObject.GetComponent<Rigidbody>();
        controls = gameObject.GetComponent<Controls>();
	}

    float index;
    float amplitudeX = 0.1f;
    float amplitudeY = 0.25f;
    float omegaX = 1.0f;
    float omegaY = 2.0f;
    float gradual = 1;

	// Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        float playerVelocity = myRigidBody.velocity.magnitude;

        amplitudeX = .1f * playerVelocity / 25f + 0.1f;
        amplitudeY = .5f * playerVelocity / 25f + 0.25f;
        

        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = amplitudeY * Mathf.Sin(omegaY * index);
        Vector3 offset = new Vector3(x, y + 1.3f, -4f);

        this.cameraGameObject.transform.localPosition = offset;

        playerPosition = player.transform.position;
    }

    public GameObject AddPlayerCamera(int playerNumber, GameObject player, int totalPlayers)
    {
        cameraGameObject = (GameObject)Instantiate(Resources.Load("EmptyCameraObject"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        var camera = cameraGameObject.GetComponent<Camera>();

        var cameraRect = camera.rect;
        if (playerNumber == 0)
        {
            cameraRect.x = 0.0f;
            cameraRect.y = 0.5f;
        }
        else if (playerNumber == 1)
        {
            cameraRect.x = 0.5f;
            cameraRect.y = 0.5f;
        }
        else if (playerNumber == 2)
        {
            cameraRect.x = 0.0f;
            cameraRect.y = 0.0f;
        }
        else if (playerNumber == 3)
        {
            cameraRect.x = 0.5f;
            cameraRect.y = 0.0f;
        }

        cameraRect.width = 0.5f;
        cameraRect.height = 0.5f;
        camera.rect = cameraRect;


        //p.myCamera = camera;
        Debug.Log("creating camera");
        return cameraGameObject;
    }
}
