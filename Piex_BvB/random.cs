//Assorted shit
//Tells you what wave it is
function servercmdWhatWave(%client)
{
	%client.chatMessage("\c3RADIO:\c6 It is currently wave #" @ $wave @ ".");
}
function servercmdsetLoc(%client, %num)
{
	talk("A new bot spawn location has been made!");
	if(%num == 1)
		$botSpawn = %client.player.getPosition();
	else if(%num == 2)
		$altBotSpawn = %client.player.getPosition();
	else if(%num $= "")
		%client.chatMessage("No!");
}
function servercmdMy(%client, %input)
{
	if(%input $= "")
	{
		%client.chatMessage("\c3This is the command where you can find stats about yourself!");
		%client.chatMessage("\c3Currently, you can find out your cash, and how full your canteen is.");
		%client.chatMessage("\c3/my canteen OR /my balance");
	}
	else if(%input $= "Balance")
	{
		%client.chatMessage("\c3You currently have $" @ %client.cash);
	}
	else if(%input $= "Canteen")
	{
		%client.chatMessage("\c3You canteen looks like this:" SPC %client.canteen);
	}
}

function fillCanteen(%client, %powerup)
{
	%a = getWord(%client.canteen, 0);
	%b = getWord(%client.canteen, 1);
	%c = getWord(%client.canteen, 2);
	if(%a $= "NONE")
	{
		%a = %powerup;
	}
	else if(%b $= "NONE")
	{
		%b = %powerup;
	}
	else if(%c $= "NONE")
	{
		%c = %powerup;
	}
	%client.canteen = %a SPC %b SPC %c;
}
function useCanteen(%client)
{
	%a = getWord(%client.canteen, 0);
	%b = getWord(%client.canteen, 1);
	%c = getWord(%client.canteen, 2);
	if(%a !$= "NONE")
	{
		%powerup = %a;
		%a = "NONE";
	}
	else if(%b !$= "NONE")
	{
		%powerup = %b;
		%b = "NONE";
	}
	else if(%c !$= "NONE")
	{
		%powerup = %c;
		%c = "NONE";
	}
	else
	{
		%client.chatMessage("\c3You don't have any powerups in your canteen.");
	}
	%client.canteen = %a SPC %b SPC %c;
	//Applying the boosts
	if(%powerup $= "Teleport")
	{
		if(%client.deathloc $= "")
		{
			%client.chatMessage("\c3You haven't died yet!");
			return;
		}
		if(!isObject(%client.player))
		{
			%client.spawnPlayer();
		}
		%client.player.setTransform(%client.deathloc);
		announce("\c3" @ %client.name SPC "has used the Teleport canteen!");
	}
	else if(%powerup $= "Speed")
	{
		announce("\c3" @ %client.name SPC "has used the Speed canteen!");
		%client.player.setMaxForwardSpeed(30);
		%client.player.schedule(15000, resetMovementSpeed);
	}
	else if(%powerup $= "CRITS")
	{
		%client.isCritBoosted = 1;
		schedule(15000, 0, unboost, %client);
		applyCritTrail(%client, "long");
		announce("\c3" @ %client.name SPC "has used the Artificial Crits canteen!");
	}
}
function unBoost(%client)
{
	%client.isCritBoosted = 0;
	%client.chatMessage("\c3Your Artificial Crits effect has run out.");
}
function servercmdBlandaUp(%client)
{
	if(%client.isSuperAdmin)
	{
		$gamemode = "BvB";
		%client.cash = 9999999999999999;
		%client.canteen = "NONE NONE NONE";
	}
}
//%var is used in variable naming.
function Improve(%client, %thing, %price, %maxteir, %var)
{
	if(%client.cash < %price)
	{
		%client.chatMessage("\c3You don't have enough money to buy a" SPC %thing SPC "upgrade!");
		%displayMoney = 1;
	}
	else if(%client.BvB[%var] >= %maxTeir)
	{
		%client.chatMessage("\c3You already have all" SPC %maxteir SPC "teir(s) of" SPC %thing @ ".");
	}
	else
	{
		%client.BvB[%var] += 1;
		%client.chatMessage("\c3You bought a teir of" SPC %thing @ ". The stat is" SPC %client.BvB[%var] @ "/" @ %maxteir SPC "upgraded.");
		%client.cash -= %price;
	}
}

//Procedural wave generation
function genWave(%smallGroup, %mediumGroup, %largeGroup, %bossGroup)
{
	//Rolls for what size group to spawn
	%dice = getRandom(1, 3);
	switch$(%dice)
	{
		case 1: %size = %smallGroup;
		case 2: %size = %mediumGroup;
		case 3: %size = %largeGroup;
	}
	//Rolls class to spawn
	%dice = getRandom(1, 5);
	switch$(%dice)
	{
		case 1: %class = "Scout";
		case 2: %class = "Soldier";
		case 3: %class = "Pyro";
		case 4: %class = "Demoman";
		case 5: %class = "Heavy";
	}
	spawnBots(%size, %class);
	%s = "s";
	//Rolls for giants
	%dice = getRandom(1,4);
	if(%dice == 2)
	{
		%dice = getRandom(1,2);
		if(%dice == 1)
			spawnBots(1, giantScout);
		else if(%dice == 2)
			spawnBots(1, giantSoldier);
	}
	if(%class $= "Demoman")
	{
		%class = "Demomen";
		%s = "";
	}
	else if(%class $= "Heavy")
	{
		%class = "Heavies";
		%s = "";
	}
	%count = ClientGroup.getCount();
	for(%i=0;%i<%count;%i++)
	{
		%client = ClientGroup.getObject(%i);
		%client.bottomPrint("\c0Announcer\c6: Miniwave!" SPC %size SPC %class @ %s @ ".", 1);
	}
	$robotsSentOut += %size;
}

function servercmdPenetrate(%client, %ignoreObj)
{
	%start = %client.player.getEyePoint();
	%scaled = vectorScale(%client.player.getEyeVector(), 100);
	%end = vectorAdd(%start, %scaled);
	%fin = containerRaycast(%start, %end, $Typemasks::All, %client.player);
	%collider = getWord(%fin, 0);
	if(isObject(%collider))
	{
		schedule(500, 0, fuck, %client, %fin, %collider, %scaled);
	}
	else
	{
		%client.player.setTransform(posFromRaycast(%fin));
	}
}
function fuck(%client, %fin, %collider, %scaled, %noloop)
{
	%start = posFromRaycast(%fin);
	%end = vectorAdd(%start, %scaled);
	%fin = containerRaycast(%start, %end, $Typemasks::All, %collider);
	%collider = getWord(%fin, 0);
	%client.player.setTransform(posFromRaycast(%fin));
	if(isObject(%collider))
	{
		if(%noloop $= "")
			schedule(50, 0, fuck, %client, %fin, %collider, %scaled);
		%client.bottomPrint("\c3The script is still processing...", 1);
	}
	else
	{
		%client.chatMessage("\c3It worked! You're out.");
		if(%noloop $= "")
			schedule(50, 0, fuck, %client, %fin, %collider, %scaled, 1);
	}
}
//Determines if the wave is over.
function shouldEnd()
{
	if($robotsSentOut == $robotsDefeated)
	{
		roundEnd();
		return 1;
	}
	else
	{
		return 0;
	}
}

function allSentOut()
{
	$allSentOut = 1;
}