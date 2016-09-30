using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using uAOP.Core;
using uAOP.Core.Aspects;

namespace uAOP.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();

            AspectConfiguration.Setup(container);

            AspectConfiguration.RegisterType<SomeClass>();

            var someClass = container.Resolve<SomeClass>();

            Console.WriteLine(someClass.SomeMethod());

            Console.WriteLine(someClass.SomeMethod());


            Console.ReadLine();

        }

      
    }
}
