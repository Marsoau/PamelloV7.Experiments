using System;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using PamelloV7.Core.Audio;
using PamelloV7.Experiments.Game.Components;
using PamelloV7.Experiments.Game.Elements;
using PamelloV7.Experiments.Game.Tests.Visual.Base;
using PamelloV7.Wrapper.Extensions;

namespace PamelloV7.Experiments.Game.Tests.Visual;

[TestFixture]
public partial class TestScenePamelloClientComponent : ExperimentsTestScene
{
    private PamelloClientComponent pamello { get; set; } = new();

    private FillFlowContainer container;

    private Box box;

    public TestScenePamelloClientComponent()
    {
        Add(pamello);

        container = new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0),
        };

        Add(container);

        rerenderText();

        pamello.IsConnected.BindValueChanged(_ => Scheduler.Add(rerenderText));
        pamello.IsAuthorized.BindValueChanged(_ => Scheduler.Add(rerenderText));
        pamello.User.BindValueChanged(_ => Scheduler.Add(rerenderText));
        pamello.SelectedPlayer.BindValueChanged(_ => Scheduler.Add(rerenderText));
        pamello.CurrentSong.BindValueChanged(_ => Scheduler.Add(rerenderText));

        pamello.Client.Events.Watch(() => Scheduler.Add(rerenderText), () => [
            pamello.User.Value,
            pamello.SelectedPlayer.Value,
            pamello.CurrentSong.Value,
        ]);

        AddStep("Connect", () => pamello.Client.ConnectAsync("https://server.tpamello.marsoau.com"));
        AddStep("Disconnect", () => pamello.Client.DisconnectAsync());
        AddStep("Authorize", () => pamello.Client.AuthorizeAsync(Guid.Parse("9a40ad25-7e80-43c1-bdd9-a7a84218db5d")));
        AddStep("Unauthorize", () => pamello.Client.UnauthorizeAsync());
    }

    private void rerenderText()
    {
        //don't do that in actual code, better update drawables properties instead of drawables themselves

        container.Clear();
        container.Children =
        [
            new SpriteText()
            {
                Text = $"Is Connected: {pamello.IsConnected.Value}",
            },
            new SpriteText()
            {
                Text = $"Is Authorized: {pamello.IsAuthorized.Value}",
            },
            new SpriteText()
            {
                Text = $"User: {pamello.User.Value}",
            },
            new SpriteText()
            {
                Text = $"Player: {pamello.SelectedPlayer.Value} - {pamello.SelectedPlayer?.Value?.IsPaused} - {new AudioTime(pamello.SelectedPlayer.Value?.Queue.CurrentSongTimePassed ?? 0).ToShortString()}",
            },
            new SpriteText()
            {
                Text = $"Song: {pamello.CurrentSong.Value}",
                EllipsisString = "...",
                AllowMultiline = false,
                Truncate = true,
                MaxWidth = 400,
            },
            new BlackButton()
            {
                Width = 200,
                Action = () => {
                    pamello.Client.Commands.PlayerPauseToggle();
                },
                Text = "Toggle Pause",
            }
        ];
    }
}
