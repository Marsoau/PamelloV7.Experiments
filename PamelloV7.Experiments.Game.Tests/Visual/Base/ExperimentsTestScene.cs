using osu.Framework.Testing;

namespace PamelloV7.Experiments.Game.Tests.Visual.Base;

public abstract partial class ExperimentsTestScene : TestScene
{
    protected override ITestSceneTestRunner CreateRunner() => new ExperimentsTestSceneTestRunner();

    private partial class ExperimentsTestSceneTestRunner : ExperimentsGameBase, ITestSceneTestRunner
    {
        private TestSceneTestRunner.TestRunner runner;

        protected override void LoadAsyncComplete() {
            base.LoadAsyncComplete();
            Add(runner = new TestSceneTestRunner.TestRunner());
        }

        public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
    }
}
