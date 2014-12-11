#pragma strict
var picture : Texture;
private var isClose;
var scareSound : AudioClip;
function Start () {

}

function Update () {

}

function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		isClose = true;
		audio.PlayOneShot(scareSound);
	}
}

function OnTriggerExit (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		isClose = false;
	}
}
function OnGUI()
	{
		if(isClose){
			//GUI.DrawTexture(Rect(500,100,picture.width,picture.height), picture);
		}
	}