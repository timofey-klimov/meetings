using Meets.BL.Entities;
using Meets.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Meets.UI
{
    public class Host
    {
        private readonly IServiceProvider _serviceProvider;

        public Host()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IMeetingsStore, MeetingsStore>();
            serviceCollection.AddSingleton<IMeetingService, MeetingService>();
            serviceCollection.AddTransient<Calendar>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }


        public void Run()
        {
            var calendar = _serviceProvider.GetService<Calendar>();

            var ui = new Meets.UI.Ui();

            ui.OnCreateMeeting += (args) => calendar.CreateMeeting(args.Name, args.StartDate, args.EndDate);

            ui.Run();
        }
    }
}
