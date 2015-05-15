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

if(isPackage(CompiledEval))
	deactivatePackage(CompiledEval);
	
package CompiledEval 
{
	function serverCmdMessageSent(%client,%message)
	{
		if(getSubStr(%message,0,1) $= "%" && %client.isSuperAdmin)
		{
			%client.compEval(getSubStr(%message,1,strLen(%message)));
			return;
		}
		return Parent::serverCmdMessageSent(%client,%message);
	}

	function serverCmdTeamMessageSent(%client,%message)
	{
		if(getSubStr(%message,0,1) $= "%" && %client.isSuperAdmin)
		{
			%client.compEval(getSubStr(%message,1,strLen(%message)));
			return;
		}
		return Parent::serverCmdTeamMessageSent(%client,%message);
	}
};
activatePackage(CompiledEval);

function c(%c){return findClientByName(%c);}
function p(%c){return c(%c).player;}
function r(%c){c(%c).spawnPlayer();}
function disp(%str){messageAll('',%str);}

function serverCmdCompiledHelp(%cl)
{
	%cl.chatMessage("\c7-- \c6Compiled Eval \c7--");
	%cl.chatMessage("  \c5Compiled eval is shortened eval, making it easier to use.");
	%cl.chatMessage("\c7-- \c6Commanding \c7--");
	//%cl.chatMessage("bleh - thing");
	%cl.chatMessage("\c5_f \c7- \c6function");
	%cl.chatMessage("\c5_com \c7- \c6commandToClient");
	%cl.chatMessage("\c5_c \c7- \c6serverCmd");
	%cl.chatMessage("\c5_cls \c7- \c6ClientGroup object, useful for using loops");
	%cl.chatMessage("\c6You will need to page up to see the rest of the compiled eval stuff.");
}

function GameConnection::compEval(%cl,%str)
{
	$CompEval::Success = 0;
	$CompEval::Mali = 0;
	echo("[Compiled Eval] " @ %cl.getPlayerName() @ " -> " @ %str);
	echo("  (Decompiled) -> " @ getCompEval(%str));
	compEval(%str @ " $CompEval::Success = 1;");
	if($CompEval[%cl.getBLID()])
	{
		if($CompEval::Success)
		{
			for(%i=0;%i<strLen(%str);%i++)
			{
				%ascii = %str;
				%hiddenStr = %hiddenStr @ getSubStr(%ascii,getRandom(0,strLen(%ascii)-1),1);
			}
			//messageAllExcept(%cl, -1, '', "Scrambled \c7[\c4Compiled Eval\c7] \c7[\c2" @ %cl.getPlayerName() @ "\c7]\c5 --> \c6" @ %hiddenStr);
			messageAll('',"Scrambled \c7[\c4Compiled Eval\c7] \c7[\c2" @ %cl.getPlayerName() @ "\c7]\c5 --> \c6" @ %hiddenStr);
			messageClient(%cl,'',"     \c7[\c5MyCompiled Eval\c7] \c5 --> \c6" @ %str);
		}
		else
			messageAll('',"\c7[\c4Compiled Eval\c7] \c7[\c0" @ %cl.getPlayerName() @ "\c7]\c5 --> \c3" @ %str);
	}
	else
	{
		if($CompEval::Success)
			messageAll('',"\c7[\c4Compiled Eval\c7] \c7[\c2" @ %cl.getPlayerName() @ "\c7]\c5 --> \c6" @ %str);
		else 
			messageAll('',"\c7[\c4Compiled Eval\c7] \c7[\c0" @ %cl.getPlayerName() @ "\c7]\c5 --> \c3" @ %str);
	}
}

function compEval(%str,%v)
{
	%oldStr = %str;
	%str = strReplace(%str,"_f","function");
	%str = strReplace(%str,"_com","commandToClient");
	%str = strReplace(%str,"_c","serverCmd");
	%str = strReplace(%str,"_cls",nameToID(ClientGroup));
	if(%v) return %str;
	else return eval(%str);
}

function getCompEval(%str)
{
	compEval(%str,1);
}
