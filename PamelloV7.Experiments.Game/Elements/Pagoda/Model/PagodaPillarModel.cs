using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public class PagodaPillarModel
{
    public BindableBool IsHighlighted { get; } = new();
    
    public BindableList<PagodaGearModel> Gears { get; } = [];

    [CanBeNull]
    public PagodaGearModel TopGear => Gears.FirstOrDefault();
    
    public PagodaPillarModel() {
        
    }
    
    public IEnumerable<PagodaGearModel> GearsAbove(PagodaGearModel targetGear) {
        return Gears.TakeWhile(gear => gear.Level < targetGear.Level);
    }

    public int GearPosition(PagodaGearModel gear) {
        return Gears.IndexOf(gear);
    }
    
    public bool CanAddGear(PagodaGearModel gear) => TopGear is null || TopGear.Level > gear.Level;
    public bool CanRemoveGear(PagodaGearModel gear) => TopGear is not null && TopGear == gear;
    
    public void RemoveGear(PagodaGearModel gear) {
        if (!CanRemoveGear(gear)) throw new InvalidOperationException($"Cannot remove gear with level {gear.Level} from this pillar");
        
        gear.Pillar.Value = null;
        Gears.Remove(gear);
    }
    public void AddGear(PagodaGearModel gear) {
        if (!CanAddGear(gear)) throw new InvalidOperationException($"Cannot add gear of level {gear.Level} to this pillar with top gear of level {TopGear?.Level}");
        
        Gears.Insert(0, gear);
        gear.Pillar.Value = this;
    }
}
