using System;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using PamelloV7.Core.Audio;
using PamelloV7.Experiments.Game.Components;
using PamelloV7.Experiments.Game.Tests.Visual.Base;

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

        AddStep("Exists", () => Assert.NotNull(pamello));
        AddStep("Connect", () => pamello.Client.ConnectAsync("https://server.tpamello.marsoau.com"));
        AddStep("Disconnect", () => pamello.Client.DisconnectAsync());
        AddStep("Authorize", () => pamello.Client.AuthorizeAsync(Guid.Parse("9a40ad25-7e80-43c1-bdd9-a7a84218db5d")));
        AddStep("Unauthorize", () => pamello.Client.UnauthorizeAsync());
    }

    private void rerenderText()
    {
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
                Text = $"Player: {pamello.SelectedPlayer.Value} - {new AudioTime(pamello.SelectedPlayer.Value?.Queue.CurrentSongTimePassed ?? 0).ToShortString()}",
            },
        ];
    }
}
