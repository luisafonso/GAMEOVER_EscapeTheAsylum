﻿#pragma strict

private var lightsource : GameObject;

function Start () {
	
	
	lightsource = GameObject.Find("Light");
	
	lightsource.GetComponent(FlashLight).enabled = true;
	
}

function Update () {

}