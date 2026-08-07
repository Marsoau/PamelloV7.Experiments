using System;
using osu.Framework.Bindables;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public class PagodaGearModel
{
    public const int MinSizeLevel = 1;
    public const int MaxSizeLevel = 7;
    
    public BindableBool IsHighlighted { get; } = new();
    public BindableBool IsSelected { get; } = new();

    public required int Level {
        get; init {
            if (value is < MinSizeLevel or > MaxSizeLevel) throw new ArgumentOutOfRangeException(nameof(Level));
            field = value;
        }
    }
}
