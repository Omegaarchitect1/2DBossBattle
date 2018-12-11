using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {

    private AudioSource audiosource;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            audiosource.Play();
            PlayerControls player = collision.GetComponent<PlayerControls>();
            player.isDead = true;
            player.anim.SetBool("isDead", player.isDead);
           // player.Respawn();
            //player.isDead = false;
           // player.anim.SetBool("isDead", player.isDead);

        }
    }
}
