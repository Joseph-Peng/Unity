using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Stop
    }

    public GameObject[] runway;
    public GameObject aZone; 
    public GameObject bZone;

    private Vector3 aZonePos;
    private Vector3 bZonePos;

    public Button startBtn;
    public Text GoldText;
    public Text MeterText;
    public Transform score;
    public int speed;
    private float time;
    public GameState gameState = GameState.Stop;
    public float skyOffset;
    private Player player;

    private int gold;
    private int meter;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        aZonePos = aZone.transform.position;
        bZonePos = bZone.transform.position;
        player = GameObject.Find("03_Player").GetComponent<Player>();
        
    }

    public void AddGold(int count)
    {
        gold += count;
        GoldText.text = gold.ToString();
        PlayAudio(0);
    }

    public void AddMeter(int count)
    {
        meter += count;
        MeterText.text = meter.ToString();
    }

    private void StartGame()
    {
        if(gameState == GameState.Stop)
        {
            gameState = GameState.Start;
            startBtn.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            player.init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Start)
        {
            Move();
            time += Time.deltaTime;
            if (time >= 0.2f)
            {
                AddMeter(1);
                time = 0;
            }
        }
    }

    private void Move()
    {
        aZone.transform.Translate(Vector3.left * speed * Time.deltaTime,Space.World);
        bZone.transform.Translate(Vector3.left * speed * Time.deltaTime,Space.World);
        if(bZone.transform.position.x <= aZonePos.x)
        {
            Destroy(aZone);
            Make();
        }
        transform.GetComponent<Renderer>().material.mainTextureOffset+=new Vector2(skyOffset*Time.deltaTime,0);
    }

    private void Make()
    {
        aZone = bZone;
        bZone = GameObject.Instantiate(runway[UnityEngine.Random.Range(0, runway.Length)]);
        bZone.transform.position = bZonePos;
    }

    public void StopGame()
    {
        if(gameState == GameState.Start)
        {
            gameState = GameState.Stop;
            score.gameObject.SetActive(true);
            score.transform.Find("FinalGold").GetComponent<Text>().text = gold.ToString();
            score.transform.Find("FinalMeter").GetComponent<Text>().text = meter.ToString();
            Destroy(aZone);
            Destroy(bZone);
            aZone = GameObject.Instantiate(runway[runway.Length - 1]);
            bZone = GameObject.Instantiate(runway[runway.Length - 1]);
            aZone.transform.position = aZonePos;
            bZone.transform.position = bZonePos;

            gold = 0;
            meter = 0;
            GoldText.text = gold.ToString();
            MeterText.text = meter.ToString();
            startBtn.gameObject.SetActive(true);
            transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0.01f);
            PlayAudio(1);
        }
    }

    public void PlayAudio(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
