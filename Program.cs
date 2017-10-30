using System;

namespace Monad
{

    class Maybe<T>
    {
        public T Value { get; }

        public Maybe() { }
        public Maybe(T instance)
        {
            this.Value = instance != null ? instance : throw new ArgumentNullException(nameof(instance));
        }

        public Maybe<TO> Bind<TO>(Func<T, Maybe<TO>> func) where TO : class
        {
            return Value != null ? func(Value) : Maybe<TO>.None();
        }

        public static Maybe<T> None() => new Maybe<T>();

    }

    static class MonadExtensions
    {
        public static Maybe<T> Return<T>(this T instance) where T : class
        {
            return instance != null ? new Maybe<T>(instance) : Maybe<T>.None();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Maybe<int> getInt() => new Maybe<int>(100);
            Maybe<string> getString() => new Maybe<string>("hello");
            Maybe<string> getNoString() => new Maybe<string>();
            Maybe<string> getSomeString() => new Maybe<string>("world");

            var rs = getInt()
                .Bind(x => getString())
                .Bind(x => getNoString())
                .Bind(x => getSomeString());

            Console.WriteLine(rs.Value == null);
        }
    }
}
