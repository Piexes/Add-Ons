//Script modified by Visolator

//Relative Velocity. Compatibility stuffs.
function Player::addRelativeVelocity(%player,%xyz)
{
	%x = getWord(%xyz,0);
	%y = getWord(%xyz,1);
	%z = getWord(%xyz,2);
	%forwardVector = %player.getForwardVector();
	%forwardX = getWord(%forwardVector,0);
	%forwardY = getWord(%forwardVector,1);
	%player.addVelocity((%x * %forwardY + %y * %forwardX) SPC (%y * %forwardY + %x * -%forwardX) SPC %z);
}

function Player::addNewItem(%player,%item)
{
	%client = %player.client;
	if(isObject(%item))
	{
		if(%item.getClassName() !$= "ItemData") return false;
		%item = %item.getName();
	}
	else
		%item = findItemByName(%item);
	if(!isObject(%item)) return false;
	%item = nameToID(%item);
	for(%i = 0; %i < %player.getDatablock().maxTools; %i++)
	{
		%tool = %player.tool[%i];
		if(%tool == 0)
		{
			%player.tool[%i] = %item;
			%player.weaponCount++;
			messageClient(%client,'MsgItemPickup','',%i,%item);
			return true;
		}
	}
	return false; //We didn't find a slot :(
}

function findItemByName(%item,%val)
{
	if(isObject(%item)) return %item.getName();
	if($lastDatablockCount != DatablockGroup.getCount() || %val) //We don't need to cause lag everytime we try to find an item
	{
		//talk("findItemByName - Resetting cached tables");
		$itemTableLookUp_Count = 0;
		for(%i=0;%i<DatablockGroup.getCount();%i++)
		{
			%obj = DatablockGroup.getObject(%i);
			if(%obj.getClassName() $= "ItemData" && strLen(%obj.uiName) > 0)
			{
				$itemTableLookup[$itemTableLookUp_Count] = %obj;
				$itemTableLookUp_Count++;
			}
		}
		//talk("findItemByName - Cached tables set to " @ $itemTableLookUp_Count);
	}
	for(%a=0;%a<$itemTableLookUp_Count;%a++)
	{
		%objA = $itemTableLookup[%a];
		if(%objA.getClassName() $= "ItemData")
			if(strPos(%objA.uiName,%item) >= 0)
			{
				//talk("Found item, position detection: " @ strPos(%objA.uiName,%item));
				return %objA.getName();
			}
	}
	$lastDatablockCount = DatablockGroup.getCount();
	return -1;
}