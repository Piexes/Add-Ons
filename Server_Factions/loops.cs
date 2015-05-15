//This is a periodic announcement loop
function livingLoop()
{
	%amount = ClientGroup.geTcount();
	for(%cl = 0; %cl < %count; %cl++)
	{
		%client = ClientGroup.getObject(%cl);
		if(isObject(%client.player))
		{
			$livingPlayers = $livingPlayers SPC %client.name;
		}
	}
	announce("\c5Living Players: \c2" SPC $livingPlayers);
	schedule(60000, 0, livingLoop);
}