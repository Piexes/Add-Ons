//Haunted gamemode.
package Haunted
{
	function Player::removeBody(%player)
	{
		if($gamemode $= "Haunted")
			return;
		parent::removeBody(%player);
	}
	function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc)
	{
		if($gamemode $= "Haunted")
		{
			if(%client.isGhost == 1)
			{
				%pos = %client.player.getPosition();
				%client.chatMessage("\c3You can't suicide! You're already dead...");
				%client.instantRespawn();
				%client.player.setTransform(%pos);
				%client.player.setNodeColor("ALL","1 1 1 0.4");
				return;
			}
			else
			{
				%client.isGhost = 1;
				%corpse = %client.player;
				%corpse.setShapeNameDistance(0);
				%client.createPlayer(%corpse.getPosition());
				%client.player.setNodeColor("ALL","1 1 1 0.4");
				%client.player.mountImage(GhostTrailImage,$backslot);
				%client.player.setMaxForwardSpeed(13);
				return;
			}
		}
		else
			parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
	}
	function armor::onTrigger(%armor, %player, %slot, %value)
	{
		%client = %player.client;
		if(%slot == 0 && %client.isGhost == 1 && %client.cooldown < $Sim::Time)
		{
			%client.cooldown = $Sim::Time + 1;
			initContainerRadiusSearch(%player.getPosition(),100,$Typemasks::CorpseObjectType);
			%corpse = containerSearchNext();
			while(%corpse = containerSearchNext())
			{
				%hasFoundCorpse++;
				%client.chatMessage("\c3A corpse has been detected! You are being reborn...");
				%client.isGhost = 0;
				%client.player.schedule(33, delete);
				%client.createPlayer(%corpse.getTransform());
				%corpse.delete();
				return;
			}
			if(%hasFoundCorpse == 0)
			{
				%client.chatMessage("\c3You have tried to ressurect yourself, but there are no corpses in the area, or you are trying to use your own corpse.");
			}
		}
		parent::onTrigger(%armor, %player, %slot, %value);
	}
	function servercmdLight(%client)
	{
		if(%client.isGhost)
		{
			%client.player.addVelocity("0 0 10");
			return;
		}
		parent::servercmdLight(%client);
	}
	function servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face)
	{
		if(%client.isGhost == 1)
		{
			%client.chatMessage("\c3You're a ghost! You can't change your appearance.");
			return;
		}
		parent::servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face);
	}
	function servercmdUpdateBodyParts(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face)
	{
		if(%client.isGhost == 1)
			return;
		parent::servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face);
	}
};
activatePackage(Haunted);