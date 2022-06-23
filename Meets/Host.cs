using Meets.BL.Controllers;
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
            serviceCollection.AddSingleton<INotificationMessageService, ConsoleNotificationMessageService>();
            serviceCollection.AddSingleton<NotificationService>();
            serviceCollection.AddTransient<Calendar>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }


        public void Run()
        {
            var calendar = _serviceProvider.GetService<Calendar>();
            var notifyService = _serviceProvider.GetService<NotificationService>();

            var ui = new Ui();

            ui.OnCreateMeeting += (args) => calendar.CreateMeeting(args.Name, args.StartDate, args.EndDate);
            ui.OnGetMeetingsByDay += (date, sorted) => calendar.GetMeetingsByDay(date, sorted);
            ui.OnRemoveMeeting += (meeting) => calendar.RemoveMeeting(meeting);

            var backgroundThread = new Thread(() => notifyService.Start());
            backgroundThread.Start();

            ui.Run();
        }
    }
}
