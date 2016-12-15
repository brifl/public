using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.I2c;
using Pi3BackgroundApp.Common;

namespace Pi3BackgroundApp.Devices
{
    internal class ArduinoI2C : IPollable<JsonObject>, IDevice
    {
        private const int BufferLength = 32;
        private const byte EmptyByte = byte.MaxValue;

        private IBoardResourceProvider<I2cDevice> I2CProvider { get; }
        private I2cDevice Arduino { get; set; }

        public string Name { get; }

        public ArduinoI2C(string name, IBoardResourceProvider<I2cDevice> i2CProvider)
        {
            Name = name;
            I2CProvider = i2CProvider;
        }

        public bool IsInitialized { get; set; }

        public async Task Initialize()
        {
            Arduino = await I2CProvider.GetAsync();
        }

        public JsonObject GetValue()
        {
            var json = ReadString(Arduino);
            
            return JsonObject.Parse(json);
        }

        private static string ReadString(I2cDevice arduino)
        {
            var readBuffer = new byte[BufferLength];
            arduino.Read(readBuffer);

            var charCount = 0;
            foreach (var b in readBuffer)
            {
                if (!b.Equals(EmptyByte))
                {
                    charCount++;
                }
            }

            if (charCount == 0)
            {
                return "{}";
            }

            var chars = Encoding.UTF8.GetString(readBuffer, 0, charCount).ToCharArray();

            return new string(chars);
        }
    }
}