using osu.Framework.Allocation;
using NUnit.Framework;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual;

[TestFixture]
public partial class TestSceneExperimentsGame : ExperimentsTestScene
{
    [BackgroundDependencyLoader]
    private void load() {
        AddGame(new ExperimentsGame());
    }
}
