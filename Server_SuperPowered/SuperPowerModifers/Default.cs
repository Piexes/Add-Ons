function Player::SuperPower_OnSelected_Default(%this)
{
	if(isObject(%client = %this.client))
		%client.chatMessage("Class selected!");
}