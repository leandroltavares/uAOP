
using uAOP.Core.Aspects;

namespace uAOP.Sample
{
    public class SomeClass
    {
        [Memoization]
        public virtual int SomeMethod()
        {
            return 42;
        }

        public virtual string SomeAnotherMethod(string message)
        {
            return "Hello World! " + message;
        }

        [Memoization]
        public virtual int Fibonacci(int position)
        {
            if (position <= 1)
                return 1;

            return Fibonacci(position - 1) + Fibonacci(position - 2);

        }
    }
}
