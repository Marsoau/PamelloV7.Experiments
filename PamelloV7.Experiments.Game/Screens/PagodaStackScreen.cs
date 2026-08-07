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
            new FillFlowContainer() {
                AutoSizeAxes = Axes.Both,
                
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(0, 10),
                
                Children = GetPillarsContainers(2).ToList(),
            }
        ];
        
        return;

        IEnumerable<Container> GetPillarsContainers(int count) {
            for (var i = 1; i <= count; i++) {
                var pillar = new PagodaPillarModel();
                var gears = GetGears(i == 1 ? 2 : 0).ToList();

                foreach (var gear in gears.AsEnumerable().Reverse()) {
                    Console.WriteLine($"Adding gear: {gear.Model.Level}, {pillar.TryAddGear(gear.Model)}");
                }

                Console.WriteLine($"Top Gear: {pillar.TopGear}");

                yield return new Container() {
                    AutoSizeAxes = Axes.Both,

                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,

                    Children = [
                        new PagodaPillar(pillar) {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,

                            BaseHeight = (PagodaGear.BaseHeight + 4) * (PagodaGearModel.MaxSizeLevel + 1),
                        },
                        new FillFlowContainer {
                            AutoSizeAxes = Axes.Both,

                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,

                            Y = -(PagodaPillar.StartHeight + 4),

                            Spacing = new Vector2(0, 4),
                            Direction = FillDirection.Vertical,

                            Children = gears,
                        },
                    ]
                };
            }
        }

        IEnumerable<PagodaGear> GetGears(int count) {
            for (var i = 1; i <= count; i++) {
                var model = new PagodaGearModel {
                    Level = i,
                    IsHighlighted = { Value = i <= 0 },
                };
                
                yield return new PagodaGear(model) {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                };
            }
        }
    }
}
