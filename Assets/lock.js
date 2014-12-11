#pragma strict

var currentCombination : int = 0;
 var correctCombination : int;
 var buttonClickProgress : int;
 private var closeToSafe = false;


 
 var buttonSound : AudioClip;
 var acceptedSound : AudioClip;
 var deniedSound : AudioClip;
 
 function OnGUI() {

 
 //GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 100, 150, 30), "Press 'O' to pick lock");

 
 if(GUI.Button(Rect (Screen.width/2+0,Screen.height/2+0, 50, 50), "0")){ ButtonWasClicked(0); audio.PlayOneShot(buttonSound); }
 if(GUI.Button(Rect (Screen.width/2+50,Screen.height/2+0, 50, 50), "1")){ ButtonWasClicked(1); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+100,Screen.height/2+0, 50, 50), "2")){ ButtonWasClicked(2); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+0,Screen.height/2+50, 50, 50), "3")){ ButtonWasClicked(3); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+50,Screen.height/2+50, 50, 50), "4")){ ButtonWasClicked(4); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+100,Screen.height/2+50, 50, 50), "5")){ ButtonWasClicked(5); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+0,Screen.height/2+100, 50, 50), "6")){ ButtonWasClicked(6); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+50,Screen.height/2+100, 50, 50), "7")){ ButtonWasClicked(7); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+100,Screen.height/2+100, 50, 50), "8")){ ButtonWasClicked(8); audio.PlayOneShot(buttonSound);}
 if(GUI.Button(Rect (Screen.width/2+0,Screen.height/2+150, 50, 50), "9")){ ButtonWasClicked(9); audio.PlayOneShot(buttonSound);}
 var temp = currentCombination.ToString();
 var temp1 = temp.Substring(0, temp.Length-1);
 GUI.Label(new Rect(Screen.width/2, Screen.height/2-200, 100, 100), temp1);
 
 
 
 }
 
 function ButtonWasClicked (buttonNmb : int) {
 
 currentCombination += buttonNmb;
 buttonClickProgress++;
 
 if(buttonClickProgress < 4){
     currentCombination *= 10;
 }
 else{
     if(currentCombination == correctCombination){
         Debug.Log("You Opened the Combination lock!");
         audio.PlayOneShot(acceptedSound);
         GameObject.Find("Safe").animation.Play("openSafe");
         GameObject.Find("Key").GetComponent(FirstPersonPickUp).enabled = true;
         GameObject.Find("Key").GetComponent(KEY).enabled = true;
		 
		 
         buttonClickProgress = 0;
         currentCombination = 0;
         GameObject.Find("Safe").GetComponent(lock).enabled = false;
         GameObject.Find("First Person Controller").GetComponent(MouseLook).enabled = true;
		 GameObject.Find("Main Camera").GetComponent(MouseLook).enabled = true;     
     }
     else{
         Debug.Log("Wrong Combination Code, reseting...");
         audio.PlayOneShot(deniedSound);
         buttonClickProgress = 0;
         currentCombination = 0;
     }
 }
 }
 
