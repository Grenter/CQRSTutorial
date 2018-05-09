using System.ServiceProcess;

namespace CQRSTutorial.Cafe.CommandService
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new TabCommandService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
