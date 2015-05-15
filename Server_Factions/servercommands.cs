function servercmdClass(%client, %class)
{
	if(%class $= "Sprint" && !isObject(%client.player))
		%client.class = "Sprint";
	else if(%class $= "Slowdown" && !isObject(%client.player))
		%client.class = "Slowdown";
	else if(%class $= "Time" && !isObject(%client.player))
		%client.class = "Time";
	else if(%class $= "Forcefeild" && !isObject(%client.player))
		%client.class = "Forcefeild";
	else if(%class $= "Radar" && !isObject(%client.player))
		%client.class = "Radar";
	else
	{
		messageClient(%client,'',"\c6Do \c5/class classname \c6to set your class. You have to be dead to do this.");
		messageClient(%client,'',"\c6The current available classes are sprint, forcefeild, and invisibility.");
	}
	if(%client.class $= "") {}
	else
		messageClient(%client,'',"\c5Your class is set to\c6" SPC %client.class @ ".");
}
//Necromancer: Spawn minions to fuck with your targets.
//Ghost: Create a new body and explore.
//Leap: You jump far distances. One shot, short cooldown.
//Grappler: You get pushed in the direction of your crosshair.
//Fire: Breathes fire.
//Portal: Travel instantly between two checkpoints.
//Invisibility: Lurk in the shadows.
//Forcefeild: In panic, creates its own bubble a-la SU.
//Levitator: Can levitate short distances.
//Revenge: You immediately respawn where you died, and get a chance to attack your enemy. Die after 20sec.
//Time: Activate your ability, set a radius search of nearby players. Anyone in that radius is transferred to that position after 10sec.
//Cannibal: You get massive boosts whenever you kill someone.
//Tonic the Slownog: Radius search, everyone within it gets immobilized/slowed for a few seconds.
//Invincibility: Activating your ability makes you invincible for a few seconds.
//Vampire: Heal the damage you do.
//Cloning: Spawn clone minions, they just hop around. Distract enemies.
//Analyzer: Container radius, gain information about everyone around you.
//Healer: Container radius, everyone inside gets healed to 100. Maybe keep it, or integrate into forcefeild.
//Demolisher: Delete all projectiles in container radius.
//Sheild: Place a big sheild to stop players and attacks.

//Ones  that seem reasonable RIGHT NOW: Leap, Grappler, Fire, Portal, Invisibility, Forcefeild, Time, Cannibal, Tonic, Analyzer, Healer/Sheild