using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //帧计数器
    public int frameCount = 0;
    // 一秒10帧
    public int frameNum = 10;
    //时间计数器
    public float timer = 0;

    public bool canFly = false;
    public bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Rigidbody>().velocity = new Vector3(5,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GAMESTATE == GameManager.GAMESTATE_PLAYING)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / frameNum)
            {
                frameCount++;
                timer -= 1.0f / frameNum;

                //update meterial's offset x
                int frameIndex = frameCount % 3;
                //this.renderer.materials(); 已过时
                this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.333333f * frameIndex, 0));
            }
        }

        if (GameManager.instance.GAMESTATE == GameManager.GAMESTATE_PLAYING)
        {
            if (Input.GetMouseButton(0))
            {
                this.GetComponent<AudioSource>().Play();
                Vector3 vel = this.GetComponent<Rigidbody>().velocity;
                this.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, 4, vel.z);
            }
        }  
    }

    public void getLife()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
    }
}
