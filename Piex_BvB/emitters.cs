//Critical Hit Emitter
datablock ParticleData(CritTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 200;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/CriticalHit";
   colors[0]     = "0 0 0 1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = false;
};
datablock ParticleEmitterData(CritTrailEmitter)
{
  ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   ejectionOffset   = 1;
   velocityVariance = 1;
   thetaMin         = 89.9;
   thetaMax         = 90.1;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "CritTrailParticle";
   uiName = "Crit Trail";
};
datablock ShapeBaseImageData(CritTrailImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = false;
   mountPoint = $backslot;
   stateName[0] = "Ready"; //This is first
   stateTransitionOnTimeout[0] = "loopStart"; //Name of state to go to next.
   stateTimeoutValue[0] = 0.1; //Seconds for it to go to the next part of the loop.

   stateName[1] = "loopStart"; //Name of the state
   stateTransitionOnTimeout[1] = "loopEnd"; //next state you go to
   stateTimeoutValue[1] = 0.1; //Seconds til the next part of the loop
   stateEmitter[1] = "CritTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "CritTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//Applying it
function applyCritTrail(%client, %length)
{
   %client.player.mountImage(CritTrailImage, $backslot);
   if(%length $= "short")
      %client.player.schedule(1000, unMountImage, $backslot);
   else if(%length $= "long")
      %client.player.schedule(15000, unMountImage, $backslot);
}