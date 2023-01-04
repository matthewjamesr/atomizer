using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject pipe;
    public GameObject bonusRing;
    public GameObject bird;
    public LogicScript logic;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (bird.GetComponent<BirdScript>().birdIsAlive)
            {
                spawnPipe();
                timer = 0;
            }
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        var range = Random.Range(lowestPoint, highestPoint);

        Instantiate(pipe, new Vector3(transform.position.x, range, 0), transform.rotation);

        if (logic.level >= 1)
        {
            if (Random.value > 0.5f)
            {
                Instantiate(bonusRing, new Vector3(transform.position.x, Random.Range(range - 7, range + 7), 0), transform.rotation);
            }
        }
    }
}
