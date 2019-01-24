using System.Collections.Generic;
using UnityEngine;
using YanlzFramework;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
	public float rotationSpeed = 10f;

	private Rigidbody rb;
    public PlayStatus playStatus = PlayStatus.idle;
    public bool isGround = false;

    public bool isAI = false;
    private float startTime = 0f;
    private float intervalTime = 3f;
    void Start ()
	{
		rb = GetComponent<Rigidbody>();
        playStatus = PlayStatus.idle;
    }
    public void move(Vector3 direction,float fixedDeltaTime) {
        rb.MovePosition(rb.position + transform.TransformDirection(direction) * moveSpeed* fixedDeltaTime);
    }
    public void rotation(float rotation, float fixedDeltaTime) {
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed* fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * fixedDeltaTime));
    }

    public void Jump()
    {
        this.isGround = true;
        rb.AddForce(Vector3.up * 50f);
    }
    //AI
    void FixedUpdate()
    {
        if (isAI)
        {
            move(Vector3.forward, Time.fixedDeltaTime);

            startTime += Time.fixedDeltaTime;
            if (startTime > intervalTime)
            {
                rotation(Random.Range(-1,1), Time.fixedDeltaTime);
                if (startTime > intervalTime*1.5f)
                    startTime = 0;
            }
        }
    }
}
