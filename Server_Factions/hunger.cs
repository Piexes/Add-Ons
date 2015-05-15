//Made by Piexes. Do not distribute.
function StarveTick()
{
	cancel($theLoop);
	$theLoop = schedule(1000, 0,StarveTick);
	//Loop
	for(%cl = 0; %cl < %count; %cl++)
	{
		%client = clientGroup.getObject(%cl);
		if(isObject(%client.Player))
		{
			talk("ARR");
			%client.hunger -= 1;
			if(%client.hunger = 0)
			{
				%client.player.kill();
				announce("\c6" @ %client.name SPC "\c7has died of starvation!");
			}
			else
				%client.bottomPrint("\c0Murders\c6:" SPC %client.souls SPC "\c0Hunger\c6:" SPC %client.hunger @ "/20" SPC "\c0Money\c6: $" @ %client.money, 0, 1);
		}
	}
}