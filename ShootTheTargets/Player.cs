using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootTheTargets;

public class Player
{
    public Texture2D Sprite;
    public Vector2 Pos;
    public Dictionary<string, Action> ActionMap;
    private readonly int _speed;

    public Player(Texture2D sprite = default, Vector2 pos = default)
    {
        Sprite = sprite;
        Pos = pos;
        _speed = 150;

        ActionMap = new Dictionary<string, Action>
        {
            { "Up", () => DoMovement(new Vector2(0, -1)) },
            { "Down", () => DoMovement(new Vector2(0, 1)) },
            { "Left", () => DoMovement(new Vector2(-1, 0)) },
            { "Right", () => DoMovement(new Vector2(1, 0)) },
            { "MoveAnalog", MoveAnalog },
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

    private void Shoot()
    {
        Console.WriteLine("Shot!");
    }
}