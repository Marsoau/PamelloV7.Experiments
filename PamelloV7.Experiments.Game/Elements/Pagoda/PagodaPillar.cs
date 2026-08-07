using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using PamelloV7.Experiments.Game.Elements.Pagoda.Model;

namespace PamelloV7.Experiments.Game.Elements.Pagoda;

public partial class PagodaPillar : CompositeDrawable
{
    public static Colour4 PillarBrown => Colour4.FromARGB(0xFF876649);
    public static Colour4 PillarYellow => Colour4.FromARGB(0xFFe6b64e);

    public const float StartHeight = 6;

    public float BaseHeight {
        get => _basePillarBox.Height;
        set => _basePillarBox.Height = value;
    }

    public PagodaPillarModel Model { get; }
    
    private Bindable<bool> _isHighlighted;

    private readonly Box _basePillarBox;
    private readonly BufferedContainer _highlightContainer;
    
    public PagodaPillar(PagodaPillarModel model) {
        Model = model;
        
        InternalChildren = [
            _highlightContainer = new BufferedContainer {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
                
                Y = -25,
                
                Size = new Vector2(24, 24),
                Scale = new Vector2(1, 0),
                
                Children = [
                    new Triangle {
                        RelativeSizeAxes = Axes.Both,
                        
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        
                        Scale = new Vector2(1, -1),
                        
                        Colour = ColourInfo.GradientVertical(
                            PillarYellow.Lighten(0.1f),
                            PillarYellow.Darken(0.2f)
                        ),
                    },
                    new Triangle {
                        RelativeSizeAxes = Axes.Both,
                        
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.Centre,
                        
                        Width = 2,
                        
                        Scale = new Vector2(1, -1),
                        
                        Blending = new BlendingParameters
                        {
                            Source = BlendingType.Zero,
                            Destination = BlendingType.OneMinusSrcAlpha,
                            SourceAlpha = BlendingType.Zero,
                            DestinationAlpha = BlendingType.OneMinusSrcAlpha,
                        },
                    }
                ]
            },
            new FillFlowContainer {
                AutoSizeAxes = Axes.Both,
                
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,

                Direction = FillDirection.Vertical,

                Children = [
                    new Container() {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,

                        Size = new Vector2(20, 25),

                        Masking = true,

                        Child = new Container() {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,

                            Size = new Vector2(20, 30),

                            Children = [
                                new Container {
                                    RelativeSizeAxes = Axes.Both,

                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,

                                    Height = 0.5f,
                                    Width = 0.5f,

                                    Masking = true,

                                    Child = new Triangle() {
                                        RelativeSizeAxes = Axes.Both,
                                        Width = 2,
                                        Colour = ColourInfo.GradientHorizontal(
                                            PillarYellow.Lighten(0.1f),
                                            PillarYellow.Darken(0.2f)
                                        ),
                                    }
                                },
                                new Container {
                                    RelativeSizeAxes = Axes.Both,

                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,

                                    Height = 0.5f,
                                    Width = 0.5f,

                                    Masking = true,

                                    Child = new Triangle() {
                                        RelativeSizeAxes = Axes.Both,

                                        Anchor = Anchor.TopRight,
                                        Origin = Anchor.TopRight,

                                        Width = 2,

                                        Colour = ColourInfo.GradientHorizontal(
                                            PillarYellow,
                                            PillarYellow.Darken(0.2f)
                                        ),
                                    }
                                },
                                new Container {
                                    RelativeSizeAxes = Axes.Both,

                                    Anchor = Anchor.BottomLeft,
                                    Origin = Anchor.TopLeft,

                                    Height = 0.5f,
                                    Width = 0.5f,

                                    Masking = true,
                                    Scale = new Vector2(1, -1),

                                    Child = new Triangle() {
                                        RelativeSizeAxes = Axes.Both,
                                        Width = 2,
                                        Colour = ColourInfo.GradientHorizontal(
                                            PillarYellow.Darken(0.1f),
                                            PillarYellow.Darken(0.2f)
                                        ),
                                    }
                                },
                                new Container {
                                    RelativeSizeAxes = Axes.Both,

                                    Anchor = Anchor.BottomRight,
                                    Origin = Anchor.TopRight,

                                    Height = 0.5f,
                                    Width = 0.5f,

                                    Masking = true,
                                    Scale = new Vector2(1, -1),

                                    Child = new Triangle() {
                                        RelativeSizeAxes = Axes.Both,

                                        Anchor = Anchor.CentreRight,
                                        Origin = Anchor.CentreRight,

                                        Width = 2,

                                        Colour = ColourInfo.GradientHorizontal(
                                            PillarYellow.Darken(0.2f),
                                            PillarYellow.Darken(0.3f)
                                        ),
                                    }
                                }
                            ]
                        },
                    },
                    new Box {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(24, 6),
                        Colour = ColourInfo.GradientHorizontal(
                            PillarYellow.Lighten(0.1f),
                            PillarYellow.Darken(0.2f)
                        ),
                    },
                    new Box {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(20, 6),
                        Colour = ColourInfo.GradientHorizontal(
                            PillarBrown.Lighten(0.1f),
                            PillarBrown.Darken(0.2f)
                        )
                    },
                    new Box {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(24, 6),
                        Colour = ColourInfo.GradientHorizontal(
                            PillarYellow.Lighten(0.1f),
                            PillarYellow.Darken(0.2f)
                        )
                    },
                    _basePillarBox = new Box {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,

                        Size = new Vector2(20, 200),

                        Colour = ColourInfo.GradientHorizontal(
                            PillarBrown.Lighten(0.1f),
                            PillarBrown.Darken(0.2f)
                        )
                    },
                    new Box {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(34, StartHeight),
                        Colour = ColourInfo.GradientHorizontal(
                            PillarBrown,
                            PillarBrown.Darken(0.3f)
                        )
                    }
                ]
            }
        ];
        
        _isHighlighted = Model.IsHighlighted.GetBoundCopy();
        
        _isHighlighted.BindValueChanged(HighlightChanged);
    }

    [BackgroundDependencyLoader]
    private void load() {
        AutoSizeAxes = Axes.Y;
        Width = PagodaGear.GetLevelSize(PagodaGearModel.MaxSizeLevel);
    }

    protected override bool OnHover(HoverEvent e) => false;

    public void HighlightChanged(ValueChangedEvent<bool> change) {
        _highlightContainer.ScaleTo(change.NewValue ? new Vector2(1, 1) : new Vector2(1, 0), 200, Easing.OutQuint);
    }
}
