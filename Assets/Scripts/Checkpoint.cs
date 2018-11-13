using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private bool IsActive;

    private AudioSource audiosource;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }


    public void setIsActive(bool value)
    {
        IsActive = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !IsActive)
        Debug.Log("Checkpoint reached!");
        PlayerControls player = collision.GetComponent<PlayerControls>();
        player.SetCurrentCheckpoint(this);
        audiosource.Play();
    }
}
