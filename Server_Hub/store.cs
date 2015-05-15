//Trails
datablock ShapeBaseImageData(TrailImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;
	mountPoint = $HeadSlot;
	stateName[0] = "Ready"; //This is first
	stateTransitionOnTimeout[0] = "loopStart"; //Name of state to go to next.
	stateTimeoutValue[0] = 0.1; //Seconds for it to go to the next part of the loop.

	stateName[1] = "loopStart"; //Name of the state
	stateTransitionOnTimeout[1] = "loopEnd"; //next state you go to
	stateTimeoutValue[1] = 0.1; //Seconds til the next part of the loop
	stateEmitter[1] = "TrailEmitter"; //The emitted emitter datablock here.
	stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

	stateName[2] = "loopEnd";
	stateWaitForTimeout[2] = 0;
	stateTransitionOnTimeout[2] = "loopStart";
	stateEmitterTime[2] = 1; //Emitter lifespan
	stateEmitter[2] = "TrailEmitter";
	stateTimeoutValue[2] = 1;
};
datablock ParticleData(TrailParticle)
{
   dragCoefficient      = 6.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = true;
   textureName          = "add-ons/server_hub/trail1";
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 1.0";
   sizes[0]      = 0.6;
   sizes[1]      = 0.4;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(TrailEmitter)
{
   ejectionPeriodMS = 60;
   periodVarianceMS = 0;
   ejectionVelocity = 0.5;
   ejectionOffset   = 0.9;
   velocityVariance = 0.49;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TrailParticle";
   uiName = "Trail - Dutton";
};

function servercmdTrail(%client)
{
	%emitter = new particleEmitterNode()
	{
		dataBlock = "GenericEmitterNode";
		emitter = "TrailEmitter";
		position = "1 1 1";
		spherePlacement = 0;
		velocity = 1;
	};
	%client.player.mountImage(TrailImage,1);
}