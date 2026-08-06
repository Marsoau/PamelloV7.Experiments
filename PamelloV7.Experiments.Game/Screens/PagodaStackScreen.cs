using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace PamelloV7.Experiments.Game.Screens;

public partial class PagodaStackScreen : Screen
{
    [BackgroundDependencyLoader]
    private void load() {
        InternalChildren = [
            new Box
            {
                Colour = Colour4.FromARGB(0xFF8c7c65),
                RelativeSizeAxes = Axes.Both,
            },
            new SpriteText
            {
                Y = 20,
                Text = "Pagoda Stack",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 40),
            }
        ];
    }
}
