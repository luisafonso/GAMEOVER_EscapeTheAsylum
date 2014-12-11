#pragma strict

//This script allows you to insert code when the Item is used (clicked on in the inventory).

var deleteOnUse = true;
var battery = false;
var pic = false;

private var playersInv : Inventory;
private var item : Item;
private static var batteryPower : float = 10;
var batterySound : AudioClip;
var showPicSound : AudioClip;
var picture : Texture;
private var used = false;

@script AddComponentMenu ("Inventory/Items/Item Effect")
@script RequireComponent(Item)

//This is where we find the components we need
function Awake ()
{
	playersInv = FindObjectOfType(Inventory); //finding the players inv.
	if (playersInv == null)
	{
		Debug.LogWarning("No 'Inventory' found in game. The Item " + transform.name + " has been disabled for pickup (canGet = false).");
	}
	item = GetComponent(Item);
}

//This is called when the object should be used.
function UseEffect () 
{
	Debug.LogWarning("<INSERT CUSTOM ACTION HERE>"); //INSERT CUSTOM CODE HERE!
	if(pic)
	{
	AudioSource.PlayClipAtPoint(showPicSound, transform.position);
	used = !used;
	} 
	if(battery){
	AudioSource.PlayClipAtPoint(batterySound, transform.position);
	FlashLight.AlterEnergy(batteryPower);
	}
	//Play a sound
	playersInv.gameObject.SendMessage("PlayDropItemSound", SendMessageOptions.DontRequireReceiver);
	
	//This will delete the item on use or remove 1 from the stack (if stackable).
	if (deleteOnUse == true)
	{
		DeleteUsedItem();
	}
}

function OnGUI()
	{
		if(used && pic){
			GUI.DrawTexture(Rect(50,100,picture.width,picture.height), picture);
			
		}
	}

//This takes care of deletion
function DeleteUsedItem()
{
	if (item.stack == 1) //Remove item
	{
		playersInv.RemoveItem(this.gameObject.transform);
	}
	else //Remove from stack
	{
		item.stack -= 1;
	}
	Debug.Log(item.name + " has been deleted on use");
}