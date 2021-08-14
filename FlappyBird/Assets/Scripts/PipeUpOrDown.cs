using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeUpOrDown : MonoBehaviour
{

    public AudioSource hitMusic;
    public AudioSource dieMusic;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hitMusic.Play();
            dieMusic.Play();
            GameManager.instance.GAMESTATE = GameManager.GAMESTATE_END;
        }
    }
}
