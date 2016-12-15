using System;

namespace Pi3BackgroundApp.Devices
{
    internal class Temperature
    {
        public float DegreesFarenheight
        {
            get { return CtoF(DegreesCelsius); }
            set { DegreesCelsius = FtoC(value); }
        }

        public float DegreesCelsius { get; set; }

        private static float CtoF(float c)
        {
            return Round((c*(9f/5)) + 32);
        }

        private static float Round(float val)
        {
            return (float)Math.Round(val, 2);
        }

        private static float FtoC(float f)
        {
            return Round((f - 32) * (5f / 9));
        }
    }
}
