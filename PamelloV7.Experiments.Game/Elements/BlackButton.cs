using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osu.Framework.Logging;
using osu.Framework.Testing.Drawables.Steps;

namespace PamelloV7.Experiments.Game.Elements;

public partial class BlackButton : Button
{
    private Container container;

    private Box hover;
    private Box background;
    private SpriteText spriteText;

    public string Text {
        get => spriteText?.Text.ToString() ?? "";
        set => spriteText?.Text = value;
    }

    public BlackButton() {
        AddInternal(container = new Container {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            AutoSizeAxes = Axes.Both,
            CornerRadius = 10,
            Masking = true,

            Children = [
                background = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.FromHex("121212"),
                    Depth = float.MaxValue,
                },
                hover = new Box
                {
                    Alpha = 0,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                    Blending = BlendingParameters.Additive,
                    Depth = float.MinValue
                },
                spriteText = new SpriteText
                {
                    Depth = -1,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Margin = new MarginPadding { Horizontal = 8, Top = 3, Bottom = 5 },
                    Font = FontUsage.Default.With(size: 16) //.With(weight: "Bold")
                }
            ]
        });
    }

    protected override bool OnHover(HoverEvent e)
    {
        if (Enabled.Value) {
            hover.FadeTo(0.1f, 300, Easing.OutQuint);
        }

        return base.OnHover(e);
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        Content.ScaleTo(0.9f, 2000, Easing.OutQuint);
        return base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        Content.ScaleTo(1, 1000, Easing.OutElastic);
        base.OnMouseUp(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        base.OnHoverLost(e);

        hover.FadeOut(500, Easing.OutQuint);
    }
}
