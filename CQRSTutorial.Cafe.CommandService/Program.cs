using System;
using Topshelf;

namespace CQRSTutorial.Cafe.CommandService
{
    static class Program
    {
        static void Main()
        {
            var host = HostFactory.Run(x =>
            {
                x.Service<TabCommandService>(tabService =>
                {
                    tabService.ConstructUsing(ts => new TabCommandService(new MessageBus()));
                    tabService.WhenStarted(ts => ts.Start());
                    tabService.WhenStopped(ts => ts.Stop());
                });

                x.SetDisplayName("CQRSTutorial Tab Command Service");
                x.SetServiceName("cqrstutorial-tab-command-service");
            });

            var exitCode = (int)Convert.ChangeType(host, host.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
