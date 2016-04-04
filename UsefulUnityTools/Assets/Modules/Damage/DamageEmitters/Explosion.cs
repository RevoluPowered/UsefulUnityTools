using UnityEngine;
using System.Collections;

//  ___________             .__               .__                        ___.                                                                /\                                     __  .__      .__  __     
//  \_   _____/__  ________ |  |   ____  _____|__| ____   ____           \_ |__   ____  ____ _____   __ __  ______ ____    ___.__. ____  __ _)/______  ____   __  _  ______________/  |_|  |__   |__|/  |_   
//   |    __)_\  \/  |____ \|  |  /  _ \/  ___/  |/  _ \ /    \   ______  | __ \_/ __ \/ ___\\__  \ |  |  \/  ___// __ \  <   |  |/  _ \|  |  \_  __ \/ __ \  \ \/ \/ /  _ \_  __ \   __\  |  \  |  \   __\  
//   |        \>    <|  |_> >  |_(  <_> )___ \|  (  <_> )   |  \ /_____/  | \_\ \  ___|  \___ / __ \|  |  /\___ \\  ___/   \___  (  <_> )  |  /|  | \|  ___/   \     (  <_> )  | \/|  | |   Y  \ |  ||  |    
//  /_______  /__/\_ \   __/|____/\____/____  >__|\____/|___|  /          |___  /\___  >___  >____  /____//____  >\___  >  / ____|\____/|____/ |__|   \___  >   \/\_/ \____/|__|   |__| |___|  / |__||__| /\ 
//          \/      \/__|                   \/               \/               \/     \/    \/     \/           \/     \/   \/                             \/                                 \/           \/ 

public class Explosion : MonoBehaviour {

	public float m_explosionTime = 10;
	public float m_damageRadius = 10;
	public float m_damageAmount = 5;
	public bool m_exploded = false;
	public int m_ObjectsDamaged = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Stop the counter if we've already run the explosion.
		if(m_exploded) return;

		// Register the time change on this frame.
		m_explosionTime -= Time.deltaTime;
		
		if( m_explosionTime <= 0)
		{
			// Avoid exploding again.
			m_exploded = true;
			
			// Find damagable in sphere.
			Collider[] objectsToDamage = Physics.OverlapSphere( gameObject.transform.position, m_damageRadius);
			
			// Run damage loop.
			foreach( Collider c in objectsToDamage )
			{
				// Send damage amount. *lag?*
				c.SendMessage( "Damage", new DamageParameters(m_damageAmount, gameObject), SendMessageOptions.DontRequireReceiver);
				// Increment damaged objects
				++m_ObjectsDamaged;
			}
			
			Debug.Log ("Completed damage cycle with a total damage of: " + (m_damageAmount * m_ObjectsDamaged));
		}
	}
}
