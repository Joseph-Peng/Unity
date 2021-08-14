using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellowBird : MonoBehaviour
{
    private GameObject bird;
    private Transform birdTransform;
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player");
        birdTransform = bird.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 birdPos = birdTransform.position;
        float y = birdPos.y - 2.4f;
        if (y > 2.4f)
        {
            y = 2.4f;
        }
        if (y < -2.4f)
        {
            y = -2.4f;
        }
        this.transform.position = new Vector3(birdPos.x+5.98f,y,-10);
    }
}
