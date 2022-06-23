namespace Meets.UI.Helpers
{
    public class InputValidator<T>
        where T: struct
    {
        public T Value { get; }

        public bool SuccessConverted { get; }

        public InputValidator(T value, bool success)
        {
            Value = value;
            SuccessConverted = success;
        }

        public static InputValidator<T> Ok(T value) => new InputValidator<T>(value, true);

        public static InputValidator<T> Fail() => new InputValidator<T>(default, false);


    }

    public static class InputValidatorExtensions
    {
        public static InputValidator<int> ToInputInt(this string str)
        {
            var result = int.TryParse(str, out var number);

            return result ? InputValidator<int>.Ok(number) : InputValidator<int>.Fail();
        }
    }
}
