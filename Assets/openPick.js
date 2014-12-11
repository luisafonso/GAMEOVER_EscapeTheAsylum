#pragma strict
private var safe : GameObject;
private var firstPersonCamera : GameObject;
private var mainCamera : GameObject;
private var closeToSafe = false;
private var picking = false;


function Start () {
	safe = GameObject.Find("Safe");
	firstPersonCamera = GameObject.Find("First Person Controller");
	mainCamera = GameObject.Find("Main Camera");
	
}

function Update () {
	if(closeToSafe && Input.GetKeyDown(KeyCode.O))
	{
	picking = !picking;
	
	if(picking){
	firstPersonCamera.GetComponent(MouseLook).enabled = false;
	mainCamera.GetComponent(MouseLook).enabled = false;
	safe.GetComponent(lock).enabled = true;
	
	}
	else
	{
	firstPersonCamera.GetComponent(MouseLook).enabled = true;
	mainCamera.GetComponent(MouseLook).enabled = true;
	safe.GetComponent(lock).enabled = false;
	} 
	}
	

}
function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		closeToSafe = true;
	}
}

function OnTriggerExit (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		closeToSafe = false;
	}
}
