namespace Meets.BL.Entities
{
    public class Meeting : ValueObject
    {
        public string Name { get; private set; }

        public MeetingTime MeetingStartTime { get; private set; }

        public MeetingTime MeetingEndTime { get; private set; }

        public TimeOnly? RemindForTime { get; private set; }

        public Meeting(
            string name,
            DateTime startTime,
            DateTime endTime,
            int? remindForInMinutes = null)
        {
            if (startTime < DateTime.Now)
                throw new ArgumentException("Invalid startTime", nameof(startTime));

            if (endTime < DateTime.Now)
                throw new ArgumentException("Invalid endTime", nameof(endTime));

            if (endTime < startTime)
                throw new ArgumentException("EndTime must be greater than startTime");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            MeetingStartTime = MeetingTime.CreateFromDateTime(startTime);
            MeetingEndTime = MeetingTime.CreateFromDateTime(endTime);
            RemindForTime =
                remindForInMinutes != null 
                    ? MeetingStartTime.Time.AddMinutes(-remindForInMinutes.Value)
                    : default(TimeOnly?);
        }

        public bool IsIntersect(Meeting meeting)
        {
            if (this.Equals(meeting)) return true;

            return InnerSpan() || LeftSpan() || RightSpan() || OuterSpan();

            bool InnerSpan()
            {
                return
                    (this.MeetingStartTime >= meeting.MeetingStartTime
                        && this.MeetingEndTime >= meeting.MeetingEndTime);
            }

            bool LeftSpan()
            {
                return
                    (this.MeetingStartTime <= meeting.MeetingStartTime
                        && this.MeetingEndTime >= meeting.MeetingEndTime);
            }

            bool RightSpan()
            {
                return
                    (this.MeetingStartTime >= meeting.MeetingStartTime
                        && this.MeetingEndTime <= meeting.MeetingEndTime);
            }

            bool OuterSpan()
            {
                return
                    (this.MeetingStartTime <= meeting.MeetingStartTime
                        && this.MeetingEndTime <= meeting.MeetingEndTime);
            }
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return MeetingStartTime;
            yield return MeetingEndTime;
        }

        public override string ToString()
        {
            return $"Встреча {Name}" +
                $"Дата {MeetingStartTime.Date.ToShortDateString()} " +
                $"Время {MeetingStartTime.Time.ToShortTimeString()} - {MeetingEndTime.Time.ToShortTimeString()}";
        }
    }
}
