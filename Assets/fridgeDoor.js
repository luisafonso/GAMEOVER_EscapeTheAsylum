#pragma strict
var doorClip : AnimationClip;
private var Door = false;
private var opened = false;
var doorSound : AudioClip;
//var Key : GameObject;
function Start () {

}

function Update () {
	if (Input.GetKeyDown(KeyCode.O) && Door == true && opened == false) //&& Key.active == false to use with key
	{
	GameObject.Find("Refridgerator_Door").animation.Play("doorOpenFridge");
	audio.PlayOneShot(doorSound);
	opened = true;
	}
}

function OnGUI()
	{
		if(Door && opened == false){
			GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 100, 150, 30), "Press 'O' to open the door");
		}
	}
function wait()
{
yield WaitForSeconds(3);
}	
function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		Door = true;
	}
}

function OnTriggerExit (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		Door = false;
	}
}

