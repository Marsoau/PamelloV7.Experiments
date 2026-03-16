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

    private readonly SpriteText isConnectedText;
    private readonly SpriteText isAuthorizedText;
    private readonly SpriteText userText;
    private readonly SpriteText playerText;
    private readonly SpriteText songText;

    public TestScenePamelloClientComponent()
    {
        Add(pamello);

        Add(new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0),
            Children =
            [
                isConnectedText = new SpriteText(),
                isAuthorizedText = new SpriteText(),
                userText = new SpriteText(),
                playerText = new SpriteText(),
                songText = new SpriteText()
                {
                    EllipsisString = "...",
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
            ]
        });

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

        rerenderText();
    }

    [Test]
    public void TestBegin()
    {
        AddStep("Connect", () => pamello.Client.ConnectAsync("https://server.tpamello.marsoau.com"));
        AddUntilStep("Wait Connected", () => pamello.IsConnected.Value);
        AddAssert("Is Connected", () => pamello.IsConnected.Value);
        AddStep("Authorize", () => pamello.Client.AuthorizeAsync(Guid.Parse("9a40ad25-7e80-43c1-bdd9-a7a84218db5d")));
        AddUntilStep("Wait Authorized", () => pamello.IsAuthorized.Value);
        AddAssert("Is Authorized", () => pamello.IsAuthorized.Value);
    }

    [Test]
    public void TestEnd()
    {
        AddStep("Unauthorize", () => pamello.Client.UnauthorizeAsync());
        AddStep("Disconnect", () => pamello.Client.DisconnectAsync());
        AddAssert("Is Unauthorized", () => !pamello.IsAuthorized.Value);
        AddAssert("Is Disconnected", () => !pamello.IsConnected.Value);
    }

    private void rerenderText()
    {
        //don't do that in actual code, better update drawables properties instead of drawables themselves

        isConnectedText.Text = $"Is Connected: {pamello.IsConnected.Value}";
        isAuthorizedText.Text = $"Is Authorized: {pamello.IsAuthorized.Value}";
        userText.Text = $"User: {pamello.User.Value}";
        playerText.Text = $"Player: {pamello.SelectedPlayer.Value} - {pamello.SelectedPlayer?.Value?.IsPaused} - {new AudioTime(pamello.SelectedPlayer.Value?.Queue.CurrentSongTimePassed ?? 0).ToShortString()}";
        songText.Text = $"Song: {pamello.CurrentSong.Value}";
    }
}
