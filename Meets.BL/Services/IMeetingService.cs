using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public interface IMeetingService
    {
        Result EnrollMeeting(string name, DateTime startDate, DateTime endDate);

        Result<IEnumerable<Meeting>> GetMeetingByDay(DateOnly date, bool sorted = false);

        Result Remove(Meeting meeting);
    }
}
