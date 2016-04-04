using UnityEngine;
using System.Collections;

/// <summary>
/// UI Element visiblity
/// Press the specified key / input button to toggle the object visibility
/// </summary>
public class KeyVisibilityUI : MonoBehaviour {
    /// <summary>
    /// Show the UI Element based on this 'input' from unity.
    /// </summary>
    public string mVisibleInput = "ShowConsole";

    /// <summary>
    /// The UI Element which we toggle.
    /// </summary>
    public GameObject mUIElement;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown(mVisibleInput))
        {
            // Invert the visibility state
            mUIElement.SetActive(!mUIElement.activeSelf);
        }
	}
}
