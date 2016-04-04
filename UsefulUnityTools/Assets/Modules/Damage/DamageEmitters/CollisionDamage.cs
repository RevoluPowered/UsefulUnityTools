using UnityEngine;
using System.Collections;


/// <summary>
/// The collision damage system, attach this to an object to make impact damage hurt the game object.
/// </summary>
[RequireComponent(typeof(DamageBase))]
public class CollisionDamage : MonoBehaviour {

	/// <summary>
    /// The damage base for the Collider / Collision damage.
    /// </summary>
	DamageBase mDamageBase;

    /// <summary>
    /// The required impact force to trigger damage.
    /// </summary>
	public float mHitDamage = 15.0f;

	/// <summary>
    /// Unity start function / Hook
    /// </summary>
	void Start () {
		mDamageBase = GetComponent<DamageBase> ();
	}

    /// <summary>
    /// When the collision enters.
    /// </summary>
    /// <param name="col"></param>
	void OnCollisionEnter( Collision col )
	{
        // The magnitude in which we hit the object at.
		float mag = col.relativeVelocity.magnitude;

        // If the velocity is higher than the magnitude, damage the object.
		if (mag >= mHitDamage) {
			// TODO: send RPC instead.
			mDamageBase.Damage (mag, this.gameObject );
			Console.Log ("[Collision] damage amount: " + mag);
		} else {
            Console.Log ("[Collision] damage not registered: " + mag);
		}
	}
}
