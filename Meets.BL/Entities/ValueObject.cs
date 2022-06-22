namespace Meets.BL.Entities
{
    public abstract class ValueObject
    {
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, component) =>
                {
                    unchecked
                    {
                        return (current * 23) + component?.GetHashCode() ?? 0;
                    }
                });
        }

        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is ValueObject valueObject)
            {
                return Enumerable.SequenceEqual(GetEqualityComponents(), valueObject.GetEqualityComponents());
            }

            return false;
        }
    }
}
