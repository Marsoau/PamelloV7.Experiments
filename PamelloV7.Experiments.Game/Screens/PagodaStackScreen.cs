using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using PamelloV7.Experiments.Game.Elements.Pagoda;
using PamelloV7.Experiments.Game.Elements.Pagoda.Model;

namespace PamelloV7.Experiments.Game.Screens;

public partial class PagodaStackScreen : Screen
{
    [BackgroundDependencyLoader]
    private void load() {
        InternalChildren = [
            new Box
            {
                Colour = Colour4.FromARGB(0xFF666361),
                RelativeSizeAxes = Axes.Both,
            },
            new SpriteText
            {
                Y = 20,
                Text = "Pagoda Stack",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 40),
            },
            new PagodaBoard {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            }
        ];
    }
}
