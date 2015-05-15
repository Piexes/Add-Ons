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
schedule(2000,0,exec,"add-ons/Zzerver_NewHealth/Support_HealthDetection.cs"); //Load this last to overwrite the other health detectors.
exec("./Support_HealthSaver.cs");
$NewHealth::Enabled = 1;
$NewHealth::PreVersion = 3;

if($NewHealth::Version >= $NewHealth::PreVersion)
{
	warn("There is already a newer health version.");
	return;
}

$NewHealth::Version = 3;

if(isPackage(CustomHealth))
	deactivatepackage(CustomHealth);

//Credit for Port for the functions
//
package CustomHealth
{
	function Player::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(!strLen(%health))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::setHealth(%this,%health);return;}
		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this,%this.getHackPosition(),%this.getDataBlock().maxDamage*%this.getSize(),$DamageType::Default,"body",true);
			return;
		}
		if(%health > %this.getMaxHealth())
			%health = %this.getMaxHealth();
		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());
		return true;
	}

	function Player::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::AddHealth(%this,%health);return;}
		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this,%this.getHackPosition(),mAbs(%health),$DamageType::Default,"body",false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}
		if(%this.health < 0)
		{
			%this.health = 0;
			%this.damage(%last,%this.getHackPosition(),%this.getMaxHealth()*%this.getSize(),%damageType,"body",true);
		}
	}

	function AIPlayer::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(!strLen(%health))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::setHealth(%this,%health);return;}
		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this,%this.getHackPosition(),%this.getDataBlock().maxDamage*%this.getSize(),$DamageType::Default,"body",true);
			return;
		}
		if(%health > %this.getMaxHealth())
			%health = %this.getMaxHealth();
		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());
		return true;
	}

	function AIPlayer::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::AddHealth(%this,%health);return;}
		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this,%this.getHackPosition(),mAbs(%health),$DamageType::Default,"body",false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}
		if(%this.health < 0)
		{
			%this.health = 0;
			%this.damage(%last,%this.getHackPosition(),%this.getMaxHealth()*%this.getSize(),%damageType,"body",true);
		}
	}

	function Vehicle::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(!strLen(%health))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::setHealth(%this,%health);return;}
		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this,%this.getHackPosition(),%this.getDataBlock().maxDamage*%this.getSize(),$DamageType::Default,"body",true);
			return;
		}
		if(%health > %this.getMaxHealth())
			%health = %this.getMaxHealth();
		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());
		return true;
	}

	function Vehicle::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::AddHealth(%this,%health);return;}
		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this,%this.getHackPosition(),mAbs(%health),$DamageType::Default,"body",false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}
		if(%this.health < 0)
		{
			%this.health = 0;
			%this.damage(%last,%this.getHackPosition(),%this.getMaxHealth()*%this.getSize(),%damageType,"body",true);
		}
	}

	function WheeledVehicle::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(!strLen(%health))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::setHealth(%this,%health);return;}
		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this,%this.getHackPosition(),%this.getDataBlock().maxDamage*%this.getSize(),$DamageType::Default,"body",true);
			return;
		}
		if(%health > %this.getMaxHealth())
			%health = %this.getMaxHealth();
		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());
		return true;
	}

	function WheeledVehicle::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;
		if(%this.maxHealth <= 0)
			{Parent::AddHealth(%this,%health);return;}
		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this,%this.getHackPosition(),mAbs(%health),$DamageType::Default,"body",false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}
		if(%this.health < 0)
		{
			%this.health = 0;
			%this.damage(%last,%this.getHackPosition(),%this.getMaxHealth()*%this.getSize(),%damageType,"body",true);
		}
	}
	
	function ShapeBase::kill(%this,%damageType,%last)
	{
		if(!isObject(%this))
			return false;
		if(getSimTime() - %this.spawnTime < $Game::PlayerInvulnerabilityTime)
			return false;
		
		if(!strLen(%damageType))
			%damageType = $DamageType::Suicide;
		
		if(!isObject(%last))
			%last = %this;
		%this.health = 0;
		%this.damage(%last,%this.getHackPosition(),%this.getMaxHealth()*%this.getSize(),%damageType,"body",true);
	}
	
	function Armor::onNewDatablock(%data,%player)
	{
		Parent::onNewDatablock(%data,%player);
		if(%player.maxHealth > 0)
			%player.setDamageLevel(%player.getHealthLevel());
	}

	function ShapeBase::damage(%this,%sourceObject,%position,%damage,%damageType,%damageLoc,%parent)
	{
		if(%parent)
		{
			Parent::damage(%this,%sourceObject,%position,%damage*%this.getSize(),%damageType,%damageLoc);
			return;
		}
		if(getSimTime() - %this.spawnTime < $Game::PlayerInvulnerabilityTime)
			return;
		%this.lastKiller = %sourceObject.sourceObject;
		if(%this.getMaxHealth() == 0)
			return; //They are invincible
		if(!%this.health)
			return Parent::damage(%this,%sourceObject,%position,%damage,%damageType,%damageLoc,true);
		%old = %this.health;
		%this.health -= %damage;
		%new = %this.health;
		%diff = %old - %new;
		%this.lastDamageType = %damageType;
		if(%diff >= 16) %this.emote(painHighImage,true);
		else if(%diff >= 8 && %diff <= 15) %this.emote(painMidImage,true);
		else %this.emote(painLowImage,true);
		%levelstuff = %this.getHealthLevel();
		if(%levelstuff > %this.getMaxHealth())
			%levelstuff = %this.getMaxHealth();
		if(%this.health > %this.maxHealth)
			%this.health = %this.maxHealth;
		if(%this.health <= 0)
			return Parent::damage(%this,%sourceObject,%position,(%this.getDataBlock().maxDamage)*%this.getSize(),%damageType,%damageLoc,true);
		else
			%this.setDamageLevel(%levelstuff);
		Parent::damage(%this,%sourceObject,%position,0,%damageType,%damageLoc);
	}

	function serverCmdSuicide(%this)
	{
		if(isObject(%pl=%this.player))
			%pl.kill();
	}
};
activatePackage("CustomHealth");

function ShapeBase::getSize(%this)
{
	if(!isObject(%this))
		return -1;
	return getWord(%this.getScale(),2);
}

function ShapeBase::addMaxHealth(%this,%maxHealth,%bool)
{
	if(!isObject(%this))
		return -1;
	if(!strLen(%maxHealth))
		return false;
	%this.maxHealth += %maxHealth;
	if(%this.maxHealth > 999999)
		%this.maxHealth = 999999;
	else if(%this.maxHealth < 0)
		%this.maxHealth = 1;
	if(%bool)
		%this.health = %this.maxHealth;
	else
		%this.health += %maxHealth;
	%levelstuff = %this.getHealthLevel();
	if(%levelstuff > %this.getMaxHealth())
		%levelstuff = %this.getMaxHealth();
	%this.setDamageLevel(%levelstuff);
	return true;
}

function ShapeBase::setMaxHealth(%this,%maxHealth)
{
	if(!isObject(%this))
		return -1;
	if(!strLen(%maxHealth) || %maxHealth <= 0)
		return false;
	%this.maxHealth = %maxHealth;
	if(%this.maxHealth > 999999)
		%this.maxHealth = 999999;
	else if(%this.maxHealth < 0)
		%this.maxHealth = 1;
	%this.health = %this.maxHealth;
	%this.setDamageLevel(0);
	return true;
}

function ShapeBase::setInvulnerbilityTime(%this,%time)
{
	%this.oldMaxHealth = %this.maxHealth;
	%this.oldHealth = %this.health;
	%this.maxHealth = 0;
	%this.health = 0;
	if(%time >= 1)
		%this.vulWasteSch = %this.schedule(%time * 1000,setInvulnerbility,0);
}

function ShapeBase::setInvulnerbility(%this,%val)
{
	if(isEventPending(%this.vulWasteSch))
	{
		cancel(%this.vulWasteSch);
		if(%this.oldMaxHealth > 1)
			%this.maxHealth = %this.oldMaxHealth;
		else
			%this.maxHealth = "";

		if(%this.oldHealth > 1)
			%this.health = %this.oldHealth;
		else
			%this.health = "";
	}

	if(%val)
	{
		%this.oldMaxHealth = %this.maxHealth;
		%this.oldHealth = %this.health;
		%this.maxHealth = 0;
		%this.health = 0;
	}
	else
	{
		if(%this.oldMaxHealth > 1)
			%this.maxHealth = %this.oldMaxHealth;
		else
			%this.maxHealth = "";
		
		if(%this.oldHealth > 1)
			%this.health = %this.oldHealth;
		else
			%this.health = "";
	}
	
	if(%time >= 1)
		%this.vulWasteSch = %this.schedule(%time * 1000,setInvulnerbility,0);
}

function ShapeBase::getHealthLevel(%this) //This is used to set their damage level, which is opposite of their health
{
	return mAbs(%this.getDatablock().maxDamage / %this.getMaxHealth() * %this.getHealth() - %this.getDatablock().maxDamage);
}

registerOutputEvent("player", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("player", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("player", "setInvulnerbilityTime" ,"int 1 300 5");

registerOutputEvent("vehicle", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("vehicle", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("vehicle", "setInvulnerbilityTime" ,"int 1 300 5");

registerOutputEvent("bot", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("bot", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("bot", "setInvulnerbilityTime" ,"int 1 300 5");

schedule(5000,0,MaxHealth_registerVariables);

function MaxHealth_registerVariables()
{
	if(isPackage(VCE_Main))
	{
		registerSpecialVar(Player,"health","%this.getHealth()","setHealth");
		registerSpecialVar(Player,"maxhealth","%this.getMaxHealth()","setMaxHealth");
		registerSpecialVar(Vehicle,"health","%this.getHealth()","setHealth");
		registerSpecialVar(Vehicle,"maxhealth","%this.getMaxHealth()","setMaxHealth");
	}
}