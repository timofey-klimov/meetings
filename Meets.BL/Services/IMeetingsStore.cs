using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public interface IMeetingsStore : IDisposable
    {
        IEnumerable<Meeting> GetMeetingByDate(DateOnly date, bool sorted = false);

        void Add(Meeting meeting);

        bool Remove(Meeting meeting);
    }
}
