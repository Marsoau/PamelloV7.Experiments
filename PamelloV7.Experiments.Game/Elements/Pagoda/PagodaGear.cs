using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osuTK;
using PamelloV7.Experiments.Game.Elements.Pagoda.Model;

namespace PamelloV7.Experiments.Game.Elements.Pagoda;

public partial class PagodaGear : CompositeDrawable
{
    public PagodaGearModel Model { get; }
    
    private readonly Box _baseGearBox;
    private readonly Container _gearContainer;

    private Bindable<bool> _isHighlighted;
    private Bindable<bool> _isSelected;
    
    public const float BaseHeight = 16;
    
    public const float BaseSize = 48;
    public const float LevelSize = 16;
    public const float LevelGrowth = 4;

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

    public static EdgeEffectParameters EdgeEffectHighlighted = new() {
        Type = EdgeEffectType.Glow,
        Colour = Colour4.White.Opacity(0.5f),
        Radius = 5,
    };
    public static EdgeEffectParameters EdgeEffectSelected = new() {
        Type = EdgeEffectType.Shadow,
        Colour = Colour4.Gold.Opacity(0.8f),
        Radius = 5,
    };
    public static EdgeEffectParameters EdgeEffectDefault = new() {
        Type = EdgeEffectType.Glow,
        Colour = Colour4.White.Opacity(0.0f),
    };
    
    public PagodaGear(PagodaGearModel model) {
        Model = model;
        
        InternalChild = _gearContainer = new Container {
            AutoSizeAxes = Axes.Both,
            
            Masking = true,
            EdgeEffect = EdgeEffectDefault,

            Child = _baseGearBox = new Box {
                Height = BaseHeight,
                Width = GetLevelSize(Model.Level),
                
                Colour = LevelColorInfos[Model.Level - 1]
            }
        };
    }

    public static float GetLevelSize(int level)
        => BaseSize + LevelSize * level + LevelGrowth * (level * level) / 2;

    [BackgroundDependencyLoader]
    private void load() {
        AutoSizeAxes = Axes.Both;

        _isHighlighted = Model.IsHighlighted.GetBoundCopy();
        _isSelected = Model.IsSelected.GetBoundCopy();
        
        _isHighlighted.BindValueChanged(_ => UpdateVisuals());
        _isSelected.BindValueChanged(_ => UpdateVisuals());
        
        UpdateVisuals();
    }
    
    private void UpdateVisuals() {
        if (_isSelected.Value) {
            _gearContainer.TweenEdgeEffectTo(EdgeEffectSelected, 200, Easing.OutQuint);
        }
        else if (_isHighlighted.Value) {
            _gearContainer.TweenEdgeEffectTo(EdgeEffectHighlighted, 200, Easing.OutQuint);
        }
        else {
            _gearContainer.TweenEdgeEffectTo(EdgeEffectDefault, 200, Easing.OutQuint);
        }
    }
}
