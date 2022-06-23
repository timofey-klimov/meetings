using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingsStore _store;

        public MeetingService(IMeetingsStore store)
        {
            _store = store;
        }

        public Result EnrollMeeting(string name, DateTime startDate, DateTime endDate)
        {
            var meeting = new Meeting(name, startDate, endDate);

            var meetingsOnDay = _store.GetMeetingByDate(meeting.MeetingStartTime.Date);
            
            var isIntersect = meetingsOnDay
                .Aggregate(false, (a, b) => a |= b.IsIntersect(meeting));

            if (isIntersect == false)
            {
                _store.Add(meeting);
                return Result.Ok();
            }

            return Result.Failt("На это время запланирована другая встреча");
        }

        public Result<IEnumerable<Meeting>> GetMeetingByDay(DateOnly date, bool sorted = false)
        {
            return Result<IEnumerable<Meeting>>.Ok(_store.GetMeetingByDate(date));
        }

        public Result Remove(Meeting meeting)
        {
            if (_store.Remove(meeting))
                return Result.Ok();
            else
                return Result.Failt("Такой встречи не существует");
        }
    }
}
