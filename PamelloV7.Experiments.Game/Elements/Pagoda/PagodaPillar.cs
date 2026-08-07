using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace PamelloV7.Experiments.Game.Elements.Pagoda;

public partial class PagodaPillar : CompositeDrawable
{
    private static Colour4 PillarBrown => Colour4.FromARGB(0xFF876649);
    private static Colour4 PillarYellow => Colour4.FromARGB(0xFFe6b64e);

    private Box _basePillarBox;

    private const float StartHeight = 6;

    public float BaseHeight {
        get => _basePillarBox.Height;
        set => _basePillarBox.Height = value;
    }
    
    public PagodaPillar() {
        InternalChild = new FillFlowContainer {
            AutoSizeAxes = Axes.Both,
            
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
        };
    }

    [BackgroundDependencyLoader]
    private void load() {
        AutoSizeAxes = Axes.Both;
    }
}
