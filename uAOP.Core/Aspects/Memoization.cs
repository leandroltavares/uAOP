using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using uAOP.Core.Helpers;

namespace uAOP.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class Memoization : Attribute, IAspectAttribute, IInterceptionBehavior
    {
        private readonly MemoryCache cache;

        public int Priority { get; set; }

        public Memoization()
        {
            this.Priority = int.MaxValue;
            this.cache = new MemoryCache("Memoization");
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var stringBuilder = new StringBuilder();
            AppendCallInformation(input, stringBuilder);
            var cacheKey = stringBuilder.ToString();

            var storedResult = cache.Get(cacheKey);

            if (storedResult == null)
            {
                var result = getNext()(input, getNext);

                cache[cacheKey] = result.ReturnValue;

                return result;

            }

            return input.CreateMethodReturn(storedResult);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

        private static void AppendCallInformation(IMethodInvocation args, StringBuilder stringBuilder)
        {
            var declaringType = args.MethodBase.DeclaringType;
            NameHelper.AppendTypeName(stringBuilder, declaringType);
            stringBuilder.Append('.');
            stringBuilder.Append(args.MethodBase.Name);

            if (args.MethodBase.IsGenericMethod)
            {
                var genericArguments = args.MethodBase.GetGenericArguments();
                NameHelper.AppendGenericArguments(stringBuilder, genericArguments);
            }

            NameHelper.AppendArguments(stringBuilder, args.Arguments);
        }

        
    }
}
