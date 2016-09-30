using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using uAOP.Core.Interfaces;

namespace uAOP.Core.Aspects
{
    public sealed class Logging : Attribute, IAspectAttribute, IInterceptionBehavior
    {
        private readonly ILogger logger;

        public int Priority { get; }

        public Logging(ILogger logger, int priority = int.MaxValue)
        {
            this.Priority = priority;
            this.logger = logger;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            logger.Log($"[{DateTime.Now}] - Method called: {input.MethodBase}");
            logger.Log($"[{DateTime.Now}] - Method arguments: {input.Arguments}");

            var result = getNext()(input, getNext);

            if (result.Exception != null)
            {
                logger.Log($"[{DateTime.Now}] - Method returned: {result.ReturnValue}");
            }
            else
            {
                logger.Log($"[{DateTime.Now}] - Method threw a Exception: {result.Exception.Message}");
                logger.Log($"[{DateTime.Now}] - StackTrace: {result.Exception.StackTrace}");
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

    }
}
