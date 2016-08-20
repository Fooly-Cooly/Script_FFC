function serverCmdDryHump(%cl)
{
	%cl.player.playthread(0, crouchBack);
}

function serverCmdPlayDead(%cl)
{
	%obj = %cl.player;
	if(FFCheck("PlayDead", %obj)) %obj.playthread(0, death1);
}