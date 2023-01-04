using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRingMoveScript : MonoBehaviour
{

    public static float staticMoveSpeed = 20;
    public float dynamicMoveSpeed = 20;
    public float deadzone = -45;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.addScore(2);
            Destroy(gameObject);
        }
    }
}
