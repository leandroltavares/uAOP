using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.InterceptionExtension.Configuration;

namespace uAOP.Core
{
    public static class AspectConfiguration
    {
        private static List<Type> aspectsTypes;

        private static IUnityContainer container;

        public static void Setup(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            aspectsTypes =  new List<Type>();

            foreach (var asm in assemblies)
            {
                aspectsTypes.AddRange(asm.GetTypes().Where(t => typeof(IAspectAttribute).IsAssignableFrom(t) && t.IsClass));
            }

            AspectConfiguration.container = container;

            //var interceptionConfiguration = container.Configure<Interception>();

            //foreach (var asm in assemblies)
            //{
            //    foreach (var type in asm.GetTypes())
            //    {
            //        foreach (var method in type.GetMethods())
            //        {
            //            foreach (var aspectType in aspectsTypes)
            //            {
            //                if (method.IsDefined(aspectType, false))
            //                {
            //                    interceptionConfiguration.AddPolicy("xpto").AddMatchingRule(
            //                        new CustomAttributeMatchingRule(aspectType, false));

            //                }
            //            }
            //        }                 
            //    }
            //}
        }

        public static void RegisterType<T>()
        {
            List<InjectionMember> injectionMembers = new List<InjectionMember>();

            injectionMembers.Add(new Interceptor<VirtualMethodInterceptor>());

            foreach (var method in typeof(T).GetMethods())
            {
                foreach (var aspectType in aspectsTypes)
                {
                    if (method.IsDefined(aspectType, false))
                    {
                        injectionMembers.Add(new InterceptionBehavior(aspectType));
                    }

                }
            }
                
            container.RegisterType<T>(injectionMembers.ToArray());
        }
    }
}
