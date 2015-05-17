//Corpse Party gamemode.
function servercmdCP(%client)
{
	if(!%client.isAdmin)
		return;
	$gamemode = "corpseParty";
}
package corpseParty
{
	function gameConnection::onDeath(%client)
	{
		if($gamemode $= "corpseParty")
		{
			%truePlayer = %client.player;
			%client.createPlayer(%truePlayer.getPosition());
			%client.player.hideNode("ALL");
			%client.player.setShapeNameDistance(0);
			%client.player = %truePlayer;
		}
		parent::onDeath(%client);
	}
	function player::PlayDeathAnimation(%player)
	{
		if($gamemode $= "corpseParty")
		{
			return;
		}
		parent::PlayDeathAnimation(%player);
	}
	function Player::RemoveBody(%player)
	{
		if($gamemode $= "corpseParty")
		{
			return;
		}
		parent::removeBody(%player);
	}
};
activatePackage(corpseParty);