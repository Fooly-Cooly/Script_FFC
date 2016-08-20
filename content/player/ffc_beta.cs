$FFC::Player["beta"] = "BetaArmor";

datablock TSShapeConstructor(BetaDts : mDts)
{
	baseShape  = "base/data/shapes/player/mBeta.dts";
};

datablock PlayerData(BetaArmor : PlayerStandardArmor)
{
	shapeFile = "base/data/shapes/player/mBeta.dts";
	uiName = "Beta Player";
};

package Beta
{
	function GameConnection::applyBodyColors(%this)
	{
		parent::applyBodyColors(%this);
		%obj = %this.player;
		if(isObject(%obj) && %obj.getDatablock().getName() $= "BetaArmor")
		{
			//%obj.AllNodes(%this,"hide");
			//%obj.AllNodes(%this,"unhide");
		}
	}
	
	function GameConnection::applyBodyPart(%this)
	{
		parent::applyBodyPart(%this);
	}
};
ActivatePackage(Beta);