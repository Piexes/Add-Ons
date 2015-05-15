package MapVoting
{
	function servercmdVoteMap(%client, %vote)
	{
		
		if(%client.firstTime $= "false")
		{
			messageClient(%client, '',"\c2You already voted!");
			talk("fak");
		}
		else
		{
			if(%vote $= "tundra")
			{
				$tundraVote += 1;
				%client.firstTime = "false";
				messageClient(%client, '',"\c2You voted for TUNDRA!");
			}
			else if(%vote $= "dam")
			{
				$damVote += 1;
				%client.firstTime = "false";
				messageClient(%client, '',"\c2You voted for DAM!");
			}
			else
			{
				messageClient(%client, '',"\c2That's not a map!");
				messageClient(%client, '',"\c2Do /listmaps to see all of them.");
			}

		}
	}

	//Shows the client possible maps.
	function servercmdListMaps(%client)
	{
		messageClient(%client, '',"\c2MAPS:");
		messageClient(%client, '',"\c3Tundra: \c6" SPC $tundraVote SPC "Votes!");
		messageClient(%client, '',"\c3Dam: \c6" SPC $damVote SPC "Votes!");
	}

	//Debug command, checks if you already voted.
	function servercmdCheckTime(%client)
	{
		talk("First time =" SPC %client.firstTime);
	}
	//Another debug. Sets clientvote.
	function servercmdFUCK(%client)
	{
		%client.firstTime = "true";
	}
};
activatePackage(MapVoting);