//Section 1: Arresting
//Section 2: Jailing
//Section 3: Stars
//[SECTION 1]
function arrest(%client, %cop)
{
	if(%client.stars >= 1)
	{
		%client.player.instantRespawn();
		announce("\c3" @ %client.name SPC "has been arrested!"); 
		%client.jailed = 1;
		%client.stars = 0;
	}
	else
	{
		%cop.chatMessage("\c0You have attempted wrongful arrest!");
		%cop.stars += 2;
	}
}
//[SECTION 2]
function servercmdJailCoordinates(%client)
{
	if(%client.isSuperAdmin)
	{
		$jailCoordinates = %client.player.getPosition();
		announce("\c3" @ %client.name SPC "has set a new jail spawn!");
	}
}
package Jail
{
	function gameConnection::SpawnPlayer(%client)
	{
		if($jailCoordinates $= "")
			$jailCoordinates = "-12 8 0.3";
		parent::SpawnPlayer(%client);
		if(%client.jailed)
			%client.player.setTransform($jailCoordinates);
	}
	function servercmdMessageSent(%client, %msg)
	{
		if(%client.jailed)
		{
			for(%i = 0; %i < clientGroup.getCount(); %i++)
			{
				%c = clientGroup.getObject(%i);
				if(%c.jailed == 1)
				{
					announce("\c7" @ %client.name @ "\c0:" SPC %msg);
				}
			}
		}
		parent::servercmdMessageSent(%client, %msg);
	}
};
activatePackage(Jail);

//[SECTION 3]
function subtractStars(%client)
{
	%client.stars -= 1;
	%client.chatMessage("\c0Your star count has fallen to" SPC %client.stars @ ".");
}
package Stars
{
	function gameConnection::OnDeath(%this,%obj,%killer,%type,%area)
	{
		if(%client $= %killer.client)
		{
			setHUD(%killer);
		}
		else
		{
			%killer.stars++;
			if(%client.stars <= 5)
				schedule(60000, 0, subtractStars, %killer);
			if(%killer.stars > 5)
				%killer.stars = 5;
			%killer.chatMessage("\c0You have murdered" SPC %obj.client.name @ ", and now you have" SPC %killer.stars SPC "stars.");
		}
		parent::onDeath(%this, %obj,%killer,%type,%area);
	}
};
activatePackage(Stars);