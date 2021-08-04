﻿using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject) obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
    }
}