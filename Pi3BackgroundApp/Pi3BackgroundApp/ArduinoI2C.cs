using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.I2c;

namespace Pi3BackgroundApp
{
    internal class ArduinoI2C : IPollable<JsonObject>, IDevice
    {
        private const int BufferLength = 32;
        private const byte EmptyByte = new byte();

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

            var charCount = readBuffer.TakeWhile(b => !b.Equals(EmptyByte)).Count();

            if (charCount == 0)
            {
                return "{}";
            }

            var chars = Encoding.UTF8.GetString(readBuffer, 0, charCount).ToCharArray();

            return new string(chars);
        }
    }
}