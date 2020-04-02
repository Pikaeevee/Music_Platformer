using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite activeCheckpoint;
    public Sprite inactiveCheckpoint;

    void Start() {
        this.GetComponent<SpriteRenderer>().sprite = inactiveCheckpoint;
    }
	public void setActive() {
        this.GetComponent<SpriteRenderer>().sprite = activeCheckpoint;
        this.GetComponent<ParticleSystem>().Play();
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
