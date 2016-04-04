using UnityEngine;
using System.Collections;

//  ________                                        __________                                                                          
//  \______ \ _____    _____ _____    ____   ____   \______   \____    ______ ____                                                      
//   |    |  \\__  \  /     \\__  \  / ___\_/ __ \   |    |  _|__  \  /  ___// __ \                                                     
//   |    `   \/ __ \|  Y Y  \/ __ \/ /_/  >  ___/   |    |   \/ __ \_\___ \\  ___/                                                     
//  /_______  (____  /__|_|  (____  |___  / \___  >  |______  (____  /____  >\___  >                                                    
//          \/     \/      \/     \/_____/      \/          \/     \/     \/     \/                                                     
// Destroy all the things.

/// <summary>
/// DamageEntity is a system for handling the OnDeath event of an object when damaged.
/// You can specify an effect which runs by applying the explosion prefab.
/// This is simple and effective.
/// </summary>
[RequireComponent(typeof(DamageBase))]
public class DamageEntity : MonoBehaviour {
	/// <summary>
    /// The damage base which is attached to the entity.
    /// </summary>
	private DamageBase mDamageBase;

	/// <summary>
    /// A prefab for when the object dies / explodes.
    /// </summary>
	public GameObject mExplosion;

	/// <summary>
    /// Unity start hook.
    /// </summary>
	void Start () {
		// Find the attached damageBase.
		mDamageBase = GetComponent<DamageBase>();

		// Assign the death delegate to the damage base, as intented.
		mDamageBase.DeathEvent += DeathEvent;
	}

    /// <summary>
    /// Death event delegate usage example
    /// Used to remove the object when it dies
    /// </summary>
    /// <param name="db"></param>
    /// <param name="damage"></param>
    /// <param name="overhealthdamage"></param>
    /// <param name="source"></param>
    void DeathEvent( DamageBase db, float damage, float overhealthdamage, GameObject source )
	{
		// Destroy this gameobject.
		Rigidbody rb = gameObject.AddComponent<Rigidbody> ();

		// This s
		rb.mass = 100; // I know this is bad, eventually I will update this to something like:::: val =  parent objects mass / child count (first layer)

		// Apply physical velocity relating to the source of the explosion or damage.

		// If the overdamage isn't high enough to multiply the standard damage by, clamp the value to a minimum of 1, but make sure to not clamp it to a maximum value, because that wouldn't make things interesting.
		float overDamageMultiplier = (overhealthdamage > 1.0f ? overhealthdamage : 1);

		// TODO: add base class to make sure we're not relying upon this, we need to be specifying the explosion value IN the damage function, instead of requiring overhead with an expensive base class this potentially would use more memory.
		Explosion explosionValue = source.GetComponent<Explosion> ();

		// The damage * overdamage, beasically stopping the force being too low and also giving us some interesting force effects in the game.
		//float calculatedDamage = damage * overDamageMultiplier;

		// The force multiplier is calculated based upon a min value, this makes sure we at least do the force of a basic level explosive, this can be changed.
		// Be warned this causes major effects in the game, changing this value should be noted to the players. * ALWAYS *.
		//float forceMultiplier = (calculatedDamage > 125 ? calculatedDamage : 125);

		// Phyiscally simulate the explosion on the object, this is awesome!
		rb.AddExplosionForce (damage * overDamageMultiplier, source.transform.position, explosionValue.mDamageRadius);

		// Run the explosion.
		GameObject tr = (GameObject)Instantiate (mExplosion, transform.position, Quaternion.identity);
       
        // Reset pos, and align to new parent.
        tr.transform.SetParent(transform, false);
		//DestroyObject (gameObject,10);

	}
}
