using System;
using UnityEngine;
using WireIO;

public class WireValueInput
{
	public WireValueInput( string name, Type dataType )
	{
		mName = name;
		mDataType = dataType;
	}

	// Standard Input Events
	public delegate void mGenericEvent( WireValueOutput output );
	
	/// <summary>
	/// The input name.
	/// </summary>
	public string mName;

	/// <summary>
	/// The allowed type of the value input.
	/// </summary>
	public Type mDataType;

	/// <summary>
	/// Called when the output value is changed.
	/// </summary>
	public mGenericEvent OnChanged = null;


	/// <summary>
	/// The currently connected output if any.
	/// This should be reset to null when you disconnect the input.
	/// </summary>
	public WireValueOutput mConnectedOutput = null;
}
