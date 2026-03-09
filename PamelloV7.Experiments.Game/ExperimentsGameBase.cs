using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using PamelloV7.Experiments.Resources;

namespace PamelloV7.Experiments.Game;

public partial class ExperimentsGameBase : osu.Framework.Game
{
    // Anything in this class is shared between the test browser and the game implementation.
    // It allows for caching global dependencies that should be accessible to tests, or changing
    // the screen scaling for all components including the test browser and framework overlays.
    protected override Container<Drawable> Content { get; }

    protected ExperimentsGameBase()
    {
        // Ensure game and tests scale with window size and screen DPI.
        base.Content.Add(Content = new DrawSizePreservingFillContainer
        {
            // You may want to change TargetDrawSize to your "default" resolution, which will decide how things scale and position when using absolute coordinates.
            TargetDrawSize = new Vector2(1280, 720)
        });
    }

    [BackgroundDependencyLoader]
    private void load(FrameworkConfigManager config) {
        Resources.AddStore(new DllResourceStore(typeof(ExperimentsResources).Assembly));

        AddFont(Resources, @"Fonts/Torus/Torus-Regular");
        AddFont(Resources, @"Fonts/Torus/Torus-Light");
        AddFont(Resources, @"Fonts/Torus/Torus-SemiBold");
        AddFont(Resources, @"Fonts/Torus/Torus-Bold");

        AddFont(Resources, @"Fonts/Torus-Alternate/Torus-Alternate-Regular");
        AddFont(Resources, @"Fonts/Torus-Alternate/Torus-Alternate-Light");
        AddFont(Resources, @"Fonts/Torus-Alternate/Torus-Alternate-SemiBold");
        AddFont(Resources, @"Fonts/Torus-Alternate/Torus-Alternate-Bold");

        AddFont(Resources, @"Fonts/Inter/Inter-Regular");
        AddFont(Resources, @"Fonts/Inter/Inter-RegularItalic");
        AddFont(Resources, @"Fonts/Inter/Inter-Light");
        AddFont(Resources, @"Fonts/Inter/Inter-LightItalic");
        AddFont(Resources, @"Fonts/Inter/Inter-SemiBold");
        AddFont(Resources, @"Fonts/Inter/Inter-SemiBoldItalic");
        AddFont(Resources, @"Fonts/Inter/Inter-Bold");
        AddFont(Resources, @"Fonts/Inter/Inter-BoldItalic");

        AddFont(Resources, @"Fonts/Noto/Noto-Basic");
        AddFont(Resources, @"Fonts/Noto/Noto-Bopomofo");
        AddFont(Resources, @"Fonts/Noto/Noto-CJK-Basic");
        AddFont(Resources, @"Fonts/Noto/Noto-CJK-Compatibility");
        AddFont(Resources, @"Fonts/Noto/Noto-Hangul");
        AddFont(Resources, @"Fonts/Noto/Noto-Thai");

        AddFont(Resources, @"Fonts/Venera/Venera-Light");
        AddFont(Resources, @"Fonts/Venera/Venera-Bold");
        AddFont(Resources, @"Fonts/Venera/Venera-Black");

        config.SetValue(FrameworkSetting.ShowUnicode, true);
    }
}
