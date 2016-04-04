using UnityEngine;
using System.Collections;

//  ___________             .__               .__                        ___.                                                                /\                                     __  .__      .__  __     
//  \_   _____/__  ________ |  |   ____  _____|__| ____   ____           \_ |__   ____  ____ _____   __ __  ______ ____    ___.__. ____  __ _)/______  ____   __  _  ______________/  |_|  |__   |__|/  |_   
//   |    __)_\  \/  |____ \|  |  /  _ \/  ___/  |/  _ \ /    \   ______  | __ \_/ __ \/ ___\\__  \ |  |  \/  ___// __ \  <   |  |/  _ \|  |  \_  __ \/ __ \  \ \/ \/ /  _ \_  __ \   __\  |  \  |  \   __\  
//   |        \>    <|  |_> >  |_(  <_> )___ \|  (  <_> )   |  \ /_____/  | \_\ \  ___|  \___ / __ \|  |  /\___ \\  ___/   \___  (  <_> )  |  /|  | \|  ___/   \     (  <_> )  | \/|  | |   Y  \ |  ||  |    
//  /_______  /__/\_ \   __/|____/\____/____  >__|\____/|___|  /          |___  /\___  >___  >____  /____//____  >\___  >  / ____|\____/|____/ |__|   \___  >   \/\_/ \____/|__|   |__| |___|  / |__||__| /\ 
//          \/      \/__|                   \/               \/               \/     \/    \/     \/           \/     \/   \/                             \/                                 \/           \/ 

/// <summary>
/// An explosion script which causes damage to any entities within the explosion radius.
/// Specify the damage amount and it will explode within range of an object.
/// </summary>
public class Explosion : MonoBehaviour {
    /// <summary>
    /// The explosion time in seconds.
    /// </summary>
	public float mExplosionTime = 10;
    /// <summary>
    /// The damage radius.
    /// </summary>
	public float mDamageRadius = 10;
    /// <summary>
    /// The damage amount in HP.
    /// </summary>
	public float mDamageAmount = 5;
    /// <summary>
    /// Has the object already exploded?
    /// </summary>
	public bool mExploded = false;
    /// <summary>
    /// How many objects / with DamageBases were damaged, used for debugging.
    /// </summary>
	public int mObjectDamagedCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Stop the counter if we've already run the explosion.
		if(mExploded) return;

		// Register the time change on this frame.
		mExplosionTime -= Time.deltaTime;
		
		if( mExplosionTime <= 0)
		{
			// Avoid exploding again.
			mExploded = true;
			
			// Find damagable in sphere.
			Collider[] objectsToDamage = Physics.OverlapSphere( gameObject.transform.position, mDamageRadius);
			
			// Run damage loop.
			foreach( Collider c in objectsToDamage )
			{
				// Send damage amount. *lag?*
				c.SendMessage( "Damage", new DamageParameters(mDamageAmount, gameObject), SendMessageOptions.DontRequireReceiver);
				// Increment damaged objects
				++mObjectDamagedCount;
			}
			
			Debug.Log ("Completed damage cycle with a total damage of: " + (mDamageAmount * mObjectDamagedCount));
		}
	}
}
