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