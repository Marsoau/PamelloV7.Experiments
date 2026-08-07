using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public class PagodaPillarModel
{
    public BindableBool IsHighlighted { get; } = new();
    
    public List<PagodaGearModel> Gears { get; } = [];

    [CanBeNull]
    public PagodaGearModel TopGear => Gears.FirstOrDefault();
    
    public PagodaPillarModel() {
        
    }
    
    public IEnumerable<PagodaGearModel> GearsAbove(PagodaGearModel targetGear) {
        return Gears.TakeWhile(gear => gear.Level < targetGear.Level);
    }
    
    public bool TryAddGear(PagodaGearModel gear) {
        if (TopGear is not null && TopGear.Level <= gear.Level) return false;
        
        Gears.Insert(0, gear);
        return true;
    }
}
