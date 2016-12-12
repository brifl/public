#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>
#include <Wire.h>

/*
 * Connects with PI on I2C
 * Sends data from DHT11 temp/humidity sensor
 */

#define I2C_SLAVE_ADDRESS 0x40
#define BUFFER_LENGTH 16
#define DHTTYPE DHT11
#define PIN_DHT11 2

DHT_Unified dht(PIN_DHT11, DHTTYPE);

uint32_t delayMS;
volatile float temperature;
volatile float humidity;
volatile bool lastReadSuccessful = false;

void setup()
{  
	Serial.begin(9600);
	initSensors();
	initI2C();
}

void initSensors()
{	
	dht.begin();
	sensor_t sensor;
	dht.temperature().getSensor(&sensor);
	delayMS = sensor.min_delay / 1000;	
}

void initI2C()
{
	Wire.begin(I2C_SLAVE_ADDRESS);
	Wire.onRequest(sendReport);
}

void loop()
{
	delay(delayMS);
	setTempAndHumidity();
}

void setTempAndHumidity()
{
	sensors_event_t event;  
	dht.temperature().getEvent(&event);

	if (isnan(event.temperature)) 
	{
		Serial.println("Error reading temperature");
		lastReadSuccessful = false;
	}
	else
	{
		Serial.print("Temperature: ");
		Serial.print(event.temperature);
		Serial.println(" *C");
		temperature = event.temperature;
		lastReadSuccessful = true;
	}

	dht.humidity().getEvent(&event);
	
	if (isnan(event.relative_humidity))
	{
		Serial.println("Error reading humidity");
		lastReadSuccessful = false;
	}
	else 
	{
		Serial.print("Humidity: ");
		Serial.print(event.relative_humidity);
		Serial.println("%");
		humidity = event.relative_humidity;
		lastReadSuccessful = true;
	}
}

void sendReport()
{
    String json = "{\"t\":";
    json += formatFloat(temperature);
    json += ", \"h\":";
    json += formatFloat(humidity);
    json += "}";
    Serial.println(json);
    Wire.write(json.c_str());
}

String formatFloat(float f)
{
    char chars[5];
    dtostrf(f, 3, 1, chars);
    return chars;
}
