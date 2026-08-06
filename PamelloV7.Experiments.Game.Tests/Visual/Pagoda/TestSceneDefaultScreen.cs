using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using PamelloV7.Experiments.Game.Screens;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual.Pagoda;

[TestFixture]
public partial class TestScenePagodaStackScreen : ExperimentsTestScene
{
    public TestScenePagodaStackScreen() {
        Add(new ScreenStack(new PagodaStackScreen()) { RelativeSizeAxes = Axes.Both });
    }
}
