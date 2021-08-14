using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int GAMESTATE_MENU = 0;
    public static int GAMESTATE_PLAYING = 1;
    public static int GAMESTATE_END = 2;


    public Transform firstBg;
    public int score = 0;
    public int GAMESTATE = GAMESTATE_MENU;

    public static GameManager instance;

    private GameObject bird;

    private void Awake()
    {
        instance = this;
        bird = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(GAMESTATE == GameManager.GAMESTATE_MENU)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GAMESTATE = GameManager.GAMESTATE_PLAYING;
                bird.SendMessage("getLife");
            }
        }
    }
}
