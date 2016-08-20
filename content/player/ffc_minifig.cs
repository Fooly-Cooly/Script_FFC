$FFC::Player["minifig"] = "MinifigArmor";

datablock TSShapeConstructor(MiniFigDts)
{
	baseShape  = "base/data/shapes/player/minifig.dts";
	sequence0  = "base/data/shapes/player/minifig_root.dsq root";
	sequence1  = "base/data/shapes/player/minifig_run.dsq run";
	sequence2  = "base/data/shapes/player/minifig_root.dsq walk";
	sequence3  = "base/data/shapes/player/minifig_back.dsq back";
	sequence4  = "base/data/shapes/player/minifig_side.dsq side";
	sequence5  = "base/data/shapes/player/minifig_crouch.dsq crouch";
	sequence6  = "base/data/shapes/player/minifig_crouchRun.dsq crouchRun";
	sequence7  = "base/data/shapes/player/minifig_crouchBack.dsq crouchBack";
	sequence8  = "base/data/shapes/player/minifig_crouchSide.dsq crouchSide";
	sequence9  = "base/data/shapes/player/minifig_look.dsq look";
	sequence10 = "base/data/shapes/player/minifig_headside.dsq headside";
	sequence11 = "base/data/shapes/player/minifig_headup.dsq headUp";
	sequence12 = "base/data/shapes/player/minifig_standjump.dsq jump";
	sequence13 = "base/data/shapes/player/minifig_standjump.dsq standjump";
	sequence14 = "base/data/shapes/player/minifig_fall.dsq fall";
	sequence15 = "base/data/shapes/player/minifig_root.dsq land";
	sequence16 = "base/data/shapes/player/minifig_armAttack.dsq armAttack";
	sequence17 = "base/data/shapes/player/minifig_armReadyLeft.dsq armReadyLeft";
	sequence18 = "base/data/shapes/player/minifig_armReadyRight.dsq armReadyRight";
	sequence19 = "base/data/shapes/player/minifig_armReadyBoth.dsq armReadyBoth";
	sequence20 = "base/data/shapes/player/minifig_spearready.dsq spearready";  
	sequence21 = "base/data/shapes/player/minifig_spearThrow.dsq spearThrow";
	sequence22 = "base/data/shapes/player/minifig_talk.dsq talk";
	sequence23 = "base/data/shapes/player/minifig_root.dsq death1";
	sequence24 = "base/data/shapes/player/minifig_root.dsq shiftUp";
	sequence25 = "base/data/shapes/player/minifig_root.dsq shiftDown";
	sequence26 = "base/data/shapes/player/minifig_root.dsq shiftAway";
	sequence27 = "base/data/shapes/player/minifig_root.dsq shiftTo";
	sequence28 = "base/data/shapes/player/minifig_root.dsq shiftLeft";
	sequence29 = "base/data/shapes/player/minifig_root.dsq shiftRight";
	sequence30 = "base/data/shapes/player/minifig_root.dsq rotCW";
	sequence31 = "base/data/shapes/player/minifig_root.dsq rotCCW";
	sequence32 = "base/data/shapes/player/minifig_headside.dsq undo";
	sequence33 = "base/data/shapes/player/minifig_root.dsq plant";
	sequence34 = "base/data/shapes/player/minifig_sit.dsq sit";
	sequence35 = "base/data/shapes/player/minifig_armAttack.dsq wrench";
	sequence36 = "base/data/shapes/player/minifig_activate.dsq activate";
	sequence37 = "base/data/shapes/player/minifig_root.dsq activate2";
	sequence38 = "base/data/shapes/player/minifig_root.dsq leftrecoil";
};

datablock PlayerData(MinifigArmor : PlayerStandardArmor)
{
	jetEmitter = "";
	jetGroundEmitter = "";
	shapeFile = "base/data/shapes/player/minifig.dts";
	uiName = "Lego Player";
};

package Minifig
{
	function Player::activateStuff(%obj)
	{
		parent::activateStuff(%obj);
		%obj.schedule(600, "playthread", 3, root);
	}

	function GameConnection::applyBodyColors(%this)
	{
		parent::applyBodyColors(%this);
		%obj = %this.player;
		if(isObject(%obj) && %obj.getDatablock().getName() $= "MinifigArmor")
		{
			%obj.setFaceName(%this.faceName);
			%obj.setDecalName(%this.decalName);
			%obj.setNodeColor(hip, %this.hipColor);
			%obj.setNodeColor(lLeg, %this.llegColor);
			%obj.setNodeColor(rLeg, %this.rlegColor);
			%obj.setNodeColor(lArm, %this.larmColor);
			%obj.setNodeColor(rArm, %this.rarmColor);
			%obj.setNodeColor(body, %this.chestColor);
			%obj.setNodeColor(lHand, %this.lhandColor);
			%obj.setNodeColor(rHand, %this.rhandColor);
			%obj.setNodeColor(headSkin, %this.headcolor);
		}
	}
	
	function serverCmdUndoBrick(%cl)
	{
		parent::serverCmdUndoBrick(%cl);
		%cl.player.schedule(450, "playthread", 3, root);
	}
};
ActivatePackage(Minifig);