using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace PamelloV7.Experiments.Game.Elements.Pagoda;

public partial class PagodaGear : CompositeDrawable
{
    private readonly Box _baseGearBox;
    
    public const float BaseSize = 48;
    public const float LevelSize = 16;
    public const float LevelGrowth = 4;
    
    public const int MinSizeLevel = 1;
    public const int MaxSizeLevel = 7;

    public static List<Colour4> LevelColors = [
        Colour4.FromHex("9b90bf"),
        Colour4.FromHex("84b3bd"),
        Colour4.FromHex("95995e"),
        Colour4.FromHex("d0c559"),
        Colour4.FromHex("f8c45b"),
        Colour4.FromHex("f79454"),
        Colour4.FromHex("e5794e")
    ];
    
    public static List<ColourInfo> LevelColorInfos = LevelColors.Select(c => ColourInfo.GradientHorizontal(
        c.Lighten(0.1f),
        c.Darken(0.1f)
    )).ToList();
    
    public int SizeLevel {
        get; set {
            if (field == value) return;
            if (value < MinSizeLevel) value = MinSizeLevel;
            if (value > MaxSizeLevel) value = MaxSizeLevel;
            
            field = value;
            
            _baseGearBox.Width = BaseSize + LevelSize * value + LevelGrowth * (value * value) / 2;
            _baseGearBox.Colour = LevelColorInfos[value - 1];
        }
    }
    
    public PagodaGear() {
        InternalChild = new Container {
            AutoSizeAxes = Axes.Both,

            Child = _baseGearBox = new Box {
                Size = new Vector2(0, 16),
            }
        };
        
        SizeLevel = 1;
    }

    [BackgroundDependencyLoader]
    private void load() {
        AutoSizeAxes = Axes.Both;
    }
}
