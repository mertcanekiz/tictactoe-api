using System.Collections.Generic;
using TicTacToe.Domain.Common;

namespace TicTacToe.Domain.ValueObjects
{
    public class Position : ValueObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}