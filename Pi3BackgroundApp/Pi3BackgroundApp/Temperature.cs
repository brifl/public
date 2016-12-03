namespace Pi3BackgroundApp
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
            return (c*(9f/5)) + 32;
        }

        private static float FtoC(float f)
        {
            return (f - 32) * (5f / 9);
        }
    }
}
