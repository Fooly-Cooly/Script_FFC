function Player::HeadNodes(%this, %cl, %head, %hat, %acc)
{
	if(!isObject(%this))
		return error("object not found");
	switch(%head)
	{
		case 0:	%this.hideNode("headSkin");
		case 1: %this.unHideNode("headSkin");
				%this.setNodeColor("headSkin",%cl.headColor);
	}
	switch(%hat)
	{
		case 0:	for(%i=0;$Hat[%i]!$="";%i++) %this.hideNode($Hat[%i]);
		case 1:	%this.unHideNode($Hat[%cl.hat]);
				%this.setNodeColor($Hat[%cl.hat],%cl.hatColor);
	}
	if(%cl.accent && %cl.hat)
		switch(%acc)
		{
			case 0: for(%i=0;$accent[%i]!$="";%i++) %this.hideNode($accent[%i]);
			case 1: %slot = %cl.hat == 1 ? 4 : %cl.accent;
					%this.unHideNode($accent[%slot]);
					%this.setNodeColor($accent[%slot],%cl.accentColor);
		}
}

function Player::HipNodes(%this, %cl, %hip)
{
	if(!isObject(%this))
		return;
	switch(%hip)
	{
		case 0:	for(%i=0;$hip[%i]!$= "";%i++) %this.hideNode($Hip[%i]);
		case 1:	%this.unHideNode($Hip[%cl.hip]);
				%this.setNodeColor($Hip[%cl.hip],%cl.hipColor);
	}
}

function Player::LegNodes(%this, %cl, %lLeg, %rLeg)
{
	if(!isObject(%this))
		return error("object not found");
	switch(%lLeg)
	{
		case 0:	%this.hideNode("skirtTrimLeft");
				for(%i=0;$LLeg[%i]!$= "";%i++)%this.hideNode($LLeg[%i]);
		case 1:	%this.unHideNode($LLeg[%cl.LLeg]);
				%this.setNodeColor($LLeg[%cl.LLeg],%cl.LLegColor);
	}
	switch(%rLeg)
	{
		case 0:	%this.hideNode("skirtTrimRight");
				for(%i=0;$RLeg[%i]!$="";%i++)%this.hideNode($RLeg[%i]);
		case 1:	%this.unHideNode($RLeg[%cl.RLeg]);
				%this.setNodeColor($RLeg[%cl.RLeg],%cl.RLegColor);
	}
}

function Player::HandNodes(%obj,%cl,%hide,%lhand,%rhand)
{
	if(!isObject(%obj))
		return;
	switch$(%hide)
	{
		case "Hide":	if(%lhand){for(%i=0;$LHand[%i]!$="";%i++)%obj.hideNode($LHand[%i]);}
						if(%rhand){for(%i=0;$RHand[%i]!$="";%i++)%obj.hideNode($RHand[%i]);}
		case "UnHide":	if(%lhand)
						{
							%obj.unHideNode($LHand[%cl.LHand]);
							%obj.setNodeColor($LHand[%cl.LHand],%cl.LHandColor);
						}
						if(%rhand)
						{
							%obj.unHideNode($RHand[%cl.RHand]);
							%obj.setNodeColor($RHand[%cl.RHand],%cl.RHandColor);
						}
	}
}

function Player::SkiNodes(%obj,%cl,%hide,%lski,%rski)
{
	if(!isObject(%obj))
		return;
	switch$(%hide)
	{
		case "Hide":	if(%lski){%obj.hideNode("LSki");}
						if(%rski){%obj.hideNode("RSki");}
		case "UnHide":	if(%lski)
						{
							%obj.unHideNode("LSki");
							%obj.setNodeColor("LSki",%color);
						}
						if(%rski)
						{
							%obj.unHideNode("RSki");
							%obj.setNodeColor("RSki",%color);
						}
	}
}

function Player::PackNodes(%obj,%cl,%hide,%pack,%pack2)
{
	if(!isObject(%obj)){return;}
	switch$(%hide)
	{
		case "Hide":
			if(%pack){for(%i=0;$pack[%i]!$= "";%i++) %obj.hideNode($pack[%i]);}
			if(%pack2){for(%i=0;$secondPack[%i]!$="";%i++) %obj.hideNode($secondPack[%i]);}
		case "UnHide":
			if(%pack)
			{
				%obj.unHideNode($Pack[%cl.Pack]);
				%obj.setNodeColor($Pack[%cl.Pack],%cl.PackColor);
			}
			if(%pack2)
			{
				%obj.unHideNode($secondPack[%cl.secondPack]);
				%obj.setNodeColor($secondPack[%cl.secondPack],%cl.secondPackColor);
			}
	}
}

function Player::BodyNodes(%obj,%cl,%hide,%chest,%larm,%rarm)
{
	if(!isObject(%obj)){return;}
	switch$(%hide)
	{
		case "Hide":	if(%chest){for(%i=0;$chest[%i]!$="";%i++)%obj.hideNode($chest[%i]);}
				if(%larm){for(%i=0;$LArm[%i]!$="";%i++) %obj.hideNode($LArm[%i]);}
				if(%rarm){for(%i=0;$RArm[%i]!$="";%i++) %obj.hideNode($RArm[%i]);}
		case "UnHide":	if(%chest)
				{
					%obj.unHideNode($chest[%cl.chest]);
					%obj.setNodeColor($chest[%cl.chest],%cl.chestColor);
				}
				if(%larm)
				{
					%obj.unHideNode($LArm[%cl.LArm]);
					%obj.setNodeColor($LArm[%cl.LArm],%cl.larmColor);
				}
				if(%rarm)
				{
					%obj.unHideNode($RArm[%cl.RArm]);
					%obj.setNodeColor($RArm[%cl.RArm],%cl.rarmColor);
				}
	}
}

function Player::unmountAllImages(%obj)
{
	if(isObject(%obj))
		for(%i=0;%i<5;%i++)
			%obj.unmountImage(%i);
}

function Player::AllNodes(%obj,%cl,%hide)
{
	if(!isObject(%obj)){return;}
	%obj.HipNodes(%cl,%hide);
	%obj.HeadNodes(%cl,%hide,1,1,1);
	%obj.LegNodes(%cl,%hide,1,1);
	%obj.SkiNodes(%cl,"hide",1,1);
	%obj.PackNodes(%cl,%hide,1,1);
	%obj.BodyNodes(%cl,%hide,1,1,1);
	%obj.HandNodes(%cl,%hide,1,1);
}