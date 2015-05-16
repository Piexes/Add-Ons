//Corpse Party gamemode.
function servercmdCP(%client)
{
	if(!%client.isAdmin)
		return;
	$gamemode = "corpseParty";
}
package corpseParty //PlayDeathAnimation
{
	function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc)
	{
		if($gamemode $= "corpseParty")
		{
			//Defining variables
			%pos = %client.player.getPosition();
			%client.oldPlayer = %client.player;

			//Creating the statue
			%client.createPlayer(%pos); //Note: this overrides %client.player with a new value.

			//Modifying the player
			%client.player.setNodeColor("ALL", "112 112 112 1");
			%client.player.setShapeNameDistance(0);

			//Restoring the player
			%client.player = %client.oldPlayer;

			//Nullifying variables
			%client.oldPlayer = "";

			//Deleting the player
			%client.player.delete();
		}
		parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
	}
	function Player::PlayDeathAnimation(%pl)
	{
		if($gamemode $= "corpseParty")
		{
			return;
		}
		parent::PlayDeathAnimation(%pl);
	}
};
activatePackage(corpseParty);