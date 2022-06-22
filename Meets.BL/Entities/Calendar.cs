using Meets.BL.Services;

namespace Meets.BL.Entities
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
    }
}
