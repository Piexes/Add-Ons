if(isPackage(lightabilities))
    deactivatePackage(lightabilities);

package lightabilities
{
	//Evals.
	//Refresh HUD.
	function evalRefreshHUD(%client)
	{
		%client.bottomprint("\c2Ability:\c6" SPC %client.power SPC "\c2Souls:\c6" SPC %client.souls);
	}
    //Normal-size as chance.
    function evalNormalMe(%client)
    {
        %client.player.setScale("1 1 1");
        %client.player.setmaxForwardSpeed(11);
    }
	//Uncloak as invisibility.
	function evalReappear(%client)
	{
    	%client.cloaked = "false";
    	%client.applyBodyParts();
    	%client.player.unHideNode("HEADSKIN");
	}
    //Ragin'
    function evalSpearSpam(%client)
    {  
        //Defining our variables
        %eyeVector = %client.player.getEyeVector();
        %eyePoint = %client.player.getEyePoint();
        //Adding them together.
        %result = vectorAdd(%eyePoint, %eyeVector);
        //Now for spawning the spear thing.
        %p = new Projectile()
        {
            dataBlock = spearProjectile;
            initialVelocity = vectorScale(%eyeVector, 30);
            initialPosition = %result;
            client = %client;
            sourceObject = %client.player;
            sourceClient = %client;
        };
        MissionCleanup.add(%p);
    }
	//When you press the light, code happens based on your power variable.
    function servercmdLight(%client)
    {
        if(%client.power $= "speed")
        {
            %client.player.setmaxforwardSpeed(1000);
            %client.player.schedule(2000,setmaxForwardSpeed,20);
            %client.bottomprint("\c2Boost Engaged!");
            schedule(1000, 0, evalRefreshHUD, %client);
            return;
        }
        else if(%client.power $= "invisibility")
        {
            if(%client.cooldown + 20 < $Sim::Time)
            {
                %client.cooldown = $sim::time;
                %client.player.hideNode("ALL");
                %client.bottomprint("\c2Cloak Engaged!");
                schedule(15000, 0, evalRefreshHUD, %client);
                schedule(15000, 0, evalReappear, %client);  
            }
            return;
        }
        else if(%client.power $= "shapeshifting")
        {
            //Checking which form to change into.
            if(%client.human $= "True")
            {
                %client.human = "False";
                %client.player.setdatablock("airDragonArmor");
            }
            else if(%client.human $= "False")
            {
                %client.human = "True";
                %client.player.setDataBlock("playerNoJet");
            }
        }
        else if(%client.power $= "flight")
        {
            if(%client.cooldown + 5 < $Sim::Time)
            {
                if(%client.jumpCount < 5)
                {
                    %client.player.addRelativeVelocity("0 20 20");
                    %client.jumpCount += 1;
                }
                else if(%client.jumpCount >= 5)
                {
                    %client.cooldown = $Sim::Time;
                    %client.jumpCount = 0;
                }
            }
            else
            {
                %cooldowntime = %client.cooldown + 12 - $Sim::Time;
                %client.bottomprint("\c2You need to wait to fly again!");
                schedule(2000, 0, evalRefreshHUD, %client);
            }
        }
        else if(%client.power $= "Teleportation")
        {
            if(%client.cooldown + 12 < $Sim::Time)
            {
                %client.cooldown = $sim::time;
                if(%client.pos)
                {
                    %client.player.setTransform(%client.x SPC %client.y SPC %client.z);
                }
                else
                {
                    messageClient(%client, '',"\c2You need to set a checkpoint first! Do /checkpoint.");
                }
            }
            else
            {
                %cooldowntime = %client.cooldown + 12 - $Sim::Time;
                %client.bottomprint("\c2You need to wait" SPC %cooldowntime SPC "seconds to do that again!");
                schedule(2000, 0, evalRefreshHUD, %client);
            }
        }
        else if(%client.power $= "Breath")
        {
            if(%client.cooldown + 2 < $Sim::Time)
            {
                //For ease of use.
                %player = %client.player;
                //Defining my stuff, also ease of use.
                %eyeVector = %player.geteyeVector();
                %eyePoint = %player.geteyePoint();
                //The start of the vector.
                %start = %eyePoint;
                //A variable used for math later.
                %scaledEyeVector = vectorScale(%eyeVector, 100);
                //End point.
                %end = vectorAdd(%start, %scaledEyeVector);
                %breeze = containerRayCast(%start, %end, $TypeMasks::PlayerObjectType, %player);
                //Now to find out if its a player, and then execute the knockback code.
                %target = getWord(%breeze, 0);
                if(isObject(%target))
                {
                    %client.cooldown = $Sim::Time;
                    %target.addRelativeVelocity("0 30 30");
                    %target.setmaxForwardSpeed(2);
                    schedule(10000, 0, evalNormalMe, %target.client);
                }
                else
                {
                    %client.bottomprint("\c2You aren't aiming at a player!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
            }
            else
            {
                %cooldowntime = %client.cooldown + 2 - $Sim::Time;
                %client.bottomPrint("\c2You have to wait" SPC %cooldowntime SPC "seconds to do that again!");
            }
        }
        else if(%client.power $= "Conjuration")
        {
            if(%client.cooldown + 12 < $Sim::Time)
            {
                %client.cooldown = $Sim::Time;
                schedule(100, 0, evalSpearSpam, %client);
                schedule(200, 0, evalSpearSpam, %client);
                schedule(300, 0, evalSpearSpam, %client);
                schedule(400, 0, evalSpearSpam, %client);
                schedule(500, 0, evalSpearSpam, %client);
            }
            else
            {
                %cooldowntime = %client.cooldown + 12 - $Sim::Time;
                %client.bottomprint("\c2You need to wait" SPC %cooldowntime SPC "seconds to do that again!");
                schedule(2000, 0, evalRefreshHUD, %client);
            }
        }
        else if(%client.power $= "Combustion")
        {
            if(%client.cooldown + 5 < $Sim::Time)
            {
                %client.cooldown = $Sim::Time;
                //Defining our variables
                %eyeVector = %client.player.getEyeVector();
                %eyePoint = %client.player.getEyePoint();
                //Adding them together.
                %result = vectorAdd(%eyePoint, %eyeVector);
                //Now for spawning the spear thing.
                %p = new Projectile()
                {
                    dataBlock = rocketLauncherProjectile;
                    initialVelocity = vectorScale(%eyeVector, 100);
                    initialPosition = %result;
                    client = %client;
                    sourceObject = %client.player;
                    sourceClient = %client;
                };
            }
            else
            {
                %cooldowntime = %client.cooldown + 5 - $Sim::Time;
                %client.bottomprint("\c2You need to wait" SPC %cooldowntime SPC "seconds to do that again!");
                schedule(2000, 0, evalRefreshHUD, %client);
            }
        }
        else if(%client.power $= "Chance")
        {
            if(%client.cooldown + 10 < $Sim::Time)
            {
                %client.cooldown = $Sim::Time;
                %player = %client.player;
                %diceRoll = getRandom(1, 8);
                //Growth
                if(%diceRoll == 1)
                {
                    %player.setScale("3 3 3");
                    %client.bottomPrint("\c2You rolled for \c6growth\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                    schedule(10000, 0, evalNormalMe, %client);
                }
                //Lunge
                if(%diceRoll == 2)
                {
                    %player.addRelativeVelocity("0 20 20");
                    %client.bottomPrint("\c2You rolled for \c6lunge\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
                //Fast
                if(%diceRoll == 3)
                {
                    %player.setmaxForwardSpeed(40);
                    %client.bottomPrint("\c2You rolled for \c6speed\c2!");
                    schedule(10000, 0, evalNormalMe, %client);
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
                //Slow
                if(%diceRoll == 4)
                {
                    %player.setmaxForwardSpeed(2);
                    %client.bottomPrint("\c2You rolled for \c6sloth speed\c2!");
                    schedule(5000, 0, evalNormalMe, %client);
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
                //Crossbow Sentry
                if(%diceRoll == 5)
                {
                    %player.addNewItem("Crossbow Turret");
                    %client.bottomPrint("\c2You rolled for \c6the crossbow turret\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
                //Kamakaze
                if(%diceRoll == 6)
                {
                    %player.spawnExplosion(rocketlauncherProjectile, 100);
                    %client.bottomPrint("\c2You rolled for \c6kamakaze!\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                }
                //Blastoff
                if(%diceRoll == 7)
                {
                    %client.bottomPrint("\c2You rolled for \c6blastoff\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                    %player.addRelativeVelocity("0 0 100");
                }
                //The drowns
                if(%diceRoll == 8)
                {
                    %client.player.setTransform("-17 -70 0.3");
                    %client.bottomPrint("\c2You rolled for \c6the drowns\c2!");
                    schedule(2000, 0, evalRefreshHUD, %client);
                    %altCooldown = 0;
                }
            }
            else
            {
                %client.bottomprint("\c2You need to wait to do that again!");
                schedule(2000, 0, evalRefreshHUD, %client);
            }
        }
    }

    function Armor::OnTrigger(%armor, %player, %slot, %value)
    {
        %client = %player.client;
        if(%slot == 4)
        {
            if(%client.power $= "Conjuration")
            {
                if(%client.altCooldown + 2 < $Sim::Time)
                {
                    %client.altCooldown = $Sim::Time;
                    //You have to add these two together.
                    %eyeVector = %player.getEyeVector();
                    %eyePoint = %player.getEyePoint();
                    //Ta-daa
                    %result = vectorAdd(%eyeVector, %eyePoint);
                    //Makes the projectile
                    %p = new Projectile()
                    {
                        dataBlock = spearProjectile;
                        initialVelocity = vectorScale(%eyeVector, 100);
                        initialPosition = %result;
                        client = %client;
                        sourceObject = %client.player;
                        sourceClient = %client;
                    };
                }
            }
            else if(%client.power $= "Speed")
            {
                if(%client.altCooldown + 5 < $Sim::Time)
                {
                    %client.player.addRelativeVelocity("0 0 20");
                    %client.altCooldown = $Sim::Time;
                }
            }
            else if(%client.power $= "Flight")
            {
                if(%client.altCooldown + 5 < $Sim::Time)
                {
                    %client.altCooldown = $Sim::Time;
                    %client.player.addRelativeVelocity("0 50 0");
                }
            }
            else if(%client.power $= "Breath")
            {
                if(%client.altCooldown + 4 < $Sim::Time)
                {
                    %client.altCooldown = $Sim::Time;
                    %client.player.addRelativeVelocity("0 3000 2");
                }
            }
            else if(%client.power $= "Combustion")
            {
                if(%client.altCooldown + 10 < $Sim::Time)
                {
                    %client.altCooldown = $Sim::Time;
                    %client.player.spawnExplosion(tMolotovProjectile, 5);
                    %client.player.addNewItem("Greek Fire");
                    %client.player.addNewItem("Greek Bomb");
                    %client.player.addNewItem("Fire Bomb");
                }
            }
            else if(%client.power $= "Shapeshifting")
            {
                if(%client.altCooldown + 4 < $Sim::Time)
                {
                    %client.altCooldown = $Sim::Time;
                    //Defining our variables
                    %eyeVector = %client.player.getEyeVector();
                    %eyePoint = %client.player.getEyePoint();
                    //Adding them together.
                    %result = vectorAdd(%eyePoint, %eyeVector);
                    //Now for spawning the spear thing.
                    %p = new Projectile()
                    {
                        dataBlock = tf2rlProjectile;
                        initialVelocity = vectorScale(%eyeVector, 30);
                        initialPosition = %result;
                        client = %client;
                        sourceObject = %client.player;
                        sourceClient = %client;
                    };
                }
            }
        }
        return Parent::onTrigger(%armor, %player, %slot, %value);
    }
};
activatePackage(lightabilities);