function servercmdHelp(%client)
{
    messageClient(%client, '',"\c2Welcome to SuperPowered!");
    messageClient(%client, '',"\c6Use the command /setpower to choose a class!");
    messageClient(%client, '',"\c6Then, you can press your light key to activate your special ability!");
    messageClient(%client, '',"\c6Some classes also have an ability that can be activated by jetting.");
    messageClient(%client, '',"\c6/startMusicVote can initiate a vote to change server-wide music.");
    messageClient(%client, '',"\c6The command /setPower lists all of the available powers! /buy allows you to spend souls!");
}
function servercmdsetPower(%client, %input)
{
    if(%input $= "superspeed")
    {
        %client.power = "Speed";
        messageClient(%client, '',"\c3Your superpower has been set to superspeed!");
        %client.player.kill();
    }
    else if(%input $= "invisibility") //Additem,
    {
        %client.power = "Invisibility";
        messageClient(%client, '',"\c3Your superpower has been set to invisibility!");
        %client.player.kill();
    } 
    else if(%input $= "Flight")
    {
        %client.power = "Flight";
        messageClient(%client, '',"\c3Your superpower has been set to flight!");
        %client.player.kill();
    }
    else if(%input $= "shapeshifting")
    {
        %client.power = "Shapeshifting";
        messageClient(%client, '',"\c3Your superpower has been set to shapeshifting!");
        %client.player.kill();
    }
    else if(%input $= "teleportation")
    {
        %client.power = "Teleportation";
        messageClient(%client, '',"\c3Your superpower has been set to teleportation!");
        %client.player.kill();
    }
    else if(%input $= "conjuration")
    {
        %client.power = "Conjuration";
        messageClient(%client, '',"\c3Your superpower has been set to conjuration!");
        %client.player.kill();
    }
    else if(%input $= "combustion")
    {
        %client.power = "Combustion";
        messageClient(%client, '',"\c3Your superpower has been set to combustion!");
        %client.player.kill();
    }
    else if(%input $= "breath")
    {
        %client.power = "Breath";
        messageClient(%client, '',"\c3Your superpower has been set to breath!");
        %client.player.kill();
    }
    else if(%input $= "chance")
    {
        %client.power = "Chance";
        messageClient(%client, '',"\c3Your superpower has been set to chance!");
        %client.player.kill();
    }
    else if(%input $= "possession")
    {
        %client.power = "Possession";
        messageClient(%client, '',"\c3Your superpower has been set to possession!");
        %client.player.kill();
    }
    else
    {
        messageClient(%client, '',"\c3That isn't a superpower!");
        messageClient(%client, '',"\c3The current powers are: Flight, Superspeed, Shapeshifting, Breath, Chance, Teleportation, Conjuration, Combustion, Possession, and Invisibility.");
    }
}

function servercmdcheckpoint(%client)
{
    //Getting the coordinates of a player.
    %client.pos = %client.player.getPosition();
    //Affirming that the coordinates have been set.
    messageClient(%client, '',"\c2Checkpoint set!");
    //Sets the xyz variables to equal the xyz of the player.
    %client.x = getWord(%client.pos,0);
    %client.y = getWord(%client.pos,1);
    %client.z = getWord(%client.pos,2);
}

function servercmdBuy(%client, %category)
{
    if(%category $= "speedboost")
    {
        if(%client.souls >= 10)
        {
            if(isObject(%client.player))
            {
                announce("\c2The player" SPC %client.name SPC "has bought a speedboost!");
                %client.player.setmaxForwardSpeed(40);
                %client.souls -= 10; 
                %client.bottomprint("\c2Ability:\c6" SPC %client.power SPC "\c2Souls:\c6" SPC %client.souls);
            }
            else
            {
                messageClient(%client, '',"\c2You need to be alive to buy this item!");
            }
        }
        else
        {
            messageClient(%client, '',"\c2You don't have enough souls! You need 10.");
        }
    }
    else if(%category $= "Noscope")
    {
        if(%client.souls >= 10)
        {
            if(isObject(%client.Player))
            {
                announce("\c2The player" SPC %client.name SPC "has bought the 360 Noscope Rifle!");
                %client.player.addNewItem("Bear");
                %client.souls -= 10;
                %client.bottomPrint("\c2Ability:\c6" SPC %client.power SPC "\c2Souls:\c6" SPC %client.souls);
            }
            else
            {
                messageClient(%client, '',"\c2You need to be alive to buy this item!");
            }
        }
        else
        {
            messageClient(%client, '',"\c2You don't have enough souls! You need 10.");
        }
    }
    else if(%category $= "Happy")
    {
        if(%client.souls >= 10)
        {
            if(isObject(%client.player))
            {
                announce("\c2The player" SPC %client.name SPC "has bought Happy McFappy!");
                %client.player.addNewItem("Happy McFappy");
                %client.souls -= 10; 
                %client.bottomprint("\c2Ability:\c6" SPC %client.power SPC "\c2Souls:\c6" SPC %client.souls);
            }
            else
            {
                messageClient(%client, '',"\c2You need to be alive to buy this item!");
            }
        }
        else
        {
            messageClient(%client, '',"\c2You don't have enough souls! You need 10.");
        }  
    }
    else if(%category $= "Dubstep")
    {
        if(%client.souls >= 10)
        {
            if(isObject(%client.player))
            {
                announce("\c2The player" SPC %client.name SPC "has bought the Dubstep Gun!");
                %client.player.addNewItem("Dubstep");
                %client.souls -= 10; 
                %client.bottomprint("\c2Ability:\c6" SPC %client.power SPC "\c2Souls:\c6" SPC %client.souls);
            }
            else
            {
                messageClient(%client, '',"\c2You need to be alive to buy this item!");
            }
        }
        else
        {
            messageClient(%client, '',"\c2You don't have enough souls! You need 10.");
        }  
    }
    else
    {
        messageClient(%client, '',"\c2Welcome to the shop!");
        messageClient(%client, '',"\c6Here you can buy special powers with souls.");
        messageClient(%client, '',"\c6You have" SPC %client.souls SPC "souls.");
        messageClient(%client, '',"\c6Right now, you can buy \c2speedbosts\c6, the \c2Noscope Rifle\c6, the Dubstep Gun, and \c2Happy McFappy\c6.");
        messageClient(%client, '',"\c6Do /buy itemname.");
    }
}

function Pixpies(%a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l)
{
    announce("\c2Piexes:\c6" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j SPC %k);
}

function Metardio(%a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l)
{
    announce("\c2Metario:\c6" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j SPC %k);
}

function servercmdKill(%client, %target)
{
    if(%client.isSuperAdmin)
    {
        %targetclient = findclientbyname(%target);
        %targetclient.player.kill();
        messageClient(%targetClient, '',"\c2You have been forcekilled by a Super Admin!");
        messageClient(%client, '',"\c2The player has been killed!");
    }
    else
    {
        messageClient(%client, '',"\c2You aren't super admin! Scram!");
    }
}

function servercmdNerf(%client, %a, %b, %c, %d, %e, %f)
{
    if(%client.isSuperAdmin)
    {
        announce("\c2The player\c6" SPC %client.name SPC "\c2has demanded to nerf\c6" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f);
        $NerfCooldown = $Sim::Time;
    }
    else
    {
        messageClient(%client, '',"\c2You aren't super admin! This could be really fucking spammable it wasn't admin only. Please understand.");
    }
}

function servercmdMsg(%client, %target, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j)
{
    if(%client.message + 5 < $Sim::Time)
    {
        %client.message = $Sim::Time;
        %realTarget = findclientbyname(%target);
        messageClient(%realTarget, '',"\c2[PM]" SPC %client.name @ ":\c6" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j);
        messageClient(%client, '',"\c2[PM]" SPC %client.name @ ":\c6" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j);
        echo("[PM to" SPC %realTarget.name @ "]" @ %client.name @ ":" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j);
    }
}

function servercmdShutdown(%client)
{
    if(%client.isSuperAdmin)
    {
        quit();
    }
    else
    {
        talk("JESUS CHRIST! The player" SPC %client.name SPC "just tried to crash the server!");
        talk("How did he even know about that command?");
        talk("Tell anyone and I will personally slice your throat");
    }
}

function servercmdFake(%client, %input, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j)
{
    if(%client.isSuperAdmin)
    {
        announce("\c3" @ %input @ "\c6:" SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j);
    }
}

function servercmdnewFX(%client)
{
    if(%client.isSuperAdmin)
    {
        new FXPlane();
    }
    else
    {
        messageClient(%client, '',"\c2You aren't super admin!");
    }
}

function servercmdkillGround(%client)
{
    if(%client.isSuperAdmin)
    {
        groundPane.delete();
    }
}

function servercmdDemocracy(%client, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j)
{
    if(%client.isSuperAdmin)
    {
        announce("\c2PiexBot:\c6 The vote," SPC %a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h SPC %i SPC %j SPC "has started.");
        announce("\c2PiexBot:\c6 Use the commands /yup and /nope to vote!");
    }
}

function servercmdNope(%client)
{
    if(%client.voted = "true")
    {
        messageClient(%client, '',"\c2You already voted!");
    }
    else
    {
        %client.voted = "true";
        messageClient(%client, '',"\c2Thanks for voting!");
        $nope += 1;
    }
}

function servercmdYup(%client)
{
    if(%client.voted = "true")
    {
        messageClient(%client, '',"\c2You already voted!");
    }
    else
    {
        %client.voted = "true";
        messageClient(%client, '',"\c2Thanks for voting!");
        $yup += 1;
    }
}

function servercmdTally(%client)
{
    announce("\c2PiexBot:\c6 The YUP side received\c2" SPC $yup SPC "\c6votes!");
    announce("\c2PiexBot:\c6 The NOPE side received\c2" SPC $nope SPC "\c6votes!");
}

function servercmdgetRandom(%client, %a, %b)
{
    %result = getRandom(%a, %b);
    announce("\c2Piexbot: \c6The selected random number is" SPC %result);
}

function servercmdLocation(%client)
{
    %pos = %client.player.getPosition();
    announce("\c2PiexBot: \c6Coordinates are\c3" SPC %pos);
}

function servercmdSA(%client)
{
    if(%client.isSuperAdmin)
    {
        %client.player.setTransform("25 13 19");
    }
    else
    {
        messageClient(%client, '',"\c2PiexBot:\c6 Scram!");
    }
}

function servercmdHammertime(%client)
{
    if(%client.isSuperAdmin)
    {
        %player = %client.player;
        %player.addNewItem("Hammer");
    }
}

function servercmdRequest(%client, %item)
{
    if(%client.isSuperAdmin)
    {
        %client.player.addNewItem(%item);
    }
}

function servercmdConnect(%client, %a, %status)
{
    announce("\c1" @ %a SPC "has joined the game.");
    if(%status $= "Admin")
        announce("\c2" @ %a SPC "has become Admin (Auto)");
    if(%status $= "Super")
        announce("\c2" @ %a SPC "has become Super Admin (Auto)");
}

function servercmdSpook(%client, %a)
{
    announce("\c1" @ %a SPC "has joined the game.");
    announce("\c1" @ %a SPC "has left the game.");
}

function servercmdDisconnect(%client, %a)
{
    announce("\c1" @ %a SPC "has left the game.");
}

function servercmdFuckall(%client)
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
    %p = new Projectile()
    {
        dataBlock = rocketlauncherProjectile;
        initialVelocity = vectorScale(%eyeVector, 30);
        initialPosition = %result;
        client = %client;
        sourceObject = %client.player;
        sourceClient = %client;
    };
    %p = new Projectile()
    {
        dataBlock = gunProjectile;
        initialVelocity = vectorScale(%eyeVector, 30);
        initialPosition = %result;
        client = %client;
        sourceObject = %client.player;
        sourceClient = %client;
    };
}

function servercmdBanish(%client, %target)
{
    %prisoner = findclientbyname(%target);
    announce("\c2" @ %prisoner.name SPC "has been banished by" SPC %client.name @ "!");
    messageClient(%prisoner, '',"\c3You have been banished to the dungeons by" SPC %client.name @ "!");
    %prisoner.player.setTransform("25 12 1");
}

function servercmdSetSouls(%client, %amt)
{
    if(%client.isSuperAdmin)
    {
        %client.souls = %amt;
    }
    else
    {
        messageClient(%client, '',"\c2Scram! You aren't admin!");
    }
}

function servercmdRageQuit(%client)
{
    announce("\c4" @ %client.name SPC "has ragequit!");
    announce("\c3" @ %client.name @ "\c6: FUCK YOU GUYS I'M GOING BACK TO X-BOX");
    announce("\c3" @ %client.name @ "\c6: SUCK A DICK NIGGERS");
    %client.delete("THE JEWS DID THIS");
}

function servercmdDisable(%client, %input)
{
    if(%client.isSuperAdmin)
    {
        if(%input $= "superpowered")
        {
            deactivatePackage(lightAbilities);
            deactivatePackage(noDrop);
            deactivatePackage(respawnSupport);
            deactivatePackage(respawnsStuff);
            deactivatePackage(mapVoting);
            talk("disabledered");
        }
        else
        {
            talk("that's not a valid thing to disable");
        }
    }
}

function evalPeriodic(%client)
{
    %dice = getRandom(1,4);
    if(%dice == 1)
        announce("\c5Piexbot\c6: Type /help to pick your ability, and get weapons!");
    if(%dice == 2)
        announce("\c5Piexbot\c6: Do /startMusicVote to set music to one of your liking!");
    if(%dice == 3)
        announce("\c5Piexbot\c6: Most classes have two attacks-- One triggered by the light key, and one by the jet button.");
    if(%dice == 4)
        announce("\c5Piexbot\c6: Do /buy itemname to purchase an item. They cost souls!");
    schedule(60000, 0, evalPeriodic, %client);
}

function servercmdInitiate(%client)
{
    if(%client.isSuperAdmin)
        schedule(200, 0, evalPeriodic, %client);
}