using System;
using System.Linq;
using System.Reflection;

namespace DefaultAppDomainApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("***** Fun with the default app domain *****\n");
            AppDomain defaultAppDomain = AppDomain.CurrentDomain;

            InitializeDomain(defaultAppDomain);
            DisplayStats(defaultAppDomain);
            ListAllAssembliesInAppDomain(defaultAppDomain);
            Console.ReadLine();
        }

        private static void DisplayStats(AppDomain defaultAD)
        {
            // Print out various stats about this domain.
            Console.WriteLine($"Name of this domain: {defaultAD.FriendlyName}");
            Console.WriteLine($"ID of domain in this process: {defaultAD.Id}");
            Console.WriteLine($"Is this the default domain?: {defaultAD.IsDefaultAppDomain()}");
            Console.WriteLine($"Base directory of this domain: {defaultAD.BaseDirectory}");
        }

        static void ListAllAssembliesInAppDomain(AppDomain defaultAD)
        {
            // Now get all loaded assemblies in the default app domain. 
            var loadedAssemblies = from a in defaultAD.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;

            Console.WriteLine($"***** Here are the assemblies loaded in {defaultAD.FriendlyName} *****\n");
            foreach (Assembly a in loadedAssemblies)
            {
                AssemblyName assemblyName = a.GetName();
                Console.WriteLine($"-> Name: {assemblyName.Name}");
                Console.WriteLine($"-> Version: {assemblyName.Version}\n");
            }
        }

        static void InitializeDomain(AppDomain defaultAppDomain)
        {
            // This logic will print out the name of any assembly
            // loaded into the application domain, after it has been
            // created. 
            defaultAppDomain.AssemblyLoad += (o, s) =>
                Console.WriteLine($"{s.LoadedAssembly.GetName().Name} has been loaded!");
        }
    }
}