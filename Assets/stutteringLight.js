#pragma strict

var FlashingLight : Light;
FlashingLight.enabled = false;

function Start () {

}

function Update () {

var RandomNumber = Random.value;

	if(RandomNumber <= .7){
	FlashingLight.enabled = true;
	}
	else FlashingLight.enabled = false;
}