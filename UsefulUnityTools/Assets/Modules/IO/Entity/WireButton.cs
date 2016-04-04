using UnityEngine;
using System.Collections;

public class WireButton : MonoBehaviour {

	public bool mState;

	WireController mController;
	WireValueOutput mButtonValue;

	// Use this for initialization
	void Start () {
		// Attach our wire controller.
		mController = gameObject.AddComponent<WireController>();

		// Create the button state output.
		mButtonValue = new WireValueOutput( "State", mState );

		// Add the button to the available outputs.
		mController.mOutputs.Add(mButtonValue);

		// Trigger update with default value.
		mButtonValue.UpdateValue( mState );
	}

	/// <summary>
	/// Toggle the button press.
	/// </summary>
	public void Toggle()
	{
		mState = !mState;
		Console.Log("Button Toggle: " + mState );
		mButtonValue.UpdateValue(mState);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
