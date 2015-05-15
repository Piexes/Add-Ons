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

function serverCmdJoinLeaveMsg(%this, %name, %message)
{
	commandToAll('ServerMessage', 'MsgClientJoin' ,'\c1%1 connected.', %name);
	schedule(1000,0,chatMessageAll, %this, '\c7%1\c3%2\c7%3\c6: %4', "", %name, "", %message);
	schedule(3000,0,commandToAll,'ServerMessage', 'ClientDrop' ,'\c1%1 has left the game.', %name);
}
