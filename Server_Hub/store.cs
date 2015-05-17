function servercmdsetTrail(%client, %trail)
{
   switch$(%trail)
   {
      case "Rainbow":
         %client.player.mountImage(RainbowTrailImage, $backslot);
      case "Dick":
         %client.player.mountImage(DickTrailImage, $backslot);
      case "Lightning":
         %client.player.mountImage(LightningTrailImage, $backslot);
      case "Jew":
         %client.player.mountImage(JewTrailImage, $backslot);
      case "Poland":
         %client.player.mountImage(PolandTrailImage, $backslot);
      case "LOL":
         %client.player.mountImage(LOLTrailImage, $backslot);
      case "":
         %client.chatMessage("\c2The current trails you can use: Rainbow, Dick, Lightning, Jew, Poland, LOL.");
   }
}
//Trails
//Rainbow
datablock ParticleData(RainbowTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/rainbowTrail";
   colors[0]     = "0 0 0 0.1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = true;
};
datablock ParticleEmitterData(RainbowTrailEmitter)
{
  ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "RainbowTrailParticle";
   uiName = "Rainbow Trail";
};
datablock ShapeBaseImageData(RainbowTrailImage)
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
   stateEmitter[1] = "RainbowTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "RainbowTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//Dicks
datablock ParticleData(DickTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/dickTrail";
   colors[0]     = "0 0 0 0.1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = true;
};
datablock ParticleEmitterData(DickTrailEmitter)
{
  ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "DickTrailParticle";
   uiName = "Dick Trail";
};
datablock ShapeBaseImageData(DickTrailImage)
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
   stateEmitter[1] = "DickTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "DickTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//Lightning
datablock ParticleData(LightningTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/lightningTrail";
   spinspeed = 5000;
   colors[0]     = "0 0 0 1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = true;
};
datablock ParticleEmitterData(LightningTrailEmitter)
{
  ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "LightningTrailParticle";
   uiName = "Lightning Trail";
};
datablock ShapeBaseImageData(LightningTrailImage)
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
   stateEmitter[1] = "LightningTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "LightningTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//Jew
datablock ParticleData(JewTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/jewTrail";
   colors[0]     = "0 0 0 1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = false;
};
datablock ParticleEmitterData(JewTrailEmitter)
{
  ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "JewTrailParticle";
   uiName = "Jew Trail";
};
datablock ShapeBaseImageData(JewTrailImage)
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
   stateEmitter[1] = "JewTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "JewTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//Poland
datablock ParticleData(PolandTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/polandTrail";
   colors[0]     = "0 0 0 1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = false;
};
datablock ParticleEmitterData(PolandTrailEmitter)
{
  ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "PolandTrailParticle";
   uiName = "Poland Trail";
};
datablock ShapeBaseImageData(PolandTrailImage)
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
   stateEmitter[1] = "PolandTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "PolandTrailEmitter";
   stateTimeoutValue[2] = 1;
};

//LOL
datablock ParticleData(LOLTrailParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.15;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   textureName          = "./Textures/lolTrail";
   colors[0]     = "0 0 0 1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.6;
   times[0]      = 0.0;
   times[1]      = 1;
   useAlphaInv = false;
};
datablock ParticleEmitterData(LOLTrailEmitter)
{
  ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   ejectionOffset   = 0;
   velocityVariance = 0;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "LOLTrailParticle";
   uiName = "LOL Trail";
};
datablock ShapeBaseImageData(LOLTrailImage)
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
   stateEmitter[1] = "LOLTrailEmitter"; //The emitted emitter datablock here.
   stateEmitterTime[1] = 1; //Time for the emitter to last, in seconds.

   stateName[2] = "loopEnd";
   stateWaitForTimeout[2] = 0;
   stateTransitionOnTimeout[2] = "loopStart";
   stateEmitterTime[2] = 1; //Emitter lifespan
   stateEmitter[2] = "LOLTrailEmitter";
   stateTimeoutValue[2] = 1;
};