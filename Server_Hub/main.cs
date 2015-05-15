//Made by Piexes. For Piexes. Kidz Bop. Fuck off.
function servercmdVote(%client, %vote) //Takes in the person initiating the command, and the user's input (characterized as %vote)
{
	if(%client.hasVoted $= "" || %client.hasVoted == 0) //Checks if the variable hasVoted is zero, or nonexistant.
	{
		%client.hasVoted = 1; //Sets it so that the player HAS voted.
		if(%vote $= "") //if they didn't put in any user input
		{
			%client.chatMessage("\c2This is the command where you vote for a gamemode. Please specify your choice."); //Messages them
			return; //This stops the player from running any of the code beyond this point. Remember its only if they didnt enter anything
		}
		else if(%vote $= "Dodgeball") //if the user input was dodgeball..
			$dodgeball += 1; //increases the dodgeball global variable by 1
		else if(%vote $= "Smash")
			$smash += 1;
		else //if none of those if statements are true...
			return; //stop running the code. this is so they dont see the sucessful vote cmd
		%client.chatMessage("\c2You successfully voted for " @ %vote);
	}
	else //this triggers if the person already voted, or if hasVoted is one.
		%client.chatMessage("\c5You already voted, cheater!");
}

function mapChange() //this isnt a servercommand. you need to do MapChange(); to execute it.
{
	announce("\c3PiexesBot:\c6 The gamemode is changing..."); //alerts the entire server.
	if($dodgeball > $smash) //if the dodgeball votecount is greater then the smash one
	{
		announce("\c3PiexesBot:\c6 The gamemode has been changed to\c0 SMASH\c6.");
		$gamemode = "Smash"; //sets the gamemode global variable to Smash.
	}
	else if($smash > $dodgeball) //if the smash votecoutn is greater then the dodgeball one
	{
		announce("\c3PiexesBot:\c6 The gamemode has been changed to\c0 DODGEBALL\c6.");
		$gamemode = "Dodgeball";
	}
	else if($smash == $dodgeball) // if the votecounts are equal
	{
		announce("\c3PiexesBot:\c6 It is a tie! A random map will be selected...");
		%roll = getRandom(1,2); //gets a random number between 1 and 2, and sets the output to %roll
		if(%roll == 1) //if the random number is 1
			%choice = "SMASH"; //sets the choice variable to SMASH
		else if(%roll == 2)
			%choice = "DODGEBALL";
		announce("\c3PiexesBot:\c6The \c0" @ %choice SPC "\c6gamemode has been selected!");
	}
}

function mapTick(%Tock)
{
	if(%tock $= "")
		%tock = 0;
	%tock++;
	%count = ClientGroup.getCount();
	for(%i=0; %i < %count; %i++)
	{
		%client = ClientGroup.getObject(%i);
		%client.hasVoted = 0;
	}
	mapChange();
	schedule(600000, mapTick, %Tock);
}




//Notes:
//if(isObject(%client.player))
//	%player.delete();
//%camera = %client.camera;
//%camera.setFlyMode();
//%camera.mode = "Observer";
//%client.setControlObject(%camera);
//endGame(), reset(), isMember("Playa"). %client.mnigame works
//$defaultMiniGame is a thing
//if(%obj.getClassName() $= "Player" && getMinigameFromObject(%obj) !$= "") MEANS someone is in a mini
//Setting "ALL" to A: 0.1 is like a ghost thinggy.
//learn about mounts

//%this.oldPlayer = %this.player;
//%this.player = %this.createPlayer(%this.oldPlayer.getTransform());
//%this.miniMe = %this.player;
//%this.player = %this.oldPlayer;
//%this.setControlObject(%this.minime);
//%this.oldPlayer = "";
//%this.miniMe.setPlayerShapeNameDistance(0);

//learn about cameras and mounts
//%cam.setMode(corpse, %client.player); when possessed.
//remember to package ontrigger so it doesnt do shit when possessed.
//player can scout ahead; sets the camera to fly then returns to the player.