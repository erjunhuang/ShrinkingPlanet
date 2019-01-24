using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public GameObject deathEffect;
    private PlayerController playerController;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Meteor")
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
			Managers.Game.GameOver();

            Managers.audioManager.Play("PlayerDeath");

			Destroy(gameObject);
		}
        playerController.isGround = false;
    }
}
