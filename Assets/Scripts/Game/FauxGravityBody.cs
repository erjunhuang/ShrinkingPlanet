using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FauxGravityBody : MonoBehaviour {
	private Rigidbody rb;
	public bool placeOnSurface = false;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        //ȥ��ϵͳ�������Ҷ�����ת
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
	
	void FixedUpdate ()
	{
		if (placeOnSurface)
			Managers.Game.currentFauxGravityAttractor.PlaceOnSurface(rb);
		else
            Managers.Game.currentFauxGravityAttractor.Attract(rb);
	}
}
