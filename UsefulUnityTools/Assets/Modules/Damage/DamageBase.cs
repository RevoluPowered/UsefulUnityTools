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
/// Damage base support class/ref, required for limitation in unity for handling .SendMessage arguments.
/// </summary>
public class DamageParameters
{
	/// <summary>
	/// Please check the description, you should avoid using this if possible but its required for some network related elements if send message is used.
	/// </summary>
	/// <param name="damageAmount">Damage amount.</param>
	/// <param name="source">Source.</param>
	public DamageParameters (float damageAmount, GameObject source)
	{
		m_damageAmount = damageAmount;
		m_source = source;
	}

	public float m_damageAmount;
	public GameObject m_source;
}

/// <summary>
/// The damage base is for handling all types of damage it registers health any damage to the colliders specified  
/// </summary>
public class DamageBase : MonoBehaviour {
	// @TODO Add the following thing.
	///enum DamageDetectionType = { Collisions, Weapon };

	// Use this for initialization
	void Start () {
		mHealth = mStartingHealth;
	}

	/// <summary>
	/// Damage by the damageAmount in the damageStruct, this stores the "source object as well" see above for SendMessage limitation for network usage.
	/// </summary>
	/// <param name="damageStruct">Damage struct.</param>
	public void Damage( DamageParameters dmgParameters )
	{
		// Data pruning.
		Damage (dmgParameters.m_damageAmount, dmgParameters.m_source);
	}


	/// <summary>
	/// Damage the specified damageAmount.
	/// </summary>
	/// <param name="damageAmount">Damage amount.</param>
	public void Damage( float damageAmount, GameObject source )
	{
		if( !mAlive ) return; // We are aleady dead...

		// Check we are not going to kill the DamageBase
		if( mHealth <= damageAmount)
		{
			// The amount we have gone over the tollerance of this health value.
			float overDamage = damageAmount - mHealth;

			// Set the health value to 0 because we can't have a negative health value, that would be stupid.
			mHealth = 0;

			// Kill it.
			mAlive = false;

			// Inform the DamageBase we have died.
			OnDeath( damageAmount, overDamage, source );
		}
		else
		{
			// Deduct the health as the DamageBase is still alive.
			mHealth -= damageAmount;

			// Inform the DamageBase we have been damaged.
			OnDamage( damageAmount, source );
		}

	}


	/// <summary>
	/// Repair the specified repair amount.
	/// Returns if the repair could happen.
	/// </summary>
	/// <param name="repairAmount">Repair amount.</param>	
	public bool Repair( float repairAmount, GameObject source )
	{
		if( mAlive )
		{
			/// GOOD PROGRAMMING PRACTICE
			mHealth = Mathf.Clamp( mHealth + repairAmount, 0, mMaximumHealth );


			// When we initiate a repair we've got to tell the member event handlers.
			OnRepair( repairAmount, source );
			return true;
		}
		return false;
	}

	/// <summary>
	/// Reset the damage base to it's default health.
	/// Reset the damage base alive value so it knows its alive again and can be damaged.
	/// </summary>
	public void Revive( GameObject source)
	{
		mAlive = true;
		mHealth = mStartingHealth;
		OnRevive( this, source );
	}

    /// <summary>
    /// Call revive event delegate
    /// </summary>
    /// <param name="damageBase"></param>
    /// <param name="source"></param>
	private void OnRevive( DamageBase damageBase, GameObject source )
	{
		if( ReviveEvent == null ) return; 
		ReviveEvent( damageBase, source );
	}

    /// <summary>
    /// Repair the object, can be originating from another game object.
    /// </summary>
    /// <param name="repairAmount"></param>
    /// <param name="source"></param>
	private void OnRepair( float repairAmount, GameObject source )
	{
		if( RepairEvent == null ) return; 
		RepairEvent( this, repairAmount, source );
	}


	/// <summary>
	/// This function is called every time the DamageBase is damaged by any kind of damage.
	/// The damage amount is specified by the 
	/// </summary>
	private void OnDamage ( float damageAmount, GameObject source )
	{
		/// The delegate doesn't need to exist when we SHOULD be using "event" types.
		/// This requires a complete overhaul in the tool subsystem as well, but I really don't give a fuck at this point.
		/// It works -> Don't change it until you have to.
		if( DamageEvent == null ) return; 
		DamageEvent( this, damageAmount, source );
	}

	/// <summary>
	/// On the DamageBase's death from any kind of damage this will be called.
	/// The overdamage perameter informs the function of the amount of damage over the maximum health that happened, 
	/// this allows you to correcly specify the amount of velocity or whatever you would like to do if it explodes too much or over a certain tollerance.
	/// </summary>
	private void OnDeath( float damageAmount, float overDamage, GameObject source )
	{
		/// The delegate doesn't need to exist when we SHOULD be using "event" types.
		/// This requires a complete overhaul in the tool subsystem as well, but I really don't give a fuck at this point.
		/// It works -> Don't change it until you have to.

		if( DeathEvent == null ) return;
		DeathEvent( this, damageAmount, overDamage, source );
	}

	// The event handler that passes the events to the sub child objects.
	// Parent Recieves event -> Child has registered with the parent it has a damage handler.
	// This saves pointles computation searching the child, because they specifically register.
	// The parent is not a cunt basically.
	public event GenericDamage DamageEvent;
	public event GenericDeath DeathEvent;
	public event GenericRepair RepairEvent;
	public event GenericRevive ReviveEvent;


	// The source is the node which is executing the damage, repair or revive.
	// These delegates are used to handle the events, they easily let me pull down the required arguments, obviously they're good practice too!
	public delegate void GenericDamage( DamageBase damageBase, float damageAmount, GameObject source );
	public delegate void GenericDeath( DamageBase damageBase, float damageAmount, float overDamage, GameObject source );
	public delegate void GenericRepair( DamageBase damageBase, float repairAmount, GameObject source );
	public delegate void GenericRevive( DamageBase damageBase, GameObject source );

    /// <summary>
    /// Is the object alive?
    /// </summary>
	public bool mAlive;
    /// <summary>
    /// The health value available.
    /// </summary>
	public float mHealth;
    /// <summary>
    /// The starting health
    /// </summary>
	public float mStartingHealth;
    /// <summary>
    /// The maximum health
    /// </summary>
	public float mMaximumHealth;
}
