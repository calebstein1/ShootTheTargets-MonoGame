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
            { "Up", MoveUp },
            { "Down", MoveDown },
            { "Left", MoveLeft },
            { "Right", MoveRight },
            { "MoveAnalog", MoveAnalog },
            { "PrimaryAction", Shoot }
        };
    }

    private void MoveUp()
    {
        DoMovement(Vector2.Normalize(new Vector2(0, -1)));
    }

    private void MoveDown()
    {
        DoMovement(Vector2.Normalize(new Vector2(0, 1)));
    }

    private void MoveLeft()
    {
        DoMovement(Vector2.Normalize(new Vector2(-1, 0)));
    }

    private void MoveRight()
    {
        DoMovement(Vector2.Normalize(new Vector2(1, 0)));
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