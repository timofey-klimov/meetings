using Meets.BL.Entities;
using System.Collections.Concurrent;

namespace Meets.BL.Services
{
    public class MeetingsStore : IMeetingsStore
    {
        private readonly ConcurrentDictionary<DateOnly, ICollection<Meeting>> _store;

        public MeetingsStore()
        {
            _store = new ConcurrentDictionary<DateOnly, ICollection<Meeting>>();
        }

        public IEnumerable<Meeting> GetMeetingByDate(DateOnly date, bool sorted = false)
        {
            var meetings = _store.Keys.Contains(date) ? _store[date] : Array.Empty<Meeting>();

            return sorted ? meetings.OrderBy(x => x.MeetingStartTime.Date) : meetings;
        }


        public void Add(Meeting meeting)
        {
            _store.AddOrUpdate(meeting.MeetingStartTime.Date,
                x => new List<Meeting>() { meeting },
                (x, y) =>
                {
                    y.Add(meeting);
                    return y;
                });
        }

        public bool Remove(Meeting meeting)
        {
            var meetings = _store[meeting.MeetingStartTime.Date];

            return meetings.Remove(meeting);
        }

        public void Dispose()
        {
            
        }
    }
}
