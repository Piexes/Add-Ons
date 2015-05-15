function servercmdnewCompany(%client, %name, %cofounder, %extraMoney)
{
	if(%name $= "")
	{
		%client.chatMessage("\c2The syntax for this command is /newCompany companyName cofounder(optional) extraStartingMoney(optional)");
		return;
	}
	%company = new ScriptObject("c" @ %name);
	%company.creator = %client;
	%company.cofounder = findclientbyname(%cofounder);
	%company.money = 1000 + %extraMoney;
	%company.name = %name;
	%client.company = ("c" @ %name);
}

function servercmdMyC(%client, %a)
{
	if(%a $= "Funds")
		%client.chatMessage("Funds:" SPC %client.company.money);
	else if(%a $= "Founder")
		%client.chatMessage("Founder:" SPC %client.company.creator.name);
	else if(%a $= "Name")
		%client.chatMEssage("Name:" SPC %client.company.name);
}

function servercmdDonate(%client, %amount)
{
	$Economy += %amount;
	if(%economy > 150)
		%economy = 150;
}

function servercmdTalk(%client)
{
	talk("Piss in a Jar:" SPC $Economy);
	%percent = $economy / 100;
	talk("\c2Economy:" SPC %percent);
}












//Copy-Pasta'd code. Consider it a core function.
function FxDTSBrick::createTrigger(%this, %data)
{
	//credits to Space Guy for showing how to create triggers

	%t = new Trigger()
	{
		datablock = %data;
		polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1"; //this determines the shape of the trigger
	};

	missionCleanup.add(%t);
	
	%boxMax = getWords(%this.getWorldBox(), 3, 5);
	%boxMin = getWords(%this.getWorldBox(), 0, 2);
	%boxDiff = vectorSub(%boxMax,%boxMin);
	%boxDiff = vectorAdd(%boxDiff, "0 0 0.2"); 
	%t.setScale(%boxDiff);
	%posA = %this.getWorldBoxCenter();
	%posB = %t.getWorldBoxCenter();
	%posDiff = vectorSub(%posA, %posB);
	%posDiff = vectorAdd(%posDiff, "0 0 0.1");
	%t.setTransform(%posDiff);

	%this.trigger = %t;
	%t.brick = %this;

	return %t;
}

//Triggers! I guess.
//This creates our own test brick
//GENERAL NOTE: make it so %inBank is false when someone suicides or gets killed
datablock fxDTSBrickData(TestBrickData : brick2x4fData)
{
	category = "PieRPG";
	subCategory = "Trigger Bricks";
	uiName = "Bank";
};
datablock TriggerData(TestBrickTriggerData)
{
	tickPeriodMS = 150;
};
//Calls when you make the brick. Starts the trigger.
function TestBrickData::onPlant(%this, %obj)
{
	%obj.createTrigger(TestBrickTriggerData);
}
//Stuff to do when someone enters the brick trigger.
function TestBrickTriggerData::onEnterTrigger(%this, %trigger, %obj)
{
	%client = %obj.client;
	%client.chatMessage("\c3Welcome to the Bank!");
	%client.chatMessage("\c6Type 1 to withdraw money, and 2 to deposit.");
	%client.inBank = 1;
}
//Stuff to do when someone leaves the brick trigger.
function TestBrickTriggerData::onLeaveTrigger(%this, %trigger, %obj)
{
	%obj.client.chatMessage("\c3See you soon!");
	%obj.client.inBank = 0;
	%obj.client.inSchool = 0;
	%obj.client.inPolice = 0;
	%obj.client.inShop = 0;
}
package MessageShit
{
	function servercmdMessageSent(%client, %msg)
	{
		if(%client.inBank == 1)
		{
			if(%msg $= "1")
			{
				talk("tell me the amoutn of sluts uwanna fuck");
				%client.stageone = 1;
				return;
			}
			else if(%msg $= "2")
			{
				talk("wow mister moneybags depositing shit eh");
				return;
			}
		}
		if(%client.stageone == 1)
		{
			talk("so its" SPC %msg SPC "sluts eh?");
			%client.stageone = 0;
			return;
		}
		parent::servercmdMessageSent(%client, %msg);
	}
};
activatePackage(MessageShit);

//You like bricks? You get bricks.
//We dive into the lot shit part.
datablock FxDTSBrickData(SmallHouseData : brick16x16fData)
{
	category = "PieRPG";
	subCategory = "Houses";
	uiName = "Small House";
};

package Lots
{
	function fxDTSBrick::OnPlant(%brick, %data)
	{
		//Parenting
		parent::OnPlant(%brick, %data);
		//Defining our shorthands.
		%client = %brick.client;
		%block = %brick.getDataBlock();
		//Actual code for lots.
		if(%block.getName() $= "SmallHouseData")
		{
			if(%brick.hasPathToGround() && %brick.getNumDownBricks() == 0)
			{
				if(%client.money >= "20")
				{
					talk("Small House has been planted!");
					%client.money -= 20;
				}
				else
				{
					talk("You don't have enough money to plant a small House!");
					schedule(33, 0, deleteBrick, %brick);
				}
			}
			else
			{
				%client.chatMessage("\c2You can't place lots there!");
				schedule(33, 0, deleteBrick, %brick);
			}
		}
		if(%brick.hasPathToGround() && %brick.getNumDownBricks() == 0 && %block.getName() !$= "SmallHouseData")
		{
			schedule(33,0, deleteBrick, %brick);
			talk("you have to use lots!1!!!!");
		}
	}
};
activatePackage(Lots);

function deleteBrick(%brick)
{
	%brick.delete();
}

function See(%client)
{
	%client.bottomPrint("\c2Economy:\c6" SPC $PieRPG::Economy)
}

function servercmdDonate(%client, %moni)
{
	%amount = %moni * 0.1;
	if($PieRPG::Economy < 50)
	{
		%dona = %amount * 1.1
		$PieRPG::Economy += %dona;
	}
	else if($PieRPG::Economy > 50 && $PieRPG::Economy =< 100)
	{
		$PieRPG::Economy += %amount;
	}
	else if($PieRPG::Economy > 100 && $PieRPG::Economy =< 125)
	{
		%dona = %amount * 0.8
		$PieRPG::Economy += %dona;
	}
	else if($PieRPG::)
}