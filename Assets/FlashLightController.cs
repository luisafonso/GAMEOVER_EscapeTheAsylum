using UnityEngine;
using System.Collections;

public class FlashLightController : MonoBehaviour 
{

	public Light flashlightObject;
	public float powerLevel;
	// Use this for initialization
	void Start () 
	{
		flashlightObject.enabled = false;
		flashlightObject.intensity = 3.0f;
		powerLevel = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			if(flashlightObject.enabled == false)
			{
				flashlightObject.enabled = true;
			}
			else
			{
				flashlightObject.enabled = false;
			}
		}
		if (flashlightObject.enabled == true) 
		{

			if(powerLevel > 0) 
			{
	
				powerLevel -= 0.001f;
				flashlightObject.intensity -= 0.001f;
			}
			else 
			{
				powerLevel = 0;
				flashlightObject.intensity = 0;
			}
		}
	}
}
