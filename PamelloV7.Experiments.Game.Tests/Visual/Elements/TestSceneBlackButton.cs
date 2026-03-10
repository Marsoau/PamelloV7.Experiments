using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using PamelloV7.Experiments.Game.Elements;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

namespace PamelloV7.Experiments.Game.Tests.Visual.Elements;

public partial class TestSceneBlackButton : ExperimentsTestScene
{
    private BlackButton button;

    public TestSceneBlackButton()
    {
        Add(new Box
        {
            Colour = Colour4.FromHex("222222"),
            RelativeSizeAxes = Axes.Both,
        });
        Add(button = new BlackButton()
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            AutoSizeAxes = Axes.Both,
            Text = "Button Text",
        });

        button.Action = () => { };
    }
}
