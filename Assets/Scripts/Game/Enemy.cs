using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FauxGravityBody
{
    public GameObject deathEffect;
    public Collider sphereCol;

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Meteor"|| col.collider.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Managers.audioManager.Play("PlayerDeath");
            Destroy(gameObject);
        }
    }
}
