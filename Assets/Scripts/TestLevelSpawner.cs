using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelSpawner : MonoBehaviour
{
    public int FieldSize = 50;

    private GameObject _cubeParent;

	// Use this for initialization
	void Start () {
        _cubeParent = new GameObject("Cubes");
        
        var player = new GameObject("Player");
	    player.AddComponent<Player>();


	    for (int x = -FieldSize; x < FieldSize; x+=10)
	    {
	        for (int y = -FieldSize; y < FieldSize; y+=10)
	        {
                for( int z = -FieldSize; z < FieldSize; z+=10 )
                {
                    var position = new Vector3(x, y, z);
                    if (position == Vector3.zero)
                    {
                        continue;
                    }

                    SpawnCube(new Vector3(x, y, z));
                }
            }
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnCube(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(_cubeParent.transform);
        cube.transform.position = position;
    }
}
