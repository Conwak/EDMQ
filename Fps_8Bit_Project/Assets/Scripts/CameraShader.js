#pragma strict

var texCor : Shader = null;

function Start () {

	Camera.main.SetReplacementShader(texCor, "RenderType");

}

function Update () {

}