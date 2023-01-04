using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    public static float staticMoveSpeed = 10;
    public float dynamicMoveSpeed = 10;
    public float deadzone = -45;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * staticMoveSpeed) * Time.deltaTime;

        if (transform.position.x < deadzone)
        {
            Destroy(gameObject);
        }
    }
}
