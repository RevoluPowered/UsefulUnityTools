using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WireIO;

/// <summary>
/// This is an output value designed to allow you to output values from an entity.
/// The standard behaviour for this Output system is that you can connect as many inputs as you like to outputs.
/// </summary>
public class WireValueOutput : BaseIO
{
	public WireValueOutput( string name, object value ) : base( name, value )
	{
	}

	bool FindInput( WireValueInput input )
	{
		try 
		{
			foreach( WireValueInput inp in mConnectedInputs )
			{
				if( input == inp )
				{
					return true;
				}
			}
		}
		catch(System.Exception e )
		{
			Console.Log(e.ToString());
		}
		return false;
	}

	/// <summary>
	/// Connects the input.
	/// </summary>
	/// <returns><c>true</c>, if input was connected, <c>false</c> otherwise.</returns>
	/// <param name="input">Input.</param>
	public bool ConnectInput( WireValueInput input )
	{
		try
		{
			// Check the input allows us to use this type.
			if( this.mValue.GetType() == input.mDataType )
			{
				// Make sure the input is not alreayd connected.
				if( FindInput( input ) )
				{ 
					Console.Log("This is already connected!");
					return false; 
				}


				// Add the input to the output.
				mConnectedInputs.Add( input );
				input.mConnectedOutput = this;
				return true;
			}
			else
			{
				Console.Log("ConnectInput data mismatch.");
			}
		}
		catch( System.Exception e )
		{
			Console.Log( e.ToString () );
		}

		// Failed to match type and or an unexpected error occured.
		return false;

	}

	/// <summary>
	/// Disconnects the input.
	/// </summary>
	/// <returns><c>true</c>, if input was disconnected, <c>false</c> otherwise.</returns>
	/// <param name="input">Input.</param>
	public bool DisconnectInput( WireValueInput input )
	{
		try
		{
			input.mConnectedOutput = null;
			bool removal = mConnectedInputs.Remove( input );
			return removal;
		}
		catch( System.Exception e )
		{
			Console.Log( e.ToString () );
		}

		// Failed to find input.
		return false;
	}

	/// <summary>
	/// Disconnects the input by name.
	/// </summary>
	/// <returns><c>true</c>, if input was disconnected, <c>false</c> otherwise.</returns>
	/// <param name="inputName">Input name.</param>
	public bool DisconnectInput( string inputName )
	{
		foreach( WireValueInput I in mConnectedInputs )
		{
			if( I.mName == inputName )
			{
				// Call disconnect.
				return DisconnectInput( I );
			}
		}

		// Failed to find input.
		return false;
	}

	/// <summary>
	/// Update the Output Value.
	/// </summary>
	/// <returns><c>true</c>, if value was updated, <c>false</c> otherwise.</returns>
	/// <param name="value">Value.</param>
	public bool UpdateValue( object value )
	{
		// Check that the new value matches the data type of the initial value.
		// This prevents the inputs mismatching types.
		if( base.mValue.GetType() != value.GetType() )
		{ 
			Console.Log ("UpdateValue data mismatch." ); 
			return false; 
		}

		// Update the value.
		base.mValue = value;

		// Inform the inputs the value has changed.
		foreach( WireValueInput I in mConnectedInputs )
		{
			// Should any exception happen within the game code this will prevent it breaking the other inputs.
			try
			{
				// Make sure the delegate has been assigned.
				if( I.OnChanged != null )
				{
					// Inform the input the output value has changed.
					I.OnChanged( this );
				}
			}
			catch( System.Exception e )
			{
				// Log to the game console.
				Console.Log( e.ToString() );
			}
		}

		// Success!
		return true;
	}

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	public object GetValue()
	{
		return base.mValue;
	}


	/// <summary>
	/// The connected inputs.
	/// </summary>
	public List<WireValueInput> mConnectedInputs = new List<WireValueInput>();	
}