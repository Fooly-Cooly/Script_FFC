function FFCheck(%cmd,%obj,%vic)
{
	%cl = %obj.client;
	%cmg = %cl.minigame;
	%check = isObject(%obj) + (isObject(%vic.player)*2);
	switch(%check)
	{
		case 1:	if(%cmg > 0)
				{
					%cmd = eval("%cmg."@%cmd@";");
					if(!%cmd)
					{
						CenterPrint(%cl,"That command is currently disabled.",1,2);
						return 0;
					}
				}
				if(%obj.isResizing)
				{
					CenterPrint(%cl,"Currently Resizing...",1,2);
					return 0;
				}
		case 3:	if((%cmg > 0) != (%vic.minigame > 0))
				{
					CenterPrint(%cl,"You are not in the same minigame.",1,2);
					return 0;
				}
				if(getTrustLevel(%obj,%vic.player)<1)
				{
					CenterPrint(%cl,%vic.name SPC "does not trust you enough to do that.",1,2);
					return 0;
				}
		default:return 0;
	}
	return 1;
}

function serverCmdFFCSetMiniGameData(%cl,%info0,%info1,%info2)
{
	%mg					= %cl.minigame;
	%mg.cloak			= getWord(%info0,0);
	%mg.floatingHead	= getWord(%info0,1);
	%mg.horse			= getWord(%info0,2);
	%mg.monkey			= getWord(%info0,3);
	%mg.playDead		= getWord(%info1,0);
	%mg.resizePlayer	= getWord(%info1,1);
	%mg.resizeVehicle	= getWord(%info1,2);
	%mg.skeleton		= getWord(%info1,3);
	%mg.smurf			= getWord(%info2,0);
	%mg.dog				= getWord(%info2,1);
	%mg.mount			= getWord(%info2,2);
}

function serverCmdFFCHelp(%cl,%page)
{

	switch(%page)
	{
		case 2: %title = "Fun Cmds Page 2";
				%page  = "/MonkeyMe (Turns you into a monkey)\n"@
						 "/HorseMe (Turns you into a horse)\n"@
						 "/LegoMe (Turns you into a minifig)\n"@
						 "/SmurfMe (Turns you into a smurf)\n"@
						 "/PedoMe (Changes your head into a bear)\n"@
						 "/BeakMe (Gives you a beak)";
		case 3: %title = "Fun Cmds Page 3";
				%page  = "/DeBodyMe (Makes you a floating head)\n"@
						 "/ShellMe (Gives you a shell hat)";
		default:%title = "Fun Cmds Page 1";
				%page  = "/Mount partname bodypart (Mounts to player)\n"@
						 "/NormalMe (Turns you back to normal)\n"@
						 "/ResizeMe X Y Z (XYZ are your diminsions)\n"@
						 "/SkeleMe (Turns you into a skeleton)\n"@
						 "/DogMe (Turns you into a dog)\n"@
						 "/GhostMe (Turns you into a ghost)";
	}
	commandToClient(%cl, 'MessageBoxOK', %title, %page);
}

package FFC_Server
{
	function GameConnection::spawnPlayer(%cl)
	{
		Parent::spawnPlayer(%cl);
		if(%cl.ffcForm) serverCmdTransformMe(%cl, %cl.ffcForm);
		if(%cl.ffcHead) serverCmdHead(%cl, %cl.ffcHead);
	}

	function serverCmdMessageBoxNo(%cl)
	{
		parent::serverCmdMessageBoxNo(%cl);
		%pl = %cl.player;
		if(!%pl.mountObj)
			return;
		commandToClient(%pl.mountObj.client, 'MessageBoxOk', "Permission to mount?","Rejected.");
		%pl.mountObj = 0;
		%pl.mountPos = 0;
	}

	function serverCmdUpdateBodyColors(%cl,%face,%hat,%accent,%pack,%pack2,%chest,%hip,%lf,%rf,%la,%ra,%lh,%rh,%cdecal,%fdecal)
	{
		Parent::serverCmdUpdateBodyColors(%cl,%face,%hat,%accent,%pack,%pack2,%chest,%hip,%lf,%rf,%la,%ra,%lh,%rh,%cdecal,%fdecal);
		%cl.headColor2			= %face;
		%cl.hatColor2			= %hat;
		%cl.accentColor2		= %accent;
		%cl.packColor2			= %pack;
		%cl.secondPackColor2	= %pack2;
		%cl.chestColor2			= %chest;
		%cl.hipColor2			= %hip;
		%cl.lLegColor2			= %lf;
		%cl.rLegColor2			= %rf;
		%cl.lArmColor2			= %la;
		%cl.rArmColor2			= %ra;
		%cl.lHandColor2			= %lh;
		%cl.rHandColor2			= %rh;
	}
	
	function serverCmdUpdateBodyParts(%cl,%face,%hat,%accent,%pack,%pack2,%chest,%hip,%lf,%rf,%la,%ra,%lh,%rh,%cdecal,%fdecal)
	{
		Parent::serverCmdUpdateBodyParts(%cl,%face,%hat,%accent,%pack,%pack2,%chest,%hip,%lf,%rf,%la,%ra,%lh,%rh,%cdecal,%fdecal);
	}
	
	function Player::ActivateStuff(%pl)
	{
		if(%pl.activatelevel == 5)
		{
			%eye = %pl.getEyePoint();
			%vec = %pl.getEyeVector();
			%end = vectorAdd(%eye, vectorScale(%vec, 2));
			%targ = ContainerRayCast(%eye, %end, $TypeMasks::PlayerObjectType, %pl);
			if(isObject(%targ) && %targ.getScale() $= "0.2 0.2 0.2")
				%pl.mountObject(%targ, 1);
		}
		parent::ActivateStuff(%pl);
	}
};
ActivatePackage(FFC_Server);