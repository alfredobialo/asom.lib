using System;

namespace asom.lib.core.util
{
    public struct Money : IEquatable<Money>
    {
        public bool Equals(Money other)
        {
            return other.Currency == Currency  && other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Money other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.CurrencyEqual(b))
            {
                return new Money(a.Value + b.Value, a.Currency);
            }

            return Money.INVALID;
        }

        public bool CurrencyEqual(Money b) =>
            Currency == b.Currency;
        public  Money Add(decimal b)
        {
            Value += b;
            return this;
        }
        public  Money Subtract(decimal b)
        {
            Value -= b;
            return this;
        }
        public  Money Multiply(decimal b)
        {
            Value *= b;
            return this;
        }

        public static Money operator ++(Money a)
        {
            return new Money(a.Value + 1, a.Currency);
        }
        public static Money operator --(Money a)
        {
            return new Money(a.Value - 1, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.CurrencyEqual(b))
            {
                return new Money(a.Value - b.Value, a.Currency);
            }

            return Money.INVALID;
        }

        public static Money operator *(Money a, Money b)
        {
            if (a.CurrencyEqual(b))
            {
                return new Money(a.Value * b.Value, a.Currency);
            }

            return Money.INVALID;
        }

        public static Money operator /(Money a, Money b)
        {
            if (b.Value == 0) return Money.INVALID;

            if (a.CurrencyEqual(b))
            {
                return new Money(a.Value / b.Value, a.Currency);
            }

            return Money.INVALID;
        }

        public static Money INVALID = new Money(0, "");

        public Money(decimal value, string currency)
        {
            Value = value;
            Currency = currency.ToUpper();
        }

        public string Currency { get; private set; }
        public decimal Value { get; private set; }

        public override string ToString()
        {
            return $"{Value:N} {Currency?.ToUpper()}";
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
    }

    public static class MoneyExtension
    {
        public static Money In(this decimal value, string currency)
        {
            return new Money(value, currency);
        }
    }
}
