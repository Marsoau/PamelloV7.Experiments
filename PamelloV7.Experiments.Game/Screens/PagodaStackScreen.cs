using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using PamelloV7.Experiments.Game.Elements.Pagoda;

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
            new Container() {
                AutoSizeAxes = Axes.Both,
                
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                
                Children = [
                    new PagodaPillar() {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                
                        BaseHeight = 200,
                    },
                    new FillFlowContainer {
                        AutoSizeAxes = Axes.Both,
                        
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        
                        Y = -(PagodaPillar.StartHeight + 4),
                
                        Spacing = new Vector2(0, 4),
                        Direction = FillDirection.Vertical,
                
                        Children = GetGears().ToList(),
                    },
                ]
            },
        ];
        
        return;

        IEnumerable<PagodaGear> GetGears() {
            for (var i = 1; i <= 7; i++) {
                yield return new PagodaGear {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    
                    SizeLevel = i,
                };
            }
        }
    }
}
