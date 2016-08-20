$FFC::MaxResize	= "2";
$FFC::MinResize	= "0.2";

function FFC_SetScale(%obj, %x0, %y0, %z0)
{
	if(!isObject(%obj))
		return;
	%scale0 = %x0 SPC %y0 SPC %z0;
	%scale1 = %obj.getScale();
	if(vectorDist(%scale0, %scale1))
	{
		%obj.isResizing = 1;
		%x1 = getWord(%scale1, 0);
		%y1 = getWord(%scale1, 1);
		%z1 = getWord(%scale1, 2);
		if(%x1 != %x0) %x1 += %x1 < %x0 ? 0.01 : -0.01;
		if(%y1 != %y0) %y1 += %y1 < %y0 ? 0.01 : -0.01;
		if(%z1 != %z0) %z1 += %z1 < %z0 ? 0.01 : -0.01;
		%obj.setScale(%x1 SPC %y1 SPC %z1);
		schedule(10, 0, "FFC_SetScale", %obj, %x0, %y0, %z0);
	}
	else %obj.isResizing = 0;
}

function serverCmdResizeMe(%cl, %x, %y, %z)
{
	%pl = %cl.player;
	if(!FFCheck("ResizePlayer", %pl))
		return;
	%x = mFloatLength(%x, 2);
	%y = mFloatLength(%y, 2);
	%z = mFloatLength(%z, 2);
	if(%x > $FFC::MaxResize) %x = $FFC::MaxResize;
	if(%y > $FFC::MaxResize) %y = $FFC::MaxResize;
	if(%z > $FFC::MaxResize) %z = $FFC::MaxResize;
	if(%x < $FFC::MinResize) %x = $FFC::MinResize;
	if(%y < $FFC::MinResize) %y = $FFC::MinResize;
	if(%z < $FFC::MinResize) %z = $FFC::MinResize;
	FFC_setScale(%pl, %x, %y, %z);
}

function serverCmdResizeVehicle(%cl, %x, %y, %z)
{
	%obj = %cl.player.getObjectMount();
	if(!FFCheck("ResizeVehicle", %obj))
		return;
	%x = mFloatLength(%x, 2);
	%y = mFloatLength(%y, 2);
	%z = mFloatLength(%z, 2);
	if(%x > $FFC::MaxResize) %x = $FFC::MaxResize;
	if(%y > $FFC::MaxResize) %y = $FFC::MaxResize;
	if(%z > $FFC::MaxResize) %z = $FFC::MaxResize;
	if(%x < $FFC::MinResize) %x = $FFC::MinResize;
	if(%y < $FFC::MinResize) %y = $FFC::MinResize;
	if(%z < $FFC::MinResize) %z = $FFC::MinResize;
	FFC_SetScale(%obj, %x, %y, %z);
}