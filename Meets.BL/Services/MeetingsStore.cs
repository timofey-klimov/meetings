using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public class MeetingsStore : IMeetingsStore
    {
        private readonly Dictionary<DateOnly, ICollection<Meeting>> _store;

        public MeetingsStore()
        {
            _store = new Dictionary<DateOnly, ICollection<Meeting>>();
        }

        public IEnumerable<Meeting> GetMeetingByDate(DateOnly date)
        {
            return _store.Keys.Contains(date) ? _store[date] : Array.Empty<Meeting>();
        }


        public void Add(Meeting meeting)
        {
            if (_store.Keys.Contains(meeting.MeetingStartTime.Date))
                _store[meeting.MeetingStartTime.Date].Add(meeting);
            else
                _store.Add(meeting.MeetingStartTime.Date, new List<Meeting>() { meeting });
        }
    }
}
