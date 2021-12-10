using System.Reflection;

namespace FirstTry_app_1
{
    internal class ResourceManager
    {
        private string v;
        private Assembly assembly;

        public ResourceManager(string v, Assembly assembly)
        {
            this.v = v;
            this.assembly = assembly;
        }
    }
}