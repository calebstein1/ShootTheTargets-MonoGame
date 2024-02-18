using Microsoft.Xna.Framework;

namespace ShootTheTargets;

public class Target
{
    public Vector2 Pos;

    public Target(Vector2 pos = default)
    {
        Pos = pos;
    }
}