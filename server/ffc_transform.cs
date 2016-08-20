function FFC_AddHead(%name, %type, %image)
{
	if(!isObject(FFC_Head))
		new ScriptObject(FFC_Head){};
	if(FFC_Head.imag[%name])
		return;
	FFC_Head.type[%name] = %type;
	FFC_Head.imag[%name] = %image;
}

function FFC_Float(%cl, %obj)
{
	if(FFCheck("FloatingHead", %obj))
	{
		%obj.allNodes(%cl, hide);
		UnhideHeadNodes(%cl);
	}
}

function FFC_Ghost(%cl, %obj)
{
	serverCmdNormalMe(%cl);
	%Color0 = "1 1 1 0.8";
	%Color1 = "1 1 1 1";	//"1 1 1 0.001";
	%obj.setShapeName("");
	%obj.setNodeColor(KnitHat, %Color0);
	%obj.setNodeColor(HeadSkin, %Color0);
	%obj.setNodeColor(Chest, %Color0);
	%obj.setNodeColor(Pants, %Color1);
	%obj.setNodeColor(LShoe, %Color0);
	%obj.setNodeColor(RShoe, %Color0);
	%obj.setNodeColor(LArm, %Color1);
	%obj.setNodeColor(RArm, %Color1);
	%obj.setNodeColor(LHand, %Color1);
	%obj.setNodeColor(RHand, %Color1);
	for(%i=0;$hip[%i]	!$= "";%i++) %obj.hideNode($hip[%i]);
	for(%i=0;$LArm[%i]	!$= "";%i++) %obj.hideNode($LArm[%i]);
	for(%i=0;$LHand[%i]	!$= "";%i++) %obj.hideNode($LHand[%i]);
	for(%i=0;$RArm[%i]	!$= "";%i++) %obj.hideNode($RArm[%i]);
	for(%i=0;$RHand[%i]	!$= "";%i++) %obj.hideNode($RHand[%i]);
}

function FFC_Smurf(%cl, %obj)
{
	serverCmdNormalMe(%cl);
	%cl.isSmurf = 1;
	%white = "1 1 1 1";
	%lightBlue = "0.106 0.459 0.7969 1";
	%obj.AllNodes(%cl,hide);
	%cl.hatColor = %white;
	%cl.headColor = %lightBlue;
	%cl.chestColor = %lightBlue;
	%cl.hipColor = %white;
	%cl.larmColor = %lightBlue;
	%cl.lhandColor = %lightBlue;
	%cl.llegColor = %white;
	%cl.rarmColor = %lightBlue;
	%cl.rhandColor = %lightBlue;
	%cl.rlegColor = %white;
	%obj.unhideNode(KnitHat);
	%obj.unhideNode(HeadSkin);
	%obj.unhideNode(Chest);
	%obj.unhideNode(Pants);
	%obj.unhideNode(LShoe);
	%obj.unhideNode(RShoe);
	%obj.unhideNode(LArm);
	%obj.unhideNode(RArm);
	%obj.unhideNode(LHand);
	%obj.unhideNode(RHand);
	%obj.setNodeColor(KnitHat,%white);
	%obj.setNodeColor(HeadSkin,%lightBlue);
	%obj.setNodeColor(Chest,%lightBlue);
	%obj.setNodeColor(Pants,%white);
	%obj.setNodeColor(LShoe,%white);
	%obj.setNodeColor(RShoe,%white);
	%obj.setNodeColor(LArm,%lightBlue);
	%obj.setNodeColor(RArm,%lightBlue);
	%obj.setNodeColor(LHand,%lightBlue);
	%obj.setNodeColor(RHand,%lightBlue);
	%obj.setDecalName("AAA-None");
	ffcSetScale(%obj,0.2,0.2,0.2);
}

function serverCmdTransformMe(%cl, %type)
{
	%obj = %cl.player;
	if(ffCheck(%type, %obj) && %type !$= "")
	{
		serverCmdNormalMe(%cl);
		%cl.ffcForm = %type;
		%obj.unMountAllImages();
		switch$(%type)
		{
			case "smurf": FFC_Smurf(%cl, %obj);
			case "ghost": FFC_Ghost(%cl, %obj);
			case "float": FFC_Float(%cl, %obj);
				 default: %obj.setDataBlock($FFC::Player[%type]);
		}
	}
}

function serverCmdHead(%cl, %arg)
{
	if(%arg $= "")
		return;
	%pl = %cl.player;
	switch$(FFC_Head.type[%arg])
	{
		case "Hat":
			%cl.ffcHead = %arg;
			%pl.unMountImage(2);
			%pl.headNodes(%cl, hide, 0, 1, 1);
			%pl.mountImage(FFC_Head.imag[%arg], 2);
		case "Head":
			%cl.ffcHead = %arg;
			%pl.unMountImage(1);
			%pl.hideNode("headSkin");
			%pl.mountImage(FFC_Head.imag[%arg], 1);
		case "Acc":
			%cl.ffcHead = %arg;
			%pl.unMountImage(1);
			%pl.unHideNode("headSkin");
			%pl.mountImage(FFC_Head.imag[%arg], 1);
	}
}