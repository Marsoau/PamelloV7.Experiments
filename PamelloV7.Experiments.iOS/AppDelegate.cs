using osu.Framework.iOS;
using PamelloV7.Experiments.Game;

namespace PamelloV7.Experiments.iOS
{
    /// <inheritdoc />
    public class AppDelegate : GameApplicationDelegate
    {
        /// <inheritdoc />
        protected override osu.Framework.Game CreateGame() => new PamelloV7.ExperimentsGame();
    }
}
