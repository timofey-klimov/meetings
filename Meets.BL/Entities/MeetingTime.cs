namespace Meets.BL.Entities
{
    public class MeetingTime : ValueObject
    {
        public DateOnly Date { get; private set; }
        public TimeOnly Time { get; private set; }

        public MeetingTime(DateOnly date, TimeOnly time)
        {
            if (date == default)
                throw new ArgumentException("Invalid date");

            if (time == default)
                throw new ArgumentException("Invalid time");

            Date = date;
            Time = time;
        }

        public static MeetingTime CreateFromDateTime(DateTime dateTime)
        {
            if (dateTime == default(DateTime))
                throw new ArgumentException("Invalid dateTime");

            return new MeetingTime(DateOnly.FromDateTime(dateTime), TimeOnly.FromDateTime(dateTime));
        }

        public static bool operator >=(MeetingTime a, MeetingTime b)
        {
            if (a.Equals(b))
                return true;

            if (a.Date == b.Date)
                return a.Time > b.Time;

            return a.Date > b.Date;
        }

        public static bool operator <=(MeetingTime a, MeetingTime b)
        {
            if (a.Equals(b))
                return true;

            return !(a >= b);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
            yield return Time;
        }

        public string GetDateToShortString() => Date.ToShortDateString();

        public string GetTimeToShortlString() => Time.ToShortTimeString();

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} {Time.ToShortTimeString()}";
        }
    }
}
