using UnityEngine;
using System.Collections;

// Analysis disable once CheckNamespace
public class CollisionDamage : MonoBehaviour {

	// Damage base.
	DamageBase mDamageBase;

	public float mHitDamage = 15.0f;

	// Use this for initialization
	void Start () {
		mDamageBase = (DamageBase)gameObject.GetComponent<DamageBase> ();
	}

	void OnCollisionEnter( Collision col )
	{
		float mag = col.relativeVelocity.magnitude;

		if (mag >= mHitDamage) {
			// TODO: send RPC instead.
			mDamageBase.Damage (mag, this.gameObject );
			Console.Log ("[Collision] damage amount: " + mag);
		} else {
            Console.Log ("[Collision] damage not registered: " + mag);
		}
	}
}
