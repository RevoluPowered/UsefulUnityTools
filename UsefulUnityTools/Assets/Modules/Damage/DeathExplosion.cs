using UnityEngine;
using System.Collections;

/// <summary>
/// On object death spawn an explosion effect.
/// </summary>
[RequireComponent(typeof(DamageBase))]
public class DeathExplosion : MonoBehaviour {

    /// <summary>
    /// The explosion effect prefab.
    /// </summary>
    public GameObject mExplosionEffect;

    /// <summary>
    /// Upon explosion do we want to destroy the object?
    /// </summary>
    public bool mDestroyObject;

    /// <summary>
    /// Local damage base
    /// </summary>
    private DamageBase mDamageBase;

    /// <summary>
    /// Unity start function
    /// </summary>
	void Start () {
        // Retrieve damage base;
        mDamageBase = GetComponent<DamageBase>();
        mDamageBase.DeathEvent += DamageBase_Death;
	}

    private void DamageBase_Death(DamageBase damageBase, float damageAmount, float overDamage, GameObject source)
    {
        // Spawn the explosion
        GameObject deathExplosion = Instantiate<GameObject>(mExplosionEffect);
        deathExplosion.transform.position = transform.position;
        
        // Kill the game object.
        if(mDestroyObject)
        {
            Destroy(gameObject);
        }

        Console.Log("[Explosion] Death event for " + gameObject.name + " with damage amount: " + damageAmount);

    }
}
