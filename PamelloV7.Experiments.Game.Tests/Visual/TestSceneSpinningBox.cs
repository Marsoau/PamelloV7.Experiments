using osu.Framework.Graphics;
using NUnit.Framework;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual;

[TestFixture]
public partial class TestSceneSpinningBox : ExperimentsTestScene
{
    public TestSceneSpinningBox() {
        Add(new SpinningBox
        {
            Anchor = Anchor.Centre,
        });
    }
}
