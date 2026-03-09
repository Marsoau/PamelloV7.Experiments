using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input.Handlers.Mouse;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace PamelloV7.Experiments.Game.Tests;

public partial class ExperimentsTestBrowser : ExperimentsGameBase
{
    protected override void LoadComplete() {
        base.LoadComplete();

        if (Host.AvailableInputHandlers.OfType<MouseHandler>().FirstOrDefault() is { } mouseHandler) {
            mouseHandler.Sensitivity.Value = 1.0;
            mouseHandler.UseRelativeMode.Value = false;
        }

        AddRange([
            new TestBrowser("PamelloV7.Experiments"),
            new CursorContainer()
        ]);
    }

    public override void SetHost(GameHost host) {
        base.SetHost(host);
        host.Window.CursorState |= CursorState.Hidden;
    }
}
