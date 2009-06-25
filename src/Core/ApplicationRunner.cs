namespace Core
{
    public class ApplicationRunner
    {
        private readonly IApplication Runner;

        ApplicationRunner(IApplication runner)
        {
            Runner = runner;
        }

        public static ApplicationRunner Run(IApplication app)
        {
            return new ApplicationRunner(app).Run();
        }

        public ApplicationRunner Run()
        {
            Runner.Get();
            Runner.Process();
            Runner.Post();
            return this;
        }
    }
}