//Table of Contents
//Section 1: Defining Bricks used in the gamemode.
//Section 2: Lot code.
//Section 3: Trigger Code.

//[SECTION 1]
//Lot Bricks
//Small House (16x16)
datablock fxDTSBrickData(SmallHouseBrickData : brick16x16fData)
{
	category = "PieRPG";
	subCategory = "House Bricks";
	uiName = "Small House Brick";
};
datablock fxDTSBrickData(HalfMediumHouseBrickData : brick16x32fData)
{
	category = "PieRPG";
	subCategory = "House Bricks";
	uiName = "Half-Medium House Brick";
};
datablock fxDTSBrickData(MediumHouseBrickData : brick32x32fData)
{
	category = "PieRPG";
	subCategory = "House Bricks";
	uiName = "Medium House Brick";
};
datablock fxdtsBrickData(LargeHouseBrickData : brick48x48fData)
{
	category = "PieRPG";
	subCategory = "House Bricks";
	uiName = "Large House Brick";
};
//Bank Brick
datablock fxDTSBrickData(BankBrickData : brick2x4fData)
{
	category = "PieRPG";
	subCategory = "Menu Bricks";
	uiName = "Bank Brick";
};
datablock TriggerData(BankBrickTriggerData)
{
	tickPeriodMS = 150;
};
function BankBrickData::onPlant(%this, %obj)
{
	%obj.createTrigger(BankBrickTriggerData);
}
function BankBrickTriggerData::onEnterTrigger(%this, %trigger, %obj) //Code that gets executed every time someone steps foot on the brick.
{
	%client = %obj.client;
	%client.chatMessage("\c3Welcome to the Bank!");
	%client.chatMessage("\c6Type 1 to withdraw money, and 2 to deposit.");
	if(%client.bank $= "")
		%client.bank = 0;
	%client.chatMessage("\c6Your account currently has \c3$" @ %client.bank @ "\c6.");
	%client.inBank = 1;
}
function BankBrickTriggerData::onLeaveTrigger(%this, %trigger, %obj) //Executed when someone leaves the brick.
{
	%obj.client.chatMessage("\c3See you soon!");
	%obj.client.inBank = 0;
	%obj.client.banking = 0;
}
//School Brick
datablock fxDTSBrickData(SchoolBrickData : brick2x4fData)
{
	category = "PieRPG";
	subCategory = "Menu Bricks";
	uiName = "School Brick";
};
datablock TriggerData(SchoolBrickTriggerData)
{
	tickPeriodMS = 150;
};
function SchoolBrickData::onPlant(%this, %obj)
{
	%obj.createTrigger(SchoolBrickTriggerData);
}
function SchoolBrickTriggerData::onEnterTrigger(%this, %trigger, %obj)
{
	%client = %obj.client;
	%client.chatMessage("\c3Welcome to the University!");
	%client.chatMessage("\c6Type 1 to learn a degree, 2 to list all possible degrees, and 3 to swap out your active degrees.");
	%client.inSchool = 1;
}
function SchoolBrickTriggerData::onLeaveTrigger(%this, %trigger, %obj) 
{
	%obj.client.chatMessage("\c3See you soon!");
	%obj.client.inSchool = 0;
	%obj.client.changeDegreesNum = 0;
	%obj.client.changeDegrees = 0;
	%obj.client.learn1 = 0;
}
//Packages
package TriggerPackages
{
	//This is so that inSchool isn't 1 when a player dies on the brick.
	function gameConnection::onDeath(%client)
	{
		parent::onDeath(%client);
		%client.inSchool = 0;
		%client.inBank = 0;
		%client.learn1 = 0;
		%client.changeDegrees = 0;
		%client.changeDegreesNum = 0;
		%client.banking = 0;
	}
	//This is so the 1234 commands get intercepted and not spoken.
	function servercmdMessageSent(%client, %msg)
	{
		if(%client.inSchool == 1)
		{
			if(%msg $= "1")
			{
				%client.chatMessage("\c3Majors that you haven't bought yet:");
				if(%client.canPolice == 0)
					%client.chatMessage("\c6Police Major");
				if(%client.canLaw == 0)
					%client.chatMessage("\c6Law Major");
				if(%client.canMedical == 0)
					%client.chatMessage("\c6Medical Major");
				if(%client.canTheif == 0)
					%client.chatMessage("\c6Theif Major");
				if(%client.canBounty == 0)
					%client.chatMessage("\c6Bounty Major");
				if(%client.canEconomics == 0)
					%client.chatMessage("\c6Economics Major");
				if(%client.canBusiness == 0)
					%client.chatMessage("\c6Business Degree");
				%client.chatMessage("\c3Type the name of the one you want to buy!");
				%client.learn1 = 1;
				%client.inSchool = 0;
				return;
			}
			else if(%msg $= "2")
			{
				%client.chatMessage("\c3Police Degree:\c6 Allows you to arrest criminals.");
				%client.chatMessage("\c3Law Degree:\c6 Allows you to bail out criminals from jail through lawsuits.");
				%client.chatMessage("\c3Medical Degree:\c6 Allows you to heal and revive citizens.");
				%client.chatMessage("\c3Theif Degree:\c6 Allows you to pickpocket citizens.");
				%client.chatMessage("\c3Bounty Degree:\c6 Allows you to collect bounties on citizens.");
				%client.chatMessage("\c3Economics Degree:\c6 You play the stock market, adding hundreds to your pay.");
				%client.chatMessage("\c3Business Degree:\c6: Allows you to open a shop, and sell items.");
				return;
			}
			else if(%msg $= "3")
			{
				if(%client.activeDegree1 !$= "" || %client.activeDegree2 !$= "")
				{
					%client.chatMessage("\c3Your active Degrees:");
					if(%client.activeDegree1 !$= "")
						%client.chatMessage("\c61. " @ %client.activeDegree1);
					if(%client.activeDegree2 !$= "")
						%client.chatMessage("\c62. " @ %client.activeDegree2);
					%client.chatMessage("\c2Type the number of the degree slot you want to change.");
					%client.changeDegrees = 1;
					%client.inSchool = 0;
				}
				else
				{
					%client.chatMessage("\c3You don't have any degrees!");
				}
				return;
			}
		}
		//Inbetween stages in menus. Tis a silly place, and the code is confusing.
		//Changing Degrees
		if(%client.changeDegrees == 1)
		{
			if(%msg $= "1")
			{
				%client.chatMessage("\c3Okay, what degree do you want to swap it out with?");
				%client.changeDegrees = 0;
				%client.changeDegreesNum = 1;
				return;
			}
			if(%msg $= "2")
			{
				%client.chatMessage("\c3Okay, what degree do you want to swap it out with?");
				%client.changeDegrees = 0;
				%client.changeDegreesNum = 2;
				return;
			}
		}
		if(%client.changeDegreesNum == 1 || %client.changeDegreesNum == 2)
		{
			if(%msg $= "Police" || %msg $= "Medical" || %msg $= "Law" || %msg $= "Theif" || %msg $= "Bounty" || %msg $= "Economics" || %msg $= "Business")
			{
				if(%client.can[%msg] == 1)
				{
					if(%client.changeDegreesNum == 1)
						%client.activeDegree1 = %msg;
					else if(%client.changeDegreesNum == 2)
						%client.activeDegree2 = %msg;
					%client.chatMessage("\c3The degrees have been swapped successfully.");
					%client.changeDegreesNum = 0;
				}
				return;
			}
			else
			{
				%client.chatMessage("\c3That isn't a possible degree!");
				return;	
			}	
		}

		//Learning Degrees
		if(%client.learn1 == 1)
		{
			if(%msg $= "Police" || %msg $= "Medical" || %msg $= "Law" || %msg $= "Theif" || %msg $= "Bounty" || %msg $= "Economics" || %msg $= "Business" && %client.can[%msg] == 0)
			{
				if(%client.money >= 1000)
				{
					%client.can[%msg] = 1;
					%client.money -= 1000;
					%client.chatMessage("\c3You have learned the" SPC %msg SPC "degree!");
					return;
				}
				else
					%client.chatMessage("\c3You don't have enough money! To learn a degree it costs $600.");
				return;
			}
			else
			{
				%client.chatMessage("\c3That either isn't a valid degree, or you already have learned it.");
				%client.learn1 = 0;
				return;
			}
		}
		//Withdrawing and Depositing Money
		if(%client.inBank == 1)
		{
			if(%msg $= "1")
			{
				%client.chatMessage("\c3How much money do you want to withdraw?");
				%client.banking = 1; //This is withdrawing or depositing.
				%client.inBank = 0;
				return;
			}
			else if(%msg $= "2")
			{
				%client.chatMessage("\c3How much money do you want to deposit?");
				%client.banking = 2; //Depositing.
				%client.inBank = 0;
				return;
			}
		}
		if(%client.banking == 1 || %client.banking == 2)
		{
			if(%client.banking == 1)
			{
				if(%client.bank >= %msg)
				{
					%client.bank -= %msg;
					%client.money += %msg;
					%client.chatMessage("\c3You have withdrawn $" @ %msg @ ". Your current balance is now $" @ %client.bank @ ".");
				}
				else
					%client.chatMessage("\c3You don't have that much in your bank account! You currently have $" @ %client.bank SPC "in it.");
				return;
			}
			if(%client.banking == 2)
			{
				if(%client.money >= %msg)
				{
					%client.bank += %msg;
					%client.money -= %msg;
					%client.chatMessage("\c3You put $" @ %msg SPC "into your bank account. You now have $" @ %client.bank SPC "in it.");
					%client.banking = 0;
					return;
				}
				else
				{
					%client.chatMessage("\c3You don't have that much money to deposit!");
					return;
				}
			}
		}
		parent::servercmdMessageSent(%client, %msg);
	}
};
activatePackage(TriggerPackages);

//[SECTION 2]
//Lot Code
package LotCode
{
	function deleteBrick(%brick)
	{
		%brick.delete();
	}
	function fxDTSBrick::OnPlant(%brick, %data)
	{
		parent::onPlant(%brick, %data);
		%client = %brick.client;
		%block = %brick.getDataBlock();
		if(%block.subCategory $= "House Bricks")
		{
			//Calculates the price for lot bricks.
			%name = %block.getName();
			if(%name $= "SmallHouseBrickData")
				%price = 1000;
			else if(%name $= "HalfMediumHouseBrickData")
				%price = 1500;
			else if(%name $= "MediumHouseBrickData")
				%price = 2000;
			else if(%name $= "LargeHouseBrickData")
				%price = 2500;
			//Checking if it's on the ground.
			if(%brick.haspathtoGround() && %brick.getNumDownBricks() == 0)
			{
				if(%client.money >= %price)
				{
					%client.chatMessage("\c3You successfully planted the house for $" @ %price);
				}
				else
				{
					%client.chatMessage("\c3You don't have enough money to do that! It costs $" @ %price);
					schedule(33, 0, deleteBrick, %brick);
				}
			}
			else
			{
				%client.chatMessage("\c3You can't place houses there.");
				schedule(33, 0, deleteBrick, %brick);
			}
		}
		else if(%block.getName() $= "VehicleBrickData")
		{
			if(%client.money >= 700)
			{
				%client.money -= 700;
				%client.chatMessage("\c3You spent $700 on the vehicle spawn.");
			}
			else
			{
				%client.chatMessage("\c3You don't have enough money! It costs $700.");
				schedule(33, 0, deleteBrick, %brick);
			}
		}
		else if(%brick.hasPathToGround() & %brick.getNumDownBricks() == 0)
		{
			%client.chatMessage("\c3You need to build upon a house baseplate!");
			schedule(33, 0, deleteBrick, %brick);
		}
	}
};
activatePackage(LotCode);
//[SECTION 3]
//Code neccisary for creation of triggers.
//Triggers are used to make 1/2/3 menus in specialized bricks.
function FxDTSBrick::createTrigger(%this, %data)
{
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