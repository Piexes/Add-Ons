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

function ChatMsgAll(%power,%type,%msg)
{
	%power = mFloor(%power);
	if(!%power)
		messageAll(%type,%msg);
	else if(%power == 1)
	{
		for(%i=0;%i<clientGroup.getCount();%i++)
		{
			%cl = clientGroup.getObject(%i);
			if(%cl.isAdmin)
				messageClient(%cl,%type,"\c7[\c6Admins\c7]\c0 " @ %msg);
		}
	}
	else if(%power == 2)
	{
		for(%i=0;%i<clientGroup.getCount();%i++)
		{
			%cl = clientGroup.getObject(%i);
			if(%cl.isSuperAdmin)
				messageClient(%cl,%type,"\c7[\c6Super Admins\c7]\c0 " @ %msg);
		}
	}
	else
		messageAll(%type,%msg);
}

function serverCmdSC(%cl,%msg0,%msg1,%msg2,%msg3,%msg4,%msg5,%msg6,%msg7,%msg8,%msg9,%msg10,%msg11,%msg12,%msg13,%msg14,%msg15,%msg16,%msg17)
{
	if(!%cl.isSuperAdmin) return;
	for(%i=0;%i<17;%i++)
		%msg = %msg SPC %msg[%i];
	%msg = trim(stripMLControlChars(%msg));
	for(%i=0;%i<clientGroup.getCount();%i++)
	{
		%c = clientGroup.getObject(%i);
		if(%c.isSuperAdmin)
			messageClient(%c,%type,"\c7SuperAdminChat(\c0" @ %cl.getPlayerName() @ "\c7)\c6: \c6" @ %msg);
	}
}

function serverCmdAC(%cl,%msg0,%msg1,%msg2,%msg3,%msg4,%msg5,%msg6,%msg7,%msg8,%msg9,%msg10,%msg11,%msg12,%msg13,%msg14,%msg15,%msg16,%msg17)
{
	if(!%cl.isAdmin) return;
	for(%i=0;%i<17;%i++)
		%msg = %msg SPC %msg[%i];
	%msg = trim(stripMLControlChars(%msg));
	for(%i=0;%i<clientGroup.getCount();%i++)
	{
		%c = clientGroup.getObject(%i);
		if(%c.isAdmin)
			messageClient(%c,%type,"\c2AdminChat(\c4" @ %cl.getPlayerName() @ "\c2)\c6: \c3" @ %msg);
	}
}

function serverCmdMC(%cl,%msg0,%msg1,%msg2,%msg3,%msg4,%msg5,%msg6,%msg7,%msg8,%msg9,%msg10,%msg11,%msg12,%msg13,%msg14,%msg15,%msg16,%msg17)
{
	if(!%cl.isModerator && !%cl.isAdmin) return;
	for(%i=0;%i<17;%i++)
		%msg = %msg SPC %msg[%i];
	%msg = trim(stripMLControlChars(%msg));
	for(%i=0;%i<clientGroup.getCount();%i++)
	{
		%c = clientGroup.getObject(%i);
		if(%c.isAdmin || %c.isModerator)
			messageClient(%c,%type,"\c4ModeratorChat(\c1" @ %cl.getPlayerName() @ "\c4)\c1: \c6" @ %msg);
	}
}
