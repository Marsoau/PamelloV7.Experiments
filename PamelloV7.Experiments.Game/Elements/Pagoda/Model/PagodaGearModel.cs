using System;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public class PagodaGearModel
{
    public enum PagodaGearState
    {
        None,
        Highlighted,
        Selected,
    }
    
    public const int MinSizeLevel = 1;
    public const int MaxSizeLevel = 7;
    
    public Bindable<PagodaPillarModel> Pillar { get; } = new();
    
    public Bindable<PagodaGearState> State { get; } = new();

    public required int Level {
        get; init {
            if (value is < MinSizeLevel or > MaxSizeLevel) throw new ArgumentOutOfRangeException(nameof(Level));
            field = value;
        }
    }
}
