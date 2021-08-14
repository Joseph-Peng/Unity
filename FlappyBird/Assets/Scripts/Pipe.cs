using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private void Start()
    {
        RandomGeneratePosition();
    }
    // y -0.1 to -0.4
    public void RandomGeneratePosition()
    {
        float pos_y = Random.Range(-0.4f,-0.1f);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,pos_y,this.transform.localPosition.z);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            this.GetComponent<AudioSource>().Play();
            GameManager.instance.score++;
        }
    }

    public void OnGUI()
    {
        GUILayout.Label("Score: "+ GameManager.instance.score);
    }

}
