//+=========================================================================================================+\\
//|         Made by..                                                                                       |\\
//|        ____   ____  _                __          _                                                      |\\
//|       |_  _| |_  _|(_)              [  |        / |_                                                    |\\
//|         \ \   / /  __   .--.   .--.  | |  ,--. `| |-' .--.   _ .--.                                     |\\
//|          \ \ / /  [  | ( (`\]/ .'`\ \| | `'_\ : | | / .'`\ \[ `/'`\]                                    |\\
//|           \ ' /    | |  `'.'.| \__. || | // | |,| |,| \__. | | |                                        |\\
//|            \_/    [___][\__) )'.__.'[___]\'-;__/\__/ '.__.' [___]                                       |\\
//|                             BL_ID: 20490                                                                |\\
//|             Forum Profile: http://forum.blockland.us/index.php?action=profile;u=40877;                  |\\
//|                                                                                                         |\\
//+=========================================================================================================+\\

if(isPackage(AutoGreetAnnoyance))
	deactivatePackage(AutoGreetAnnoyance);
	
package AutoGreetAnnoyance 
{
	function GameConnection::autoAdminCheck(%this)
	{
		for(%i=0;%i<clientGroup.getCount();%i++)
		{
			%cl = clientGroup.getObject(%i);
			if($Server::ForcedToAutoGreet[%cl.getBLID()] && %cl != %this)
				%this.schedule(100,AutoGreet,%this.getPlayerName());
		}
		return Parent::autoAdminCheck(%this);
	}
};
activatePackage(AutoGreetAnnoyance);

function GameConnection::AutoGreet(%this,%person)
{
	if(%person $= "") %person = "nobody";
	schedule(1,0,serverCmdStartTalking,%this);

	%r = getRandom(1,3);
	if(%r == 1)
		%msg = "Welcome to the server, " @ %person @ "!";
	if(%r == 2)
		%msg = "Hello, " @ %person @ "!";
	if(%r == 3)
		%msg = "Welcome, " @ %person @ "! Enjoy!";

	schedule(22 * strLen(%msg),0,serverCmdMessageSent,%this,%msg);
}
