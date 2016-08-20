function serverCmdNormalMe(%cl)
{
	%pl = %cl.player;
	if(!isObject(%pl))
		return;
	%mg = %cl.minigame;
	%db = %mg > 0 ? %mg.playerdatablock.getName() : "PlayerStandardArmor";
	%sc = %mg > 0 ? %mg.playerdatablock.scale : "1 1 1";
	%pl.setScale(%sc);
	%pl.setDataBlock(%db);
	%pl.unMountAllImages();
	%pl.unHideNode("headSkin");
	%pl.setFaceName(%cl.faceName);
	%pl.playThread(0,root);
	serverCmdUpdateBodyParts(%cl,%cl.hat,%cl.accent,%cl.pack,%cl.secondpack,%cl.chest,%cl.hip,%cl.lLeg,%cl.rLeg,%cl.lArm,%cl.rArm,%cl.lHand,cl.rHand);
	serverCmdUpdateBodyColors(%cl,%cl.headColor2,%cl.hatColor2,%cl.accentColor2,%cl.packColor2,%cl.secondPackColor2,%cl.chestColor2,%cl.hipColor2,%cl.lLegColor2,%cl.rLegColor2,%cl.lArmColor2,%cl.rArmColor2,%cl.lHandColor2,%cl.rHandColor2,%cl.decalName,%cl.faceName);
	%cl.ffcHead = '';
	%cl.ffcForm = '';
}