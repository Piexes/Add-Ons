//This section is hella messy.
function servercmdsetClass(%client, %class)
{
	if(%class $= "Scout" || %class $= "Pyro" || %class $= "Soldier" || %class $= "Demoman" || %class $= "Heavy" || %class $= "Engineer" || %class $= "Medic" || %class $= "Sniper" || %class $= "Spy")
	{
		%client.class = %class;
		%client.player.kill();
	}
	else
	{
		%client.chatMessage("\c3That's not a class! The classes are scout, pyro, soldier, demoman, heavy, engineer, medic, sniper, and spy.");
	}
}

package MvMClasses
{
	//Item Giving
	function gameConnection::spawnPlayer(%client)
	{
		parent::spawnPlayer(%client);
		if(%client.class !$= "")
		{
			if(%client.class $= "Scout")
			{
				%client.player.addNewItem("The Force-A-Nature");
				%client.player.addNewItem("TF2 Scattergun");
				%client.player.addNewItem("TF2Pistol");
				%client.player.addNewItem("The Sandman");
				%client.player.setDataBlock("PlayerTF2Scout");
			}
			else if(%client.class $= "Soldier")
			{
				%client.player.addNewItem("TF2 Rocket Launcher");
				%client.player.addNewItem("TF2 Shotgun");
				%client.player.addNewItem("Shovel");
				%client.player.setDataBlock("PlayerTF2Soldier");
			}
			else if(%client.class $= "Pyro")
			{
				%client.player.addNewItem("TF2 Flamethrower");
				%client.player.addNewItem("TF2 Flaregun, Red");
				%client.player.addNewItem("TF2 Shotgun");
				%client.player.addNewItem("Fire Axe");
				%client.player.setDataBlock("PlayerTF2Pyro");
			}
			else if(%client.class $= "Demoman")
			{
				%client.player.addNewItem("Pipe L");
				%client.player.addNewItem("Stickybomb L");
				%client.player.addNewItem("Bottle");
				%client.player.setDataBlock("PlayerTF2Demoman");
			}
			else if(%client.class $= "Heavy")
			{
				%client.player.addNewItem("TF2 Minigun");
				%client.player.addNewItem("TF2 Shotgun");
				%client.player.addNewItem("Fists");
				%client.player.setDataBlock("PlayerTF2Heavy");
			}
			else if(%client.class $= "Engineer")
			{
				%client.player.addNewItem("TF2 Shotgun");
				%client.player.addNewItem("TF2Pistol");
				%client.player.addNewItem("TF2 Wrench");
				%client.player.setDataBlock("PlayerTF2Engineer");
			}
			else if(%client.class $= "Medic")
			{
				%client.player.addNewItem("Bonesaw");
				%client.player.setDataBlock("PlayerTF2Medic");
			}
			else if(%client.class $= "Sniper")
			{
				%client.player.addNewItem("TF2Sniper");
				%client.player.addNewItem("TF2 SMG");
				%client.player.addNewItem("Kukri");
				%client.player.setDataBlock("PlayerTF2Sniper");
			}
		}
		else if(%client.class $= "" && $gamemode $= "BvB")
		{
			%client.chatMessage("\c4You don't have a class! The script has automatically chosen one for you. Select your own with /setClass.");
			%client.class = "Soldier";
			%client.schedule(33, instantRespawn);
		}
	}
	//Teleport canteen
	function gameConnection::onDeath(%client, %killer, %killerPlayer, %damageType, %damageLoc)
	{
		if($gamemode $= "BvB")
		{
			%client.deathloc = %client.player.getPosition();
			if(%client.BvBInstant >= 1)
			{
				%client.schedule(100, instantRespawn);
			}
		}
		parent::onDeath(%client, %killer, %killerPlayer, %damageType, %damageLoc);
	}
	//Crits
	function Player::Damage(%this, %projectile, %position, %damage, %type)
	{
		if($gamemode $= "BvB")
		{
			%killer = %projectile.client;
			if(%killer.isCritBoosted == 1)
			{
				%damage *= 4;
			}
			%dice = getRandom(1, 15);
			//Luck Upgrade
			if(%killer.BvBCritical > 0)
			{
				if(%killer.BvBCritical == 1)
					%dice = getRandom(1, 10);
				else if(%killer.BvBCritical == 2)
					%dice = getRandom(1, 7);
				else if(%killer.BvBCritical == 3)
					%dice = getRandom(1, 5);
			}
			if(%dice == 3)
			{
				%damage *= 5;
				applyCritTrail(%killer, "short");
				%killer.bottomPrint("\c0Critical \c6Hit!", 1);
			}
			//Reduction upgrade.
			if(%killer.BvBResist >= 1)
			{
				if(%killer.BvBResist == 1)
					%damage *= 0.80;
				else if(%killer.BvBResist == 2)
					%damage *= 0.60;
			}
			//Addition upgrade
			if(%killer.BvBDamage > 0)
			{
				if(%killer.BvBDamage == 1)
					%damage *= 1.2;
				else if(%killer.BvBDamage == 2)
					%damage *= 1.4;
			}
		}
		parent::Damage(%this, %projectile, %position, %damage, %type);
	}
	//Canteen usage
	function servercmdLight(%client)
	{
		if($gamemode $= "BvB")
		{
			useCanteen(%client);
			return;
		}
		parent::servercmdLight(%client);
	}
	//Money spawns
	function fxDTSBrick::OnBotDeath(%spawnBrick, %client)
	{
		%client.dump();
		if($gamemode $= "BvB")
		{
			%dice = getRandom(20, 50);
			if(%client.BvBMoney >= 1)
				%dice *= 1.5;
			%client.cash += %dice;
			%client.bottomPrint("\c2+" @ %dice SPC "cash! $" @ %client.cash SPC "total.", 3);
			$robotsDefeated += 1;
			if($allSentOut == 1)
				shouldEnd();
		}
		parent::onDeath(%client, %this);
	}
	//If I dont do this satan will come and punch me in the balls
	function gameConnection::onClientEnterGame(%this)
	{
		%this.cash = 200;
		%this.canteen = "NONE NONE NONE";
		parent::onCliententerGame(%this);
	}
};
activatePackage(MvMClasses);

function servercmdUpgrade(%client, %upg, %isBuying)
{
	%class = %client.class;
	%cash = %client.cash;
	if(%upg $= "")
	{
		%client.chatMessage("\c3Welcome to the upgrade system for the\c6" SPC %class @ "\c3.");
		%client.chatMessage("\c3The syntax for this command is /upgrade selectionName. selectionName is the first word of the upgrade/canteen.");
		%client.chatMessage("\c3Do /upgrade list for a list of all possible selections, or /upgrade canteen to see the canteen list.");
	}
	else if(%upg $= "List")
	{
		%client.chatMessage("\c3UPGRADE LIST");
		%client.chatMessage("\c6Damage - 2 Teirs - $400 EACH. - \c7Increases your overall damage by 20% per tier.");
		%client.chatMessage("\c6Instant Respawn - 1 Teir - $700 EACH.");
		%client.chatMessage("\c6Critical Chance - 3 Teirs - $300 EACH. - \c7Increases your critical chance per tier.");
		%client.chatMessage("\c6Money Multiplier - 1 Teir - $900 EACH. - \c7Increases incoming money by x1.5.");
		%client.chatMessage("\c6Resist Damage - 1 Teir - $500 EACH. - \c7Decreases incoming damage by 20% per tier.");
	}
	else if(%upg $= "Canteen")
	{
		%client.chatMessage("\c3CANTEEN LIST");
		%client.chatMessage("\c6Artificial Crits - $100 EACH - \c7Temporarily grants you x4 damage for a short amount of time.");
		%client.chatMessage("\c6Teleport - $150 EACH - \c7Teleports you to your spawn location. Works even in death.");
		%client.chatMessage("\c6Speed - $100 EACH - \c7Increases your speed dramatically for a short amount of time.");
		%client.chatMessage("\c6Canteens are usable by pressing your light key, usually R.");
		%client.chatMessage("\c6Select a canteen by saying /upgrade canteen");
	}
	//Canteens
	//Theres the fillCanteen script in random.cs that is crucial
	else if(strPos(%client.canteen, "NONE") >= 0 && %upg $= "Artificial" || %upg $= "Teleport" || %upg $= "Speed")
	{
		//I want to make an else statement after this one if the player has no space, but can't.
		//That would end the giant-ass one I'm making that I need to continue.
		//So I created this variable, which tells me that this else if ended up true.
		%theresSpace = 1;
		//Artificial Critical Hits
		if(%upg $= "Artificial")
		{
			if(%client.cash < 100)
			{
				%client.chatMessage("\c3You don't have enough money to buy Artificial Crits!");
				%displayMoney = 1;
			}
			else
			{
				%client.chatMessage("\c3You bought a single Artificial Crit canteen.");
				%client.cash -= 100;
				fillCanteen(%client, "CRITS");
			}
		}
		//Teleportation
		else if(%upg $= "Teleport")
		{
			if(%client.cash < 150)
			{
				%client.chatMessage("\c3You don't have enough money to buy Teleport!");
				%displayMoney = 1;
			}
			else
			{
				%client.chatMessage("\c3You bought a single Teleport canteen.");
				%client.cash -= 150;
				fillCanteen(%client, "TELEPORT");
			}
		}
		//Speed
		else if(%upg $= "Speed")
		{
			if(%client.cash < 100)
			{
				%client.chatMessage("\c3You don't have enough money to buy Speed!");
				%displayMoney = 1;
			}
			else
			{
				%client.chatMessage("\c3You bought a single Speed canteen.");
				%client.cash -= 100;
				fillCanteen(%client, "SPEED");
			}
		}
	}
	//From here on out it is regular upgrades. The improve function is stored in random.cs
	//%upg is used as a variable name.
	else if(%upg $= "Movement")
	{
		%thing = "Movement Speed";
		%price = 200;
		%maxteir = 2;
		improve(%client, %thing, %price, %maxteir, "speed");
	}
	else if(%upg $= "Damage")
	{
		%thing = "Damage";
		%price = 400;
		%maxteir = 2;
		improve(%client, %thing, %price, %maxteir, %upg);
	}
	else if(%upg $= "Instant")
	{
		%thing = "Instant Respawn";
		%price = 700;
		%maxteir = 1;
		improve(%client, %thing, %price, %maxteir, %upg);
	}
	else if(%upg $= "Critical")
	{
		%thing = "Critical Chance";
		%price = 300;
		%maxteir = 3;
		improve(%client, %thing, %price, %maxteir, %upg);
	}
	else if(%upg $= "Money")
	{
		%thing = "Money Multiplier";
		%price = 900;
		%maxteir = 1;
		improve(%client, %thing, %price, %maxteir, %upg);
	}
	else if(%upg $= "Resist")
	{
		%thing = "Resist Damage";
		%price = 500;
		%maxteir = 1;
		improve(%client, %thing, %price, %maxteir, %upg);
	}
	//Displays the money they have, if needed.
	if(%displayMoney == 1)
	{
		if(%client.cash $= "")
			%client.cash = 0;
		%client.chatMessage("\c3You only have $" @ %client.cash);
	}
	if(%alreadyDone == 1)
	{
		%client.chatMessage("\c3You already have all the upgrades for this selection.");
	}
	//This executes if they tried to get a canteen thing, and %theresSpace (in their canteen) is at zero.
	if(%theresSpace == 0)
	{
		if(%upg $= "Artificial" || %upg $= "Teleport" || %upg $= "Speed")
		{
			%client.chatMessage("\c3Your canteen is full!");
			%client.chatMessage("\c3It looks like this:" SPC %client.canteen);
		}
	}
}