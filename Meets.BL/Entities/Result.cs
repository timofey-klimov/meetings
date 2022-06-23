namespace Meets.BL.Entities
{
    public class Result : ValueObject
    {
        public bool Success { get; }

        public string ReasonPhrase { get; }

        public Result(bool success, string reasonPhrase)
        {
            Success = success;
            ReasonPhrase = reasonPhrase;
        }

        public static Result Ok() => new Result(true, string.Empty);
        public static Result Failt(string reason) => new Result(false, reason);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Success;
            yield return ReasonPhrase;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result(bool success, string reason, T value)
            : base(success, reason)
        {
            Value = value;
        }

        public static Result<T> Ok(T value) => new Result<T>(true, string.Empty, value);
        public static Result<T> Fault(string reason) => new Result<T>(false, reason, default);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Success;
            yield return ReasonPhrase;
            yield return Value;
        }
    }
}
