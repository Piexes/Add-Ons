//For mods that use anything such as: %this.getDatablock().maxDamage, %this.getDamageLevel() - This is what they need to be replaced with in case they use this mod.

function Player::getHealth(%this)
{
	if(!isObject(%this))
		return -1;
	if(%this.maxHealth > 0)
		return %this.health;
	return %this.getDatablock().maxDamage - %this.getDamageLevel();
}

function Player::getMaxHealth(%this)
{
	if(!isObject(%this))
		return -1;
	if(%this.maxHealth > 0)
		return %this.maxHealth;
	return %this.getDatablock().maxDamage;
}