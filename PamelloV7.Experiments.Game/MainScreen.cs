using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace PamelloV7.Experiments.Game;

public partial class MainScreen : Screen
{
    [BackgroundDependencyLoader]
    private void load() {
        InternalChildren = [
            new Box
            {
                Colour = Color4.Violet,
                RelativeSizeAxes = Axes.Both,
            },
            new SpriteText
            {
                Y = 20,
                Text = "Main Screen",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = new FontUsage("Torus-Regular", 40)
            },
            new SpinningBox
            {
                Anchor = Anchor.Centre,
            }
        ];
    }
}
