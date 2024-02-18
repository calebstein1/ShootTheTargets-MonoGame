using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootTheTargets;

public class Player
{
    public Texture2D Sprite;
    public Vector2 Pos;
    public readonly Dictionary<string, Action> MoveAnalogActionMap, MoveDiscreetActionMap;
    private readonly int _speed;
    private bool _didShoot;

    public Player(Texture2D sprite = default, Vector2 pos = default)
    {
        Sprite = sprite;
        Pos = pos;
        _speed = 150;
        _didShoot = false;

        MoveAnalogActionMap = new Dictionary<string, Action>
        {
            { "MoveAnalog", MoveAnalog },
            { "PrimaryAction", Shoot }
        };
        
        MoveDiscreetActionMap = new Dictionary<string, Action>
        {
            { "Up", () => DoMovement(new Vector2(0, -1)) },
            { "Down", () => DoMovement(new Vector2(0, 1)) },
            { "Left", () => DoMovement(new Vector2(-1, 0)) },
            { "Right", () => DoMovement(new Vector2(1, 0)) },
            { "PrimaryAction", Shoot }
        };
    }

    private void MoveAnalog()
    {
        var thumbStickVector = Vector2.Normalize(GamePad.GetState(0).ThumbSticks.Left);
        thumbStickVector.Y *= -1;
        DoMovement(thumbStickVector);
    }

    private void DoMovement(Vector2 direction)
    {
        Pos += _speed * Game1.Delta * direction;
    }

    /*
     * Don't use async void in your code!
     * This game is meant to demonstrate the InputHandler library, not define the most robust cooldown mechanism.
     */
    private async void Shoot()
    {
        if (_didShoot) return;
        Console.WriteLine("Shot!");
        _didShoot = true;
        await Task.Delay(500);
        _didShoot = false;
    }
}
