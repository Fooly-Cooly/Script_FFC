$FFC::Player["monkey"] = "MonkeyArmor";

datablock TSShapeConstructor(monkeyDts)
{
	baseShape  = "./monkey.dts";
	sequence0  = "./h_root.dsq root";
	sequence1  = "./h_run.dsq run";
	sequence2  = "./h_run.dsq walk";
	sequence3  = "./h_back.dsq back";
	sequence4  = "./h_side.dsq side";
	sequence5  = "./h_root.dsq crouch";
	sequence6  = "./h_run.dsq crouchRun";
	sequence7  = "./h_back.dsq crouchBack";
	sequence8  = "./h_side.dsq crouchSide";
	sequence9  = "./h_look.dsq look";
	sequence10 = "./h_root.dsq headside";
	sequence11 = "./h_root.dsq headUp";
	sequence12 = "./h_jump.dsq jump";
	sequence13 = "./h_jump.dsq standjump";
	sequence14 = "./h_root.dsq fall";
	sequence15 = "./h_root.dsq land";
	sequence16 = "./h_armAttack.dsq armAttack";
	sequence17 = "./h_root.dsq armReadyLeft";
	sequence18 = "./h_root.dsq armReadyRight";
	sequence19 = "./h_root.dsq armReadyBoth";
	sequence20 = "./h_spearReady.dsq spearready";  
	sequence21 = "./h_root.dsq spearThrow";
	sequence22 = "./h_root.dsq talk";  
	sequence23 = "./h_death1.dsq death1"; 
	sequence24 = "./h_shiftUp.dsq shiftUp";
	sequence25 = "./h_shiftDown.dsq shiftDown";
	sequence26 = "./h_shiftAway.dsq shiftAway";
	sequence27 = "./h_shiftTo.dsq shiftTo";
	sequence28 = "./h_shiftLeft.dsq shiftLeft";
	sequence29 = "./h_shiftRight.dsq shiftRight";
	sequence30 = "./h_rotCW.dsq rotCW";
	sequence31 = "./h_rotCCW.dsq rotCCW";
	sequence32 = "./h_root.dsq undo";
	sequence33 = "./h_plant.dsq plant";
	sequence34 = "./h_root.dsq sit";
	sequence35 = "./h_root.dsq wrench";
	sequence36 = "./h_root.dsq activate";
	sequence37 = "./h_root.dsq activate2";
	sequence38 = "./h_root.dsq leftrecoil";
};    

datablock PlayerData(MonkeyArmor : HorseArmor)
{
	shapeFile = "./monkey.dts";
	uiName = "Monkey";
	paintable = true;
	mountThread[0] = "sit";
	mountThread[1] = "armReadyBoth";
	mountNode[0] = 2;
};

function MonkeyArmor::onNewDatablock(%this,%obj)
{
	parent::onNewDatablock(%this,%obj);
	%obj.setNodeColor("ALL",getColorIDTable(%obj.client.currentColor));
	%obj.setScale("0.5 0.5 0.5");
}