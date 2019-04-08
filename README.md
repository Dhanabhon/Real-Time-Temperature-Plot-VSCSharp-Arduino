# Real-Time-Temperature-Plot-VSCSharp-Arduino
Example Real-time temperature graph between C#.NET and Arduino

Using ZedGraph Library (http://zedgraph.sourceforge.net/samples.html)

*   *Arduino UNO R3 Code:**

        #include <math.h>
        
        #define RESISTOR_CONNECT_THERMISTOR	10000
        
        void setup() {
          Serial.begin(9600);
        }
        
        void loop() {
          Serial.println((Temperature(analogRead(0))));  // display Celsius
          delay(1000);
        }
        
        /* Calculate the temperature according to the following formula. (Steinhart-Hart Equation)
           https://learn.adafruit.com/thermistor/using-a-thermistor
        */
        
        float Temperature(int RawADC) {
          float temperature,resistance;
          // Calculate the resistance of thethermistor
          resistance   = (float)(1023-RawADC)*RESISTOR_CONNECT_THERMISTOR/RawADC;
          int B = 3975;
          temperature  = 1/(log(resistance/RESISTOR_CONNECT_THERMISTOR)/B+1/298.15)-273.15;
          return temperature;
        }
