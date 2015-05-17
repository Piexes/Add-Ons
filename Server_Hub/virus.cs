//This chooses who gets the plague at the start of the round.
function decidePG()
{
	%count = clientGroup.getCount();
	for(%i=0;%i<%count;%i++)
	{
		%client = clientGroup.getObject(getRandom(0,%count - 1));
	}
	if(isObject(%client.player) && getMinigameFromObject(%client.player) !$= "")
	{
		givePlague(%client, 1);
	}
	else if(!isObject(%client.player))
	{
		announce("\c7The claws of the plague have reached out to a player, but they have been deemed unworthy...");
		%client.chatMessage("\c7You were going to be given the \c0ultimate blessing\c7, but you are already taken.");
		schedule(2000, 0, decidePg);
	}
}

//The transformation process from being regular to getting the plague.
function pTransform(%client, %giver)
{
	if(%giver == 0)
		schedule(7000, announce, "\c7The plague has taken another victim.");
	%client.hasPlague = 1;
	%client.player.schedule(7000, setMaxForwardSpeed, "10");
	//Black Coloring
	%client.player.setNodeColor("lhand","0 0 0 1");
	%client.player.schedule(2000,setNodeColor,"larm","0 0 0 1");
	%client.player.schedule(3000,setNodeColor,"rhand","0 0 0 1");
	%client.player.schedule(3700,setNodeColor,"rarm","0 0 0 1");
	%client.player.schedule(5300,setNodeColor,"chest","0 0 0 1");
	%client.player.schedule(6200,setNodeColor,"headskin","0 0 0 1");
	%client.player.schedule(7000,setNodeColor,"ALL","0 0 0 1");
}

//Gives a person the plague.
function givePlague(%client, %giver)
{
	if(isObject(%client.player))
	{
		if(%giver == 1)
		{
			%client.chatMessage("\c7You are the \c0plague-giver\c7. Punish those who are deemed \c8unholy.");
			pTransform(%client, 1);
		}
		else
		{
			if(%client.hasPlague == 1)
				return;
			announce("\c3" @ %client.name @ ":\c6 Achoo!");
			schedule(5000, 0, pTransform, %client, 0);
		}
	}
	else
	{
		%client.chatMessage("\c7You were going to be given the \c0ultimate blessing\c7, but you have already been taken.");
	}
}

package VirusGame
{
	//People lose the plague when they die.
	function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc)
	{
		parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
		if(%client.hasPlague == 1)
			%client.hasPlague = 0;
	}
	//Plague Infections.
	function Armor::OnTrigger(%armor, %player, %slot, %value)
	{
		%client = %player.client;
		if(%client.cooldown > $Sim::Time)
		{
			return;
		}
		%client.cooldown = $Sim::Time + 1;
		if(%slot == 0 && %client.hasPlague == 1)
		{
			%start = %client.player.getEyePoint();
			%scaledeyeVector = vectorScale(%client.player.getEyeVector(), 3);
			%end = vectorAdd(%start, %scaledEyeVector);
			%result = containerRaycast(%start, %end, $TypeMasks::PlayerObjectType, %client.player);
			%targPlayer = getWord(%result, 0);
			if(isObject(%targPlayer))
			{
				%client.bottomPrint("\c7You have gifted the plague to" SPC %targPlayer.client.name, 2);
				givePlague(%targPlayer.client, 0);
			}
			else
			{
				%client.chatMessage("\c7You have not hit anything that the jaws of the plague could affect.");
			}
		}
		parent::onTrigger(%armor, %player, %slot, %value);
	}
	//This makes it so players can't change their avatar when they have the plague.
	function servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face)
	{
		if(!%client.hasPlague)
		{
			parent::servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face);
		}
	}
	function servercmdUpdateBodyParts(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face)
	{
		if(!%client.hasPlague)
		{
			parent::servercmdUpdateBodyParts(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face);
		}
	}
	//Decides a plague giver at the start of a round.
	function MinigameSO::Reset(%minigame, %client)
	{
		parent::Reset(%minigame, %client);
		if($gamemode $= "Plague")
			schedule(500, 0, decidePG);
	}
};
activatePackage(VirusGame);