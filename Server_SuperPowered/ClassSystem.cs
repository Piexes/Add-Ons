//+=========================================================================================================+\\
//|         Made by..                                                                                       |\\
//|        ____   ____  _                __          _                                                      |\\
//|       |_  _| |_  _|(_)              [  |        / |_                                                    |\\
//|         \ \   / /  __   .--.   .--.  | |  ,--. `| |-' .--.   _ .--.                                     |\\
//|          \ \ / /  [  | ( (`\]/ .'`\ \| | `'_\ : | | / .'`\ \[ `/'`\]                                    |\\
//|           \ ' /    | |  `'.'.| \__. || | // | |,| |,| \__. | | |                                        |\\
//|            \_/    [___][\__) )'.__.'[___]\'-;__/\__/ '.__.' [___]                                       |\\
//|                             BL_ID: 20490                                                                |\\
//|             Forum Profile: http://forum.blockland.us/index.php?action=profile;u=40877;                  |\\
//|                                                                                                         |\\
//+=========================================================================================================+\\

//if(!isObject(SuperPowerGroup))
//{
	//announce("Added: SuperPowerGroup");
	//new ScriptGroup(SuperPowerGroup);
//}

$SP::StripChars = "`~!@#^&*-=+{}\\|;:\'\",<>/?[].";

//You can make your own class, use Player::SuperPower_OnSelected_SuperPowerObjectName on a different script file
//A folder called "SuperPowerModifers" is where you can put your super power script modifiers
//functions it can call:

//  Player::SuperPower_OnSelected_SuperPowerObjectName
//  GameConnection::SuperPower_OnSelected_SuperPowerObjectName
//  See file "SuperPowerModifers/Default.cs"

function registerSuperPowerClass(%name, %datablock, %health, %speedFactor, %items)
{
	%strName = stripChars(%name, $SP::StripChars);
	%strName = strReplace(%strName, " ", "_");
	%objName = "SuperPower_" @ %strName;

}

function SuperPower::onAdd(%this)
{
	announce("Added a class: " @ %this.uiName);
	announce("  -> Datablock: " @ %this.class_Datablock);
	announce("  -> Class Health: " @ %this.class_Health);
	announce("  -> SpeedFactor: " @ %this.class_SpeedMultiplier);
	announce("  -> Item count: " @ %this.class_itemCount);
}

//registerSuperPowerClass("Default", "PlayerNoJet", 100, 1, "");

announce("ClassSystem.cs has been loaded successfully.");