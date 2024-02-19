using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootTheTargets;

public class Target
{
    public Vector2 Pos;

    public Target(Vector2 pos = default)
    {
        Pos = pos;
        Task.Run(RemoveTarget);
    }

    private async Task RemoveTarget()
    {
        await Task.Delay(5000);
        Game1.Targets.Remove(this);
    }
}