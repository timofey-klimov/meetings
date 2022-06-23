using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public class ConsoleNotificationMessageService : INotificationMessageService
    {
        public void Notify(IEnumerable<Meeting> meetings)
        {
            meetings.ToList()
                .ForEach(item => Console.WriteLine($"Скоро встреча {item.Name}. Время начала {item.MeetingStartTime.GetTimeToShortlString()}"));
        }
    }
}
