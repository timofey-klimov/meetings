using Meets.BL.Entities;
using Meets.BL.Services;

namespace Meets.BL.Controllers
{
    //Controller
    public class Calendar
    {
        private readonly IMeetingService _meetingService;

        public Calendar(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public Result CreateMeeting(string name, DateTime startDate, DateTime endDate)
        {
            return _meetingService.EnrollMeeting(name, startDate, endDate);
        }

        public Result<IEnumerable<Meeting>> GetMeetingsByDay(DateOnly date, bool sorted)
        {
            return _meetingService.GetMeetingByDay(date, sorted);
        }

        public Result RemoveMeeting(Meeting meeting)
        {
            return _meetingService.Remove(meeting);
        }
    }
}
