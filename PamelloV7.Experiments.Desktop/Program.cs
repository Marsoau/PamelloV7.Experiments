using osu.Framework.Platform;
using osu.Framework;
using PamelloV7.Experiments.Game;

namespace PamelloV7.Experiments.Desktop;

public static class Program
{
    public static void Main() {
        using (GameHost host = Host.GetSuitableDesktopHost(@"PamelloV7.Experiments"))
        using (osu.Framework.Game game = new ExperimentsGame())
            host.Run(game);
    }
}
