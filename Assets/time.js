#pragma strict
private var called = false;
function Start () {
if(!called)
Time.timeScale = 1;
called = true;
}

function Update () {

}