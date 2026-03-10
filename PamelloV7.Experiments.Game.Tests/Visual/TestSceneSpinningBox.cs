using osu.Framework.Graphics;
using NUnit.Framework;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual;

[TestFixture]
public partial class TestSceneSpinningBox : ExperimentsTestScene
{
    // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
    // You can make changes to classes associated with the tests and they will recompile and update immediately.

    public TestSceneSpinningBox() {
        Add(new SpinningBox
        {
            Anchor = Anchor.Centre,
        });
    }
}
