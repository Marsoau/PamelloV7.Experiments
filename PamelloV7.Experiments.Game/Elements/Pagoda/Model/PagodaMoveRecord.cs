using JetBrains.Annotations;

namespace PamelloV7.Experiments.Game.Elements.Pagoda.Model;

public record PagodaMoveRecord(
    PagodaGearModel Gear,
    PagodaPillarModel From,
    PagodaPillarModel To,
    [CanBeNull]
    PagodaMoveRecord CausedBy = null
);
