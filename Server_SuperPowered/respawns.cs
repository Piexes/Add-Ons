package RespawnSupport
{
	//For stuff to happen on the creation of a new player.
	function gameConnection::spawnPlayer(%this)
    {
        //Lets the original code run.
        parent::spawnPlayer(%this);
        //HELP!
        if(%this.power $= "")
        {
            messageClient(%this,'',"\c6Use \c5/help\c6 to figure out why you have\c5 no weapons\c6!");
            messageClient(%this,'',"\c1Use help to figure out why you have no weapons!");
            messageClient(%this,'',"\c2Use help to figure out why you have no weapons!");
            messageClient(%this,'',"\c3Use help to figure out why you have no weapons!");
            messageClient(%this,'',"\c4Use help to figure out why you have no weapons!");
            messageClient(%this,'',"\c6Use \c5/help\c6 to figure out why you have\c5 no weapons\c6!");
        }
        //Souls. Enough said.
        if(%this.souls $= "")
        {
            %this.souls = "0";
        }
        //This is for the shapeshifters out there.
        %this.human = "true";
        //For the jumpers!
        %this.jumpCount = "";
        %this.cooldown = 0;
        %this.altCooldown = 0;
        //For the ragers!
        %this.canRocket = "false";
        //Sets up all of the abilities on spawn.
        if(%this.power $= "Speed")
        {
            %this.player.setmaxForwardSpeed(20);
            %this.player.addNewItem("MKI");
            %this.player.addNewItem("Chainsaw");
        }
        else if(%this.power $= "Invisibility")
        {
            %this.player.setmaxForwardSpeed(13);
            %this.player.addNewItem("MKI");
            %this.player.addNewItem("Sword");
            %this.player.addNewITem("Cutlass");
        }
        else if(%this.power $= "Flight"||%this.power $= "Breath"||%this.power $= "Possession")
        {
            %this.player.addNewItem("Knife-Pole");
            %this.player.addNewItem("MKI");
        }
        else if(%this.power $= "Shapeshifting")
        {
            %this.player.addNewItem("Fireball");
            %this.player.addNewItem("D - Firebreath");
            %this.player.addNewItem("D - Firebarrage");
        }
        else if(%this.power $= "Teleportation")
        {
            %this.player.addNewItem("Sniper Rifle");
            %this.player.addNewITem("Chef's Knife");
        }
        else if(%this.power $= "Combustion")
        {
            %this.player.addNewItem("Blunderbuss");
        }
        else if(%this.power $= "Conjuration")
        {
            %this.player.addNewItem("Pistol");
            %this.player.addNewItem("Hatchet");
            %this.player.setmaxForwardSpeed(11);
        }
        else if(%this.power $= "Chance")
        {
            %this.player.addNewItem("Hatchet");
            %this.player.addNewItem("MKI");
        }
        //Sets the HUD. Shhh.
        %this.bottomprint("\c2Ability:\c6" SPC %this.power SPC "\c2Souls:\c6" SPC %this.souls);
    }
};
activatepackage(RespawnSupport);

function Player::addNewItem(%player,%item) //Thanks Visolator...
{
    %client = %player.client;
    if(isObject(%item))
    {
        if(%item.getClassName() !$= "ItemData") return -1;
         %item = %item.getName();
    }
    else
        %item = findItemByName(%item);
    if(!isObject(%item)) return;
        %item = nameToID(%item);
            for(%i = 0; %i < %player.getDatablock().maxTools; %i++)
            {
                %tool = %player.tool[%i];
                if(!isObject(%tool))
                {
                    %player.tool[%i] = %item;
                    %player.weaponCount++;
                    messageClient(%client,'MsgItemPickup','',%i,%item);
                    break;
                }
            }
    }

    //Hats go to visolator, additem script.
    function findItemByName(%item)
    {
        for(%i=0;%i<DatablockGroup.getCount();%i++)
        {
            %obj = DatablockGroup.getObject(%i);
            if(%obj.getClassName() $= "ItemData")
                if(strPos(%obj.uiName,%item) >= 0)
                    return %obj.getName();
        }
        return -1;
    }