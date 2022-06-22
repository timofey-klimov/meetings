using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public interface IMeetingService
    {
        Result EnrollMeeting(string name, DateTime startDate, DateTime endDate);
    }
}
