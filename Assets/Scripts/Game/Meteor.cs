using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : FauxGravityBody {

    public string MeteorName;
	public GameObject explosion;
	public Collider sphereCol;
	public ParticleSystem trail;

	void OnCollisionEnter(Collision col)
	{
		Quaternion rot = Quaternion.LookRotation(transform.position.normalized);
		rot *= Quaternion.Euler(90f, 0f, 0f);
		Instantiate(explosion, col.contacts[0].point, rot);

		sphereCol.enabled = false;

        if (trail) trail.Stop(true, ParticleSystemStopBehavior.StopEmitting);

		this.enabled = false;
		GetComponent<AudioSource>().Stop();

		Destroy(gameObject, 4f);
	}

}
