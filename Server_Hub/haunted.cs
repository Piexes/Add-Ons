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
			}
			else
			{
				%client.isGhost = 1;
				%corpse = %client.player;
				%corpse.setShapeNameDistance(0);
				%client.createPlayer(%corpse.getPosition());
				%client.player.setNodeColor("ALL","1 1 1 0.4");
				return;
			}
		}
		else
			parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
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
		if(%client.isGhost == 0)
			parent::servercmdUpdateBodyColors(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %body, %face);
	}
};
activatePackage(Haunted);