using Meets.BL.Entities;

namespace Meets.BL.Services
{
    public interface INotificationMessageService
    {
        void Notify(IEnumerable<Meeting> meetings);
    }
}
