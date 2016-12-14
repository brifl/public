using System;

namespace Pi3BackgroundApp.Common
{
    internal class DeviceReadFailedException : Exception
    {
        public DeviceReadFailedException(string deviceName, string reason) : base($"Device read failed for '{deviceName}'. Reason: {reason}")
        {
        }
    }
}