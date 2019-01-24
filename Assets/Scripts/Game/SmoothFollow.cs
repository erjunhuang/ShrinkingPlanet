using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

	private Transform target;

	public float smoothness = 0.06f;
	public float rotationSmoothness = 60f;

	public Vector3 offset;
	private Vector3 velocity = Vector3.zero;

    public Transform point;
    private void Start()
    {
        //��ȡ���
        target = Managers.Game.currentPlayer.transform;
    }

    void FixedUpdate () {

		if (target == null)
		{
			return;
		}
        //λ��
        Vector3 newPos = target.TransformDirection(offset);
		transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothness);
        //��ת
        Quaternion targetRot = Quaternion.LookRotation(-transform.position.normalized, target.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotationSmoothness);
    }
}
