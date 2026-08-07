using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using PamelloV7.Experiments.Game.Elements.Pagoda.Model;

namespace PamelloV7.Experiments.Game.Elements.Pagoda;

public partial class PagodaBoard : CompositeDrawable
{
    public List<PagodaPillar> Pillars { get; }
    public List<PagodaGear> Gears { get; }
    
    public Container GearsFlyingArea { get; }
    
    
    public PagodaBoardModel Model { get; }
    
    public PagodaBoard(PagodaBoardModel model = null) {
        Model = model ?? new PagodaBoardModel();
        
        InternalChildren = [
            new FillFlowContainer {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                
                Children = [
                    GearsFlyingArea = new Container {
                        RelativeSizeAxes = Axes.X,
                        Height = 100,
                
                        Children = [
                            new Box {
                                RelativeSizeAxes = Axes.Both,
                                
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                
                                Colour = Colour4.White.Opacity(0.5f),
                            }
                        ]
                    },
                    new Container {
                        AutoSizeAxes = Axes.Both,
                
                        Child = new FillFlowContainer {
                            AutoSizeAxes = Axes.Both,
                            
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(20, 0),
                            
                            Children = Pillars = GetPillars().ToList()
                        }
                    },
                ]
            },
            ..Gears = GetGears().ToList()
        ];
    }

    [BackgroundDependencyLoader]
    private void load() {
        AutoSizeAxes = Axes.Both;
    }
    
    [CanBeNull]
    private PagodaPillar GetPillarForModel(PagodaPillarModel model) => Pillars.FirstOrDefault(p => p.Model == model);

    public Vector2 GetGearPillarPosition(PagodaGearModel gear) {
        var pillar = GetPillarForModel(gear.Pillar.Value);
        if (pillar is null) return Vector2.Zero;

        const float spacing = 4;
        
        var gearPosition = pillar.ToSpaceOfOtherDrawable(Vector2.Zero, this);
        if (gearPosition == Vector2.Zero) return Vector2.Zero;
        
        gearPosition.X += pillar.Width / 2;
        gearPosition.Y += pillar.Height - PagodaPillar.StartHeight - PagodaGear.BaseHeight / 2;
        
        gearPosition.Y -= (PagodaGear.BaseHeight + spacing) * pillar.Model.Gears.Count;
        gearPosition.Y += (pillar.Model.GearPosition(gear) + 1) * (PagodaGear.BaseHeight + spacing);
        
        gearPosition.Y -= spacing;
        
        return gearPosition;
    }

    public Vector2 GetGearSelectedPosition(PagodaGearModel gear) {
        var pillar = GetPillarForModel(gear.Pillar.Value);
        if (pillar is null) return Vector2.Zero;
        
        var pillarPosition = pillar.ToSpaceOfOtherDrawable(Vector2.Zero, this);
        if (pillarPosition == Vector2.Zero) return Vector2.Zero;
        
        var flyingAreaPosition = GearsFlyingArea.ToSpaceOfOtherDrawable(Vector2.Zero, this);
        if (flyingAreaPosition == Vector2.Zero) return Vector2.Zero;
        
        var gearPosition = new Vector2(
            pillarPosition.X + pillar.Width / 2,
            flyingAreaPosition.Y + GearsFlyingArea.Height / 2
        );
        
        return gearPosition;
    }
    
    private IEnumerable<PagodaPillar> GetPillars() {
        return Model.Pillars.Select(p => new PagodaPillar(p) {
            Anchor = Anchor.BottomCentre,
            Origin = Anchor.BottomCentre,
        });
    }

    private IEnumerable<PagodaGear> GetGears() {
        foreach (var gearModel in Model.Gears) {
            yield return new PagodaGear(gearModel) {
                Origin = Anchor.Centre,
            };
        }
    }

    protected override void UpdateAfterChildren() {
        foreach (var gear in Gears) {
            var position = gear.Model.State.Value switch {
                PagodaGearModel.PagodaGearState.Selected => GetGearSelectedPosition(gear.Model),
                _ => GetGearPillarPosition(gear.Model),
            };
            gear.Position = position;
        }
        base.UpdateAfterChildren();
    }

    public (PagodaPillar pillar, PagodaGear gear) GetHoveredItems() {
        var inputManager = GetContainingInputManager();
        if (inputManager is null) return (null, null);
        
        var hovered = inputManager.HoveredDrawables;
        
        PagodaPillar pillar = null;
        PagodaGear gear = null;
        
        foreach (var drawable in hovered) {
            if (drawable is PagodaPillar currentPillar) pillar = currentPillar;
            if (drawable is PagodaGear currentGear) gear = currentGear;
        }
        
        return (pillar, gear);
    }

    protected override bool OnMouseMove(MouseMoveEvent e) {
        UpdateItems();
        return base.OnMouseMove(e);
    }

    protected override void OnHoverLost(HoverLostEvent e) {
        UpdateItems();
        base.OnHoverLost(e);
    }

    public void UpdateItems() {
        var items = GetHoveredItems();
        
        Console.WriteLine($"pillar: {items.pillar}, gear: {items.gear}");
        
        if (items.pillar is not null) {
            Model.TryHighlightPillar(items.pillar.Model);
            Model.TryHighlightGear(items.pillar.Model.TopGear);
        }
        else {
            Model.TryHighlightPillar(null);
            Model.TryHighlightGear(null);
        }
    }
}
