using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WireController : MonoBehaviour
{
	void Start()
	{
	}

	void Update()
	{

	}

	public List<WireValueInput> mInputs = new List<WireValueInput>();
	public List<WireValueOutput> mOutputs = new List<WireValueOutput>();
}

//
//
//
//
//
///// <summary>
///// Standard WireIO Connection Controller.
///// This is used to dynamically pass values to entities and control them like wiring.
///// This works very well for visually designing things like circuits or contraptions.
///// It could even be used to networking in a game.
///// </summary>
//public class WireController : MonoBehaviour
//{
//
//	public List<WireIO.Input> m_inputs;
//	public List<WireIO.Output> m_outputs;
//	
//	
//	// Use this for initialization
//	void Start () {
//		m_inputs = new List<WireIO.Input>();
//		m_outputs = new List<WireIO.Output>();
//		
//		
//		Debug.Log ("-------- WireIO Complete Test... ------------");
//		Debug.Log ("Creating Input: \"My Input Test\".");
//		
//		// Create some test inputs or whatever...
//		
//		WireIO.Input testInput = new WireIO.Input( "My Input Test", true );
//		
//		// Output Pre-Creation
//		Debug.Log ("Creating Output: \"My Output Test\"." );
//		
//		WireIO.Output testOutput = new WireIO.Output( "My Output Test", false );
//		
//		// Pre Print Values
//		
//		Debug.Log("Value of Output: " + testOutput.m_val );
//		Debug.Log("Value of Input: " + testInput.m_val );
//		
//		Debug.Log("Connecting the output to the input...");
//		// Show Current Output.
//		testOutput.ConnectInput( testInput );
//		
//		Debug.Log("Connected...");
//		Debug.Log("Value of Output: " + testOutput.m_val );
//		Debug.Log("Value of Input: " + testInput.m_val );
//		
//		
//		
//	}
//	
//	void Update () 
//	{
//		// Update all inputs cached as changed from last tick.
//		// It's better to do this first as if output values update we could miss a value from a previous tick!
//		foreach( WireIO.Input input in m_inputs )
//		{
//			if( input.OnChanged != null )
//			{
//				input.OnChanged( input.m_ConnectedOutput, input.m_lastValue );
//			}
//			
//			// Avoid triggering this too many times, once is enough - you know...
//			// When the value is actually changed.			
//			input.m_changed = false;
//		}
//		
//		// Update all outputs.
//		foreach( WireIO.Output output in m_outputs )
//		{
//			// :S Should work.
//			if( output.OnChanged != null )
//			{
//				output.OnChanged( output.m_lastValue );
//			}
//			
//			// Make sure the trigger is not invoked at the wrong time.
//			output.m_changed = false;
//		}
//		
//		
//		///
//		/// This will actually be seperate IRG.
//		///
//		
//		
//	}
//	
//	/// <summary>
//	/// Connects the input to the specified output.
//	/// </summary>
//	/// <returns>
//	/// The input.
//	/// </returns>
//	/// <param name='output'>
//	/// If set to <c>true</c> output.
//	/// </param>
//	bool ConnectInput( string inputName, WireIO.Output output )
//	{
//		return false;
//	}
//	
//	/// <summary>
//	/// Disconnects the input from the output its connected to.
//	/// </summary>
//	/// <param name='input'>
//	/// Input.
//	/// </param>
//	void DisconnectInput( string inputName )
//	{
//		
//	}
//	
//	/// <summary>
//	/// Disconnects all inputs from the output.
//	/// </summary>
//	/// <param name='output'> 
//	/// Output.
//	/// </param>
//	void DisconnectAllInputs( string input)
//	{
//		
//	}
//}
