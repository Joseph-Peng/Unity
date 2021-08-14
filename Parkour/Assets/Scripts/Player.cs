using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Run,
        Jump,
        DoubleJump,
        Die
    }

    public float jumpPower;
    public Texture[] runTexture;
    public Texture[] jumpTexture;
    public Texture[] doubleJumpTexture;

    private int runIndex;
    private int jumpIndex;
    private int doubleJumpIndex;

    private Renderer playerRender;
    private Vector3 birthPos;
    private Rigidbody rig;
    private GameManager gameManager;

    private PlayerState playerState;

    public float aniPlaySpeed;
    public float time;

    private bool isPlayRun;
    private bool isPlayJump;
    private bool isPlayDoubleJump;     

    void Start()
    {
        gameManager = GameObject.Find("01_Sky").GetComponent<GameManager>();
        rig = GetComponent<Rigidbody>();
        birthPos = this.transform.position;
        playerRender = this.transform.Find("Sprite_Plane").GetComponent<Renderer>();
        rig.Sleep();
        //init();
    }

    public void init()
    {
        playerState = PlayerState.Run;
        this.transform.position = birthPos;
        PlayRun();
        rig.useGravity = true;
        rig.WakeUp();
    }

    void Update()
    {
        if(gameManager.gameState ==GameManager.GameState.Start && playerState != PlayerState.Die)
        {
            CheckIsDie();
            rig.WakeUp();
            time += Time.deltaTime;
            //if (Input.GetKeyDown("space"))
            if (Input.GetMouseButtonDown(0))
            {
                if (playerState == PlayerState.Run)
                {
                    print("Jump");
                    playerState = PlayerState.Jump;
                    transform.GetComponent<Rigidbody>().AddForce(0, jumpPower * 1.5f, 0);
                    gameManager.PlayAudio(2);
                }
                else if (playerState == PlayerState.Jump)
                {
                    print("doubleJump");
                    playerState = PlayerState.DoubleJump;
                    transform.GetComponent<Rigidbody>().AddForce(0, jumpPower, 0);
                    PlayDoubleJump();
                    gameManager.PlayAudio(2);
                }
            }
            if (time >= aniPlaySpeed)
            {
                time = 0;
                if (isPlayRun == true)
                {
                    RunAniing();
                }

                if (isPlayJump == true)
                {
                    JumpAniing();
                }

                if (isPlayDoubleJump == true)
                {
                    print("isPlayDoubleJump");
                    DoubleJumpAniing();
                }
            }
        }
    }

    void PlayRun()
    {
        isPlayRun = true;
        isPlayJump = false;
        isPlayDoubleJump = false;
        runIndex = 0;
    }

    void PlayJump()
    {
        isPlayRun = false;
        isPlayJump = true;
        isPlayDoubleJump = false;
        jumpIndex = 0;
    }

    void PlayDoubleJump()
    {
        isPlayRun = false;
        isPlayJump = false;
        isPlayDoubleJump = true;
        doubleJumpIndex = 0;
    }

    private void RunAniing()
    {
        runIndex += 1;
        if (runIndex >= runTexture.Length)
        {
            runIndex = 0;
        }
        playerRender.material.mainTexture = runTexture[runIndex];
    }

    private void JumpAniing()
    {
        jumpIndex += 1;
        if (jumpIndex >= jumpTexture.Length)
        {
            PlayRun();
            return;
        }
        playerRender.material.mainTexture = jumpTexture[jumpIndex];
    }

    private void DoubleJumpAniing()
    {
        print("DoubleJumpAniing();");
        doubleJumpIndex += 1;
        if (doubleJumpIndex >= doubleJumpTexture.Length)
        {
            PlayRun();
            return;
        }
        playerRender.material.mainTexture = doubleJumpTexture[doubleJumpIndex];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameManager.gameState == GameManager.GameState.Stop)
        {
            return;
        }
        if(playerState==PlayerState.Jump || playerState == PlayerState.DoubleJump)
        {
            playerState = PlayerState.Run;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.gameState == GameManager.GameState.Stop)
        {
            return;
        }
        if (other.CompareTag("coin"))
        {
            gameManager.AddGold(10);
            Destroy(other.gameObject);
        }
    }

    public void CheckIsDie()
    {
        if(transform.position.x<=-17 || transform.position.y <=-8)
        {
            playerState = PlayerState.Die;
            rig.useGravity = false;
            rig.Sleep();
            gameManager.StopGame();
            this.transform.position = birthPos;
        }
    }
}
