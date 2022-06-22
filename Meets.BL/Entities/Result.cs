using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
