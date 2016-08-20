datablock ParticleData(HeadBurnParticle)
{
	textureName			 = "base/data/particles/cloud";
	dragCoefficient		 = 0.0;
	gravityCoefficient	 = -1.0;
	inheritedVelFactor	 = 0.0;
	windCoefficient		 = 0;
	constantAcceleration = 3.0;
	lifetimeMS			 = 1200;
	lifetimeVarianceMS   = 100;
	spinSpeed			 = 0;
	spinRandomMin		 = -90.0;
	spinRandomMax		 =  90.0;
	useInvAlpha			 = false;
	colors[0]			 = "1 1 0.3 0.0";
	colors[1]			 = "1 1 0.3 1.0";
	colors[2]			 = "0.6 0.0 0.0";
	sizes[0]			 = 0.0;
	sizes[1]			 = 1.0;
	sizes[2]			 = 1.0;
	times[0]			 = 0.0;
	times[1]			 = 0.2;
	times[2]			 = 1.0;
};
datablock ParticleEmitterData(HeadBurnEmitter)
{
   ejectionPeriodMS = 14;
   periodVarianceMS = 4;
   ejectionVelocity = 0;
   ejectionOffset   = 1.00;
   velocityVariance	= 0.0;
   thetaMin         = 30;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance	= false;
   particles		= HeadBurnParticle;   
};

datablock ShapeBaseImageData(HeadExplosionImage)
{
	shapeFile	= "base/data/shapes/empty.dts";
	emap		= false;
	mountPoint	= $BackSlot;
  	rotation	= "0 0 0 0";

	stateName[0]				= "Start";
	stateTransitionOnTimeout[0]	= "Next1";
	stateWaitForTimeout[0]		= false;
	stateTimeoutValue[0]		= 0.1;
	stateEmitter[0]				= TankSmokeEmitter;
	stateEmitterTime[0]			= 4;

	stateName[1]				= "Next1";
	stateTransitionOnTimeout[1]	= "Next2";
	stateWaitForTimeout[1]		= false;
	stateTimeoutValue[1]		= 0.1;
	stateEmitter[1]				= vehicleExplosionEmitter;
	stateEmitterTime[1]			= 1;

	stateName[2]				= "Next2";
	stateTransitionOnTimeout[2]	= "Final";
	stateWaitForTimeout[2]		= false;
	stateTimeoutValue[2]		= 0.1;
	stateSound[2]				= VehicleExplosionSound;
	stateEmitter[2]				= HeadBurnEmitter;
	stateEmitterTime[2]			= 2;


	stateName[3]				= "Final";
	stateTransitionOnTimeout[3]	= "Done";
	stateWaitForTimeout[3]		= false;
	stateTimeoutValue[3]		= 4.1;
	stateEmitter[3]				= vehicleFinalExplosionEmitter;
	stateEmitterTime[3]			= 1;

	stateName[4]				="Done";
	stateScript[4]				="onDone";
};

function HeadExplosionImage::onDone(%this,%obj,%slot){%obj.unMountImage(%slot);}

function serverCmdExplode(%cl,%part,%vic)
{
	%check = ($Sim::Time > %cl.ffcLastExp) + (%cl.isSuperAdmin && (%vic ? 1:0));
	switch(%check)
	{
		case 0:	CenterPrint(%cl,"Please wait to use this again.",1,2);
				return;
		case 1:	%msg = '\c3%1 \c0has blown up their own %3.';
				%pl  = %cl.player;
		case 2:	%msg = '\c3%1 \c0blew up \c3%2s \c0%3.';
				%vic = FindClientByName(%vic);
				%pl  = %vic.player;
	}
	if(!isObject(%pl))
		return;
	switch$(%part)
	{
		case  "Head":	%pl.headNodes(%vi,hide,1,1,1);
		case "LHand":	%pl.handNodes(%vi,hide,1,0);
		case "RHand":	%pl.handNodes(%vi,hide,0,1);
		case "LFoot":	%pl.legNodes(%vi,hide,1,0);
		case "RFoot":	%pl.legNodes(%vi,hide,0,1);
		case "Pants":	%pl.hipNodes(%vi,hide);
		case  "Body":	for(%i=0;%i<5;%i++)					%pl.unMountImage(%i);
						for(%i=0;$chest[%i]!$= "";%i++)		%pl.hideNode($chest[%i]);
						for(%i=0;$pack[%i]!$= "";%i++)		%pl.hideNode($pack[%i]);
						for(%i=0;$secondPack[%i]!$= "";%i++)%pl.hideNode($secondPack[%i]);
						for(%i=0;$LArm[%i]!$= "";%i++)		%pl.hideNode($lArm[%i]);
						for(%i=0;$RArm[%i]!$= "";%i++)		%pl.hideNode($lArm[%i]);
		default:		return;
	}
	%cl.ffcLastExp = $Sim::Time + 5;
	%pl.emote(HeadExplosionImage);
	messageAll('',%msg,%cl.name,%vic.name,%part);			
}