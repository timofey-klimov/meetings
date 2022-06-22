using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public interface IMeetingsStore
    {
        IEnumerable<Meeting> GetMeetingByDate(DateOnly date);

        void Add(Meeting meeting);
    }
}
