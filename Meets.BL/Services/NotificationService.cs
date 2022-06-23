using Microsoft.Extensions.DependencyInjection;

namespace Meets.BL.Services
{
    public class NotificationService : BackgroundTask
    {
        private readonly IServiceScopeFactory _factory;

        public NotificationService(IServiceScopeFactory factory)
        {
             _factory = factory;
        }

        public override void Start()
        {
            var timer = new Timer(Notify, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void Notify(object state)
        {
            var timeNow = TimeOnly.FromDateTime(DateTime.Now);
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            using var scope = _factory.CreateScope();
            using var store = scope.ServiceProvider.GetRequiredService<IMeetingsStore>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationMessageService>();

            var meetingsSoon = store.GetMeetingByDate(dateNow)
                .Where(x => x.RemindForTime == timeNow);

            notificationService.Notify(meetingsSoon);
        }
    }
}
