function servercmdLight(%client)
{
	if(%client.class $= "Forcefeild")
	{
		%pos = %client.player.getPosition();
		InitContainerRadiusSearch(%pos,10,$TypeMasks::PlayerObjectType, %client.player);
		%player = containerSearchNext();
		while(%player = containerSearchNext())
		{
			%player.setRelativeVelocity("0 0 50");
		}
	}
	else if(%client.class $= "Sprint")
	{
		if(%client.cooldown + 5 < $Sim::Time)
		{
			%client.cooldown = $Sim::Time;
			%client.player.addRelativeVelocity("0 5000 0");
		}
	}
	else if(%client.class $= "Slow-down")
	{
		if(%client.cooldown + 20 < $Sim::Time)
		{
			%client.cooldown = $Sim::Time;
			%pos = %client.player.getPosition();
			InitContainerRadiusSearch(%pos, 20, $TypeMasks::PlayerObjectType, %client.player);
			%player = containerSearchNext();
			while(%player = containerSearchNext())
			{
				%client.player.setMaxForwardSpeed("2");
				%client.player.schedule(3000, setMaxForwardSpeed, 7);
			}
		}
	}
	else if(%client.class $= "Radar")
	{
		messageClient(%client,'',"\c5Nearby Players:");
		%pos = %client.player.getPosition();
		InitContainerRadiusSearch(%pos, 40, $TypeMasks::PlayerObjectType, %client.player);
		%player = containerSearchNext();
		while(%player = containerSearchNext())
		{
			%player.client.health = %player.dataBlock.maxDamage - %player.getDamageLevel();
			messageClient(%client,'',"\c5" @ %player.client.name @ "\c6: Class:" SPC %player.client.class @ ", Murders:" SPC %player.client.money SPC ", Health:" SPC %player.client.health @ ".");
		}
	}
}
//R forcefeild, jet sheild?