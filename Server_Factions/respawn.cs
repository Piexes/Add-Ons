//Made by Piexes. Do not distribute.
package Respawns
{
	function gameConnection::spawnPlayer(%client)
	{
		parent::spawnPlayer(%client);
		%player = %client.player;
		//Weapon Spawning
		%roll1 = getRandom(1,5);
		%roll2 = getRandom(1,5);
		%rare = getRandom(1, 20);
		//First set of items, melee/bombs.
		if(%roll1 == 1)
			%player.addNewItem("Knife-Pole");
		else if(%roll1 == 2)
			%player.addNewItem("Stick Grenade");
		else if(%roll1 == 3)
			%player.addNewItem("Cutlass");
		else if(%roll1 == 4)
			%player.addNewItem("Molotov");
		else if(%roll1 == 5)
			%player.addNewItem("Chef's Knife");
		//Now for ranged weapons
		if(%roll2 == 1)
			%player.addNewItem("Flintlock Pistols");
		if(%roll2 == 2)
			%player.addNewItem("Blunderbuss");
		if(%roll2 == 3)
			%player.addNewItem("Spear");
		if(%roll2 == 4)
			%player.addNewItem("Crossbow");
		if(%roll2 == 5)
			%player.addNewItem("Revolving Musket");
		//Announce rares!
		if(%rare == 1|| %rare == 2|| %rare == 3 || %rare == 4)
			announce("\c7The player\c0" SPC %client.name SPC "\c7has spawned with a rare item!");
		//Rare weapons, for the lucky customer.
		if(%rare == 1)
			%player.addNewItem("Dubstep Gun");
		else if(%rare == 2)
			%player.addNewItem("Riot Shield");
		else if(%rare == 3)
			%player.addNewItem("Chainsaw");
		else if(%rare == 4)
			%player.addNewItem("Butterfly Knife");
		//Reset Cooldowns
		%client.cooldown = 0;
	}
};
activatePackage(Respawns);

package Death
{
	function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc)
	{
		parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
		if(%killer != %client)
			%killer.money += 1;
	}
};
activatePackage(Death);