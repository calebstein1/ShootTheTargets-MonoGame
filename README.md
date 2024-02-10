# Shoot The Targets

Shoot The Targets is a (very) simple target shooting game using MonoGame.
It's actually not meant to be very fun on it's own, it's meant to demonstrate my InputHandler library which can be found over [here](https://github.com/calebstein1/InputHandler).

Right now, it's just a crosshair that moves around on screen (I guess that's actually technically enough to demonstrate the library!) and logs when you shoot to the console, but ultimately you'll be shooting at randomly appearing targets.
Once it's done I'll probably throw some pre-compiled binaries up here.

## Building

You'll need to download the [InputHandler](https://github.com/calebstein1/InputHandler) and fix the reference in ShootTheTargets.csproj, but then it should just be a `dotnet run` in the ShootTheTargets directory, or build the solution with Visual Studio or Rider.

## Controls

Movement: WASD keys, arrow keys, controller d-pad, or controller left thumbstick (thumbstick supports smooth directional movement)

Shoot: spacebar, left click, or controller a or b buttons
