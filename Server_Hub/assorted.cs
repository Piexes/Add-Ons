//Random scripts that I've made for the hub.
function servercmdRestart(%client)
{
	if(!%client.isSuperAdmin)
		return;
	%mini = %client.minigame;
	%mini.reset();
}

function servercmdGhost(%client)
{
	%client.player.setNodeColor("ALL", "0 0 0 0.1");
}

function SpectateMode(%client)
{
	announce("ho");
	%camera = %client.camera;
	%camera.setFlyMode();
	%camera.mode = "Observer";
	%client.setControlObject(%camera);
}
package CameraShit
{
	function servercmdS(%client)
	{
		spec(%client);
	}
	function servercmdLight(%client)
	{
		%client.oldPlayer = %client.player;
		%client.player = %client.createPlayer(%client.oldPlayer.getTransform());
		%client.miniMe = %client.player;
		%client.player = %client.oldPlayer;
		%client.setControlObject(%client.minime);
		%client.oldPlayer = "";
		%client.miniMe.setPlayerShapeNameDistance(0);
	}
};
function servercmdBlandaUp(%client)
{
	activatePackage(CameraShit);
	deactivatePackage(VirusGame);
}
function servercmdCam(%client)
{
	%cam = %client.camera;
	%cam.setMode(corpse, %client.player);
	%client.setControlObject(%cam);
	%client.schedule(2000, setControlObject, %client.player);
}
function servercmdGhost(%client) //unmount is broken
{
	if(%client.isGhost == 1)
	{
		%cam = %client.camera;
		%client.isGhost = 0;
		%client.setControlObject(%client.player);
		%cam.unmountObject(%client.player);
	}
	else
	{
		%client.isGhost = 1;
		%cam = %client.camera;
		%cam.setFlyMode();
		%cam.mode = "Observer";
		%cam.mountObject(%client.player,0);
		%client.setControlObject(%cam);
	}
}
function servercmdPos(%client)
{
	talk(%client.player.getPosition());
}
function servercmdrandomNum(%client, %a, %b)
{
	%Choice = getRandom(%a, %b);
	announce(%choice);
}