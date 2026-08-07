using System;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public class PagodaBoardModel
{
    public BindableList<PagodaMoveRecord> Moves { get; }
    public BindableList<PagodaPillarModel> Pillars { get; }
    public BindableList<PagodaGearModel> Gears { get; }

    public Bindable<PagodaPillarModel> HighlightedPillar = new();
    public Bindable<PagodaGearModel> HighlightedGear = new();
    public Bindable<PagodaGearModel> SelectedGear = new();
    

    public PagodaBoardModel() {
        Moves = [];
        Pillars = [
            new PagodaPillarModel(),
            new PagodaPillarModel(),
            new PagodaPillarModel(),
        ];

        var preGears = Enumerable.Range(1, 2).Select(i => {
            var gear = new PagodaGearModel { Level = i };
            return gear;
        }).ToList();
        
        var firstPillar = Pillars.First();

        foreach (var gear in preGears.AsEnumerable().Reverse()) {
            firstPillar.AddGear(gear);
        }

        Gears = [..preGears];
    }

    public void TryHighlightGear([CanBeNull] PagodaGearModel gear) {
        if (SelectedGear.Value is not null) return;
        if (gear is not null && gear.Pillar.Value.TopGear != gear) return;

        HighlightedGear.Value?.State.Value = PagodaGearModel.PagodaGearState.None;
        
        HighlightedGear.Value = gear;
        gear?.State.Value = PagodaGearModel.PagodaGearState.Highlighted;
    }

    public void TryHighlightPillar([CanBeNull] PagodaPillarModel pillar) {
        if (SelectedGear.Value is null || SelectedGear.Value?.Pillar.Value == pillar) return;
        
        HighlightedPillar.Value?.IsHighlighted.Value = false;
        
        HighlightedPillar.Value = pillar;
        pillar?.IsHighlighted.Value = true;
    }
    
    public bool MoveGear(PagodaGearModel gear, PagodaPillarModel from, PagodaPillarModel to) {
        if (!from.CanRemoveGear(gear) || !to.CanAddGear(gear)) return false;
        
        from.RemoveGear(gear);
        to.AddGear(gear);
            
        Moves.Add(new PagodaMoveRecord(gear, from, to));
        
        return true;
    }
}
