//Section 1: Functions
//Section 2: Spawns
//[SECTION 1]
function setHud(%client)
{
	%color = %client.color;
	%client.bottomPrint(%color @ "Money:\c6" SPC %client.money SPC %color @ "Income:\c6" SPC %client.income);
}
activatePackage(SpawnHud);
function servercmdsetHudColor(%client, %color)
{
	if(%color $= "Orange")
	{
		%client.chatMessage("<color:FFA500>HUD has been changed.");
		%client.color = "<color:FFA500>";
	}
	else if(%color $= "Red")
	{
		%client.chatMessage("HUD has been changed.");
		%client.color = "\c0";
	}
	else if(%color $= "Blue")
	{
		%client.chatMessage("\c1HUD has been changed.");
		%client.color = "\c1";
	}
	else if(%color $= "Yellow")
	{
		%client.chatMessage("\c3HUD has been changed.");
		%client.color = "\c3";
	}
	else
		%client.chatMessage("\c6That's not a valid color! The current options are <color:FFA500>orange, \c0red, \c1blue, \c6and \c3yellow.");
}
//[SECTION 2]
package SpawnHUD
{
	function gameConnection::SpawnPlayer(%client)
	{
		parent::SpawnPlayer(%client);
		setHud(%client);
	}
};