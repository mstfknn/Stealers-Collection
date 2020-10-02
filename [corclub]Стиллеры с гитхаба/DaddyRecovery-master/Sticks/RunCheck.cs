namespace DaddyRecovery.Sticks
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class RunCheck
    {
        public static bool InstanceCheck()
        {
            var attribute = (GuidAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            new Mutex(true, attribute.Value, out bool isNew); return isNew;
        }
    }
}