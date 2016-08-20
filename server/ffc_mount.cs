function serverCmdMount(%cl, %vic, %part)
{
	%pl = %cl.player;
	%vic = findClientByName(%vic);
	if(!FFCheck("Mount", %pl, %vic))
		return;
	switch$(%part)
	{
		case   "Hat": %pos = 5;
		case  "Head": %pos = 2;
		case  "Body": %pos = 7;
		case "LHand": %pos = 1;
		case "Pants": %pos = 8;
		case "LFoot": %pos = 4;
		case "RFoot": %pos = 3;
			 default: centerPrint(%cl, "Mount point not found...", 2);
					  return;
	}
	%obj = %vic.player;
	switch(isObject(%obj.mountObj))
	{
		case 0:	%obj.mountObj = %pl;
				%obj.mountPos = %pos;
				commandToClient(%vic, 'MessageBoxYesNo', "Permission to mount?", %cl.name SPC "wishes to mount to your" SPC %part @".", 'MountYes');
		case 1: centerPrint(%cl, "Player currently has a mount request pending...", 2);
	}
}

function serverCmdMountYes(%cl)
{
	%pl = %cl.player;
	if(!isObject(%pl) || !isObject(%pl.mountObj))
		return;
	%pl.mountObject(%pl.mountObj,%pl.mountPos);
	%pl.mountObj = 0;
	%pl.mountPos = 0;
}