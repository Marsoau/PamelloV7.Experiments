using osu.Framework;
using osu.Framework.Platform;

namespace PamelloV7.Experiments.Game.Tests
{
    public static class Program
    {
        public static void Main() {
            using (GameHost host = Host.GetSuitableDesktopHost("visual-tests"))
            using (var game = new ExperimentsTestBrowser())
                host.Run(game);
        }
    }
}
