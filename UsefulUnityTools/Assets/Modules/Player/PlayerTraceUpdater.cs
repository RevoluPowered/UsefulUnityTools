using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// This is a class that is used to handle the player traces, this is automatically retrieving one update of the trace per tick,
/// thus any weapons or anything that requires this should pull the information from this.
/// This will be one of the most passed around object locally on the client; really only one should exist. 
/// Networking should be relatively simple for this all that will need syncronized is the player's local commands to the player object which will be implemented by the server and client api which is still to 
/// be re-developed away from the old systm of the NetworkView.RPC system.
/// </summary>
public class PlayerTraceUpdater : NetworkBehaviour {
    // Enable trace debugging
    public bool mDebuggingEnabled = false;

    /// <summary>
    /// Did the trace hit?
    /// </summary>
    public bool mTraceHit;

    /// <summary>
    /// The raycast information
    /// </summary>
    public RaycastHit mRaycastInformation;

    /// <summary>
    /// The raycast layer
    /// </summary>
    public string mRaycastLayer;

    /// <summary>
    /// The max distance of the ray.
    /// </summary>
    public float mMaxDistance = 100.00f;

    /// <summary>
    /// The camera the trace system uses to handle the trace.
    /// </summary>
    public Camera mMainCamera;

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer) return;
        // Update this camera to be the camera used for the rotation.
        mMainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;

        if(mMainCamera == null )
        {
            mMainCamera = Camera.main;
        }

        // Generate a new ray, relative to the rotation of the object's forward direction. This must also ignore triggers because this isn't allowed to report them.
        mTraceHit = Physics.Raycast(mMainCamera.transform.position, mMainCamera.transform.rotation * Vector3.forward, out mRaycastInformation, mMaxDistance);

        // if the trace hit we need to be doing some stuff here...
        // This is editor only anyway!
        if(mTraceHit && mDebuggingEnabled)
        {

            Color setColour = Color.red;
            if(Input.GetMouseButton(0))
            {
                setColour = Color.green;
            }


            // Draw our debugging ray.
            Debug.DrawRay(mMainCamera.transform.position, (mMainCamera.transform.rotation * Vector3.forward) * mRaycastInformation.distance, setColour);
        }
	}
}
