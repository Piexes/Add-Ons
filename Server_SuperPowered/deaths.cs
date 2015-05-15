//*sigh*. Here we go...
package RespawnsStuff
{
    function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc)
    {
        Parent::onDeath(%client, %killerPlayer, %killer, %damageType, %damageLoc);
        if(%killer != %client)
        {
            %killer.souls += 1;
            %killer.bottomprint("\c2Ability:\c6" SPC %killer.power SPC "\c2Souls:\c6" SPC %killer.souls);
        }
    }
};
activatepackage(RespawnsStuff);