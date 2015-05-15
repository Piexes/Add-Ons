function serverCmdGetTrust(%c,%ID)
{
   %b = "BrickGroup_" @ %ID;
   if(!%c.isAdmin) return;
   if(isObject(%b))
   {
      if(getTrustLevel(%b,%c.brickgroup) >= 2)
      {
         %c.chatMessage("You already trust " @ %b.name @ " (ID: " @ %ID @ ") enough.");
         return;
      }
      SetMutualBrickGroupTrust(%c.getBLID(),%ID,2);
      %c.chatMessage("You now have full trust with " @ %b.name @ " (ID: " @ %ID @ ").");
   }
   else if(%c.getBLID() == %ID)
      %c.chatMessage("I thought you already trust yourself.");
   else
      %c.chatMessage("This client (ID: " @ %ID @ ") has not connected to the server yet.");
}
