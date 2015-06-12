//Made by Piexes.

//TODO
//Crit Boost Emitter

//Execs
exec("./random.cs");
exec("./upgrades.cs");
exec("./viso.cs");
exec("./emitters.cs");
//Core
function roundStart()
{
	$allSentOut = 0;
	$robotsSentOut = 0;
	$robotsDefeated = 0;
	%players = ClientGroup.getcount();
	announce("\c0Announcer:\c6 Wave #" @ $wave SPC "has begun!");
	//This determines how many bots to send out based on playercount.
	if(%players == 0)
	{
		return;
	}
	else if(%players < 4)
	{
		%smallGroup = 3;
		%mediumGroup = 6;
		%largeGroup = 9;
		%bossGroup = 1;
	}
	else if(%players < 6)
	{
		%smallGroup = 6;
		%mediumGroup = 9;
		%largeGroup = 12;
		%bossGroup = 1;
	}
	else if(%players < 10)
	{
		%smallGroup = 9;
		%mediumGroup = 12;
		%largeGroup = 15;
		%bossGroup = 1;
	}
	else if(%players < 15)
	{
		%smallGroup = 12;
		%mediumGroup = 14;
		%largeGroup = 17;
		%bossGroup = 2;
	}
	else if(%players > 15)
	{
		%smallGroup = 14;
		%mediumGroup = 16;
		%largeGroup = 19;
		%bossGroup = 3;
	}
	genWave(%smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(10000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(20000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(30000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(40000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(50000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(60000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(70000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(80000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(90000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(100000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(110000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(120000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(130000, 0, genWave, %smallGroup, %mediumGroup, %largeGroup, %bossGroup);
	schedule(130000, 0, allSentOut);
}
function roundEnd(%wave, %players)
{
	%players = ClientGroup.getcount();
	initContainerRadiusSearch($botSpawn, 1000, $Typemasks::PlayerObjectType);
	while(%object = containerSearchNext())
	{
		//If bots are on the battlefeild...
		if(%object.getClassName() $= "AiPlayer")
		{
			%object.delete();
		}
	}
	//OLD CODE DELETE THIS PLEASE IN THE FUTURE WHEN I SEE IF I ACTUALLY NEED THIS STEAMING PILE OF SHIT IF STATEMENT PAIR OR NOT
	if(%lost <= 0)
	{
		announce("\c0Announcer:\c6 You defeated the robots! Good job!");
		$wave += 1;
	}
	else if(%lost > 0)
	{
		announce("\c0Announcer:\c6 You failed the wave!");
	}
	//If it's not the last wave...
	if($wave < 6)
	{
		announce("\c0Announcer:\c6 The next wave will begin in\c7 60\c6 seconds.");
		schedule(60000, 0, roundStart);
	}
	//If the last wave just ended.
	else
	{
		announce("\c0Announcer:\c6 You have completed all six waves! Congratulations!");
		announce("\c7The server's upgrades are being reset, a new round will begin in 30 seconds...");
		$wave = 1;
		for(%i=0;%i<%players;%i++)
		{
			//Update this with correct variables.
			%client = ClientGroup.getObject(%i);
			%client.money = 200;
		}
	}
}

//Bot Spawning
function spawnBots(%amount, %class)
{
	//Does it %amount amount of times.
	for(%i=0;%i<%amount;%i++)
	{
		//Decides which spawn the bot arrives to
		%roll = getRandom(1, 2);
		if(%roll == 1)
			%spawnLocation = $botSpawn;
		else if(%roll == 2)
			%spawnLocation = $altBotSpawn;
		//Creates the neccisary spawn brick. Otherwise bot kablooey-s.
		if(!isObject(%brick = nameToID(DumbRoboBrick)))
		{
			%brick = new fxDtsBrick(DumbRoboBrick)
			{
				datablock = brick1x1Data;
				isPlanted = false;
				itemPosition = 1;
				position = "0 0 -2000";
			};
		}
		//Creating the bot itself.
		%robot = new AiPlayer()
		{
			spawnTime = $Sim::Time;
			spawnBrick = %brick;
			dataBlock = PlayerNoJet;
			position = %spawnLocation;
			//Springtime for Hitler
			Name = ZombieHoleBot.hName;
			hType = ZombieHoleBot.hType;
			hSearchRadius = ZombieHoleBot.hSearchRadius;
			hSearch = ZombieHoleBot.hSearch;
			hSight = ZombieHoleBot.hSight;
			hWander = ZombieHoleBot.hWander;
			hGridWander = ZombieHoleBot.hGridWander;
			hReturnToSpawn = ZombieHoleBot.hReturnToSpawn;
			hSpawnDist = ZombieHoleBot.hSpawnDist;
			hMelee = ZombieHoleBot.hMelee;
			hAttackDamage = ZombieHoleBot.hAttackDamage;
			hSpazJump = ZombieHoleBot.hSpazJump;
			hSearchFOV = ZombieHoleBot.hSearchFOV;
			hFOVRadius = ZombieHoleBot.hFOVRadius;
			hTooCloseRange = ZombieHoleBot.hTooCloseRange;
			hAvoidCloseRange = ZombieHoleBot.hAvoidCloseRange;
			hShoot = ZombieHoleBot.hShoot;
			hMaxShootRange = ZombieHoleBot.hMaxShootRange;
			hStrafe = ZombieHoleBot.hStrafe;
			hAlertOtherBots = ZombieHoleBot.hAlertOtherBots;
			hIdleAnimation = ZombieHoleBot.hIdleAnimation;
			hSpasticLook = ZombieHoleBot.hSpasticLook;
			hAvoidObstacles = ZombieHoleBot.hAvoidObstacles;
			hIdleLookAtOthers = ZombieHoleBot.hIdleLookAtOthers;
			hIdleSpam = ZombieHoleBot.hIdleSpam;
			hAFKOmeter = ZombieHoleBot.hAFKOmeter + getRandom( 0, 2 );
			hHearing = ZombieHoleBot.hHearing;
			hIdle = ZombieHoleBot.hIdle;
			hSmoothWander = ZombieHoleBot.hSmoothWander;
			hEmote = ZombieHoleBot.hEmote;
			hSuperStacker = ZombieHoleBot.hSuperStacker;
			hNeutralAttackChance = ZombieHoleBot.hNeutralAttackChance;
			hFOVRange = ZombieHoleBot.hFOVRange;
			hMoveSlowdown = ZombieHoleBot.hMoveSlowdown;
			hMaxMoveSpeed = 1.0;
			hActivateDirection = ZombieHoleBot.hActivateDirection;
			isHoleBot = 1;
			//Winter, for poland, and france!
			hMelee = 1;
			hShoot = 1;
				hWep = "flamerImage";
			hReturnToSpawn = 0;
		};
		initContainerRadiusSearch($botSpawn, 500, $Typemasks::fxBrickObjectType);
		while(%brick = containerSearchNext())
		{
			%robot.minigame = getMinigameFromObject(%brick);
			break;
		}
		missionCleanup.add(%robot);
		//Making the robot look right.
		//This is the default template.
		%robot.setNodeColor("HEADSKIN", "0.9 0.9 0.9 1");
		%robot.setNodeColor("rhand", "0.9 0.9 0.9 1");
		%robot.setNodeColor("lhand", "0.9 0.9 0.9 1");
		%robot.setNodeColor("chest", "0 0 0.374 1");
		%robot.setNodeColor("rarm", "0.2 0.2 0.2 1");
		%robot.setNodeColor("larm", "0.2 0.2 0.2 1");
		%robot.setNodeColor("rshoe", "0 0 0 1");
		%robot.setNodeColor("lshoe", "0 0 0 1");
		%robot.setNodeColor("pants", "0 0 0 1");
		//Making specific looks for different classes.
		if(%class $= "Scout")
		{
			%robot.addNewItem("TF2 Scattergun");
			%robot.setWeapon(findItemByName("TF2 Scattergun"));
			%robot.setFaceName(memeBlockMongler);
			%robot.unhideNode(scoutHat);
			%robot.setNodeColor("scoutHat", "0 0 0.374 1");
			%robot.setDecalName(Hoodie);
			%robot.setMaxForwardSpeed(15);
		}
		else if(%class $= "Soldier")
		{
			%robot.addNewItem("TF2 Rocketlauncher");
			%robot.setWeapon(findItemByName("TF2 Rocketlauncher"));
			%robot.unhideNode("Helmet");
			%robot.setNodeColor("Helmet", "0 0 0.374 1");
			%robot.setDecalName(linkTunic);
			%robot.setFaceName(brownSmiley);
		}
		else if(%class $= "Pyro")
		{
			%robot.addNewItem("TF2 Flamethrower");
			%robot.setWeapon(findItemByName("TF2 Flamethrower"));
			%robot.unhideNode("pointyHelmet");
			%robot.setNodeColor("HEADSKIN", "0 0 0 1");
			%robot.setNodeColor("pointyHelmet", "0 0 0 1");
			%robot.setDecalName("Chef");
			%robot.setFaceName("memeYaranika");
		}
		else if(%class $= "Demoman")
		{
			%robot.addNewItem("Pipe L");
			%robot.setWeapon(findItemByName("Pipe L"));
			%robot.setFaceName("smileyPirate3");
			%robot.setDecalName("knight");
		}
		else if(%class $= "Heavy")
		{
			%robot.addNewItem("TF2 Minigun");
			%robot.setWeapon(findItemByName("TF2 Minigun"));
			%robot.setFaceName(brownSmiley);
			%robot.setDecalName(Archer);
			%robot.setMaxForwardSpeed(5);
		}
		else if(%class $= "Engineer")
		{
			%robot.setFaceName(Jamie);
			%robot.setDecalName(Worm_Engineer);
			%robot.unhideNode(knitHat);
			%robot.setNodeColor("knitHat","0 0 0.374 1");
		}
		else if(%class $= "Medic")
		{
			%robot.setDecalName(DrKleiner);
			%robot.setFaceName(asciiTerror);
			%robot.unhideNode(cape);
			%robot.unhideNode(scoutHat);
			%robot.setNodeColor("cape","0 0 0.374 1");
			%robot.setNodeColor("scoutHat","0 0 0.374 1");
		}
		else if(%class $= "giantSoldier")
		{
			%robot.addNewItem("TF2 Rocketlauncher");
			%robot.setWeapon(findItemByName("TF2 Rocketlauncher"));
			%robot.unhideNode("Helmet");
			%robot.setNodeColor("Helmet", "0 0 0.374 1");
			%robot.setDecalName(linkTunic);
			%robot.setFaceName(brownSmiley);
			%robot.setScale("3 3 3");
		}
		else if(%class $= "giantScout")
		{
			%robot.addNewItem("TF2 Scattergun");
			%robot.setWeapon(findItemByName("TF2 Scattergun"));
			%robot.setFaceName(memeBlockMongler);
			%robot.unhideNode(scoutHat);
			%robot.setNodeColor("scoutHat", "0 0 0.374 1");
			%robot.setDecalName(Hoodie);
			%robot.setMaxForwardSpeed(20);
			%robot.setScale("3 3 3");
		}
	}
}