package ChatShit
{
	function servercmdMessageSent(%client, %msg)
	{
		%oldPrefix = %client.clanPrefix;
		if(%client.isAdmin)
		{
			%client.clanPrefix = "\c5[\c6Admin\c5] ";
			if(%client.isHost)
			{
				%client.clanPrefix = "\c7[\c0Host\c7] ";
			}
			else if(%client.isSuperAdmin)
			{
				%client.clanPrefix = "\c2[\c6Super Admin\c2] ";
			}
		}
		//New If statement
		else if(%client.customTag = "" || %client.isAdmin) { return;}
		else
		{
			%client.clanPrefix = %client.tag;
		}
		Parent::serverCmdMessageSent(%client,%msg);
	}
};
activatepackage(ChatShit);

function servercmdsetTag(%client, %tag, %color)
{
	if(%color $= "blue")
		%client.tag = "\c1" @ %tag;
	else if(%color $= "pink")
		%client.tag = "\c5" @ %tag;
	else if(%color $= "yellow")
		%client.tag = "\c3" @ %tag;
	else if(%color $= "red")
		%client.tag = "\c0" @ %tag;
	else if(%color $= "white")
		%client.tag = "\c6" @ %tag;
	else
		messageClient(%client, '',"\c2That's not a valid color! The current ones are pink, white, red, yellow, and blue.");
}