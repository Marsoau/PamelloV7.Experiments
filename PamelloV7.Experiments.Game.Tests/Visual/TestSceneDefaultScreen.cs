using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;
using PamelloV7.Experiments.Game.Screens;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual;

[TestFixture]
public partial class TestSceneDefaultScreen : ExperimentsTestScene
{
    public TestSceneDefaultScreen() {
        Add(new ScreenStack(new DefaultScreen()) { RelativeSizeAxes = Axes.Both });
    }
}
