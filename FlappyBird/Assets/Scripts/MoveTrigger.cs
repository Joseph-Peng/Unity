using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public Transform currentBG;
    public Pipe pipe1;
    public Pipe pipe2;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // move bg to the front of first transform
            //1. get first transform
            Transform firstBG =  GameManager.instance.firstBg;
            // 2. move
            currentBG.position = new Vector3(firstBG.position.x+10, currentBG.position.y, currentBG.position.z);

            GameManager.instance.firstBg = currentBG;
            pipe1.RandomGeneratePosition();
            pipe2.RandomGeneratePosition();
        }
    }
}
