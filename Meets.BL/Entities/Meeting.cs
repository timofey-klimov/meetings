namespace Meets.BL.Entities
{
    public class Meeting : ValueObject
    {
        public string Name { get; private set; }

        public MeetingTime MeetingStartTime { get; private set; }

        public MeetingTime MeetingEndTime { get; private set; }

        public Meeting(
            string name,
            DateTime startTime,
            DateTime endTime)
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
    }
}
