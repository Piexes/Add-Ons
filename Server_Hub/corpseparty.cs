//Corpse Party gamemode.
function servercmdCP(%client)
{
	if(!%client.isAdmin)
		return;
	$gamemode = "corpseParty";
}
//Clears corpses
function servercmdclearCorpses(%client)
{
	if(%client.isAdmin)
	{
		//This clears corpses
		initContainerRadiusSearch(%client.player.getPosition(),100,$Typemasks::CorpseObjectType);
		%corpse = containerSearchNext();
		while(%object = containerSearchNext())
		{
			%count++;
			%object.delete();
		}
		if(%count > 0)
			announce(%count SPC "statues have been cleared!");

		//This clears empty bodies
		initContainerRadiusSearch(%client.player.getPosition(),100,$Typemasks::PlayerObjectType);
		%body = containerSearchNext();
		while(%body = containerSearchNext())
		{
			if(%body.isStatue)
			{
				%tally++;
				%body.delete();
			}
		}
		if(%tally > 0)
			echo("Invisible bodies cleared:" SPC %tally);
	}
}
package corpseParty
{
	function gameConnection::onDeath(%client)
	{
		if($gamemode $= "corpseParty")
		{
			%truePlayer = %client.player;
			%client.createPlayer(%truePlayer.getPosition());
			%client.player.isStatue = 1;
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