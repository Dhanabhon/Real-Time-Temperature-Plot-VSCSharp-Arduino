# Real-Time-Temperature-Plot-VSCSharp-Arduino
โปรแกรม Plot Graph ค่าอุณหภูมิ ที่ได้จาก Temperature sensor โดยรับค่าจากบอร์ด Arduino ส่งผ่าน Serial Port พัฒนาโดยใช้ภาษา C#

โปรแกรมนี้วาดกราฟโดยใช้ไลบรารี่ ZedGraph สามารถดาวน์โหลดได้จาก http://zedgraph.sourceforge.net/samples.html 

*   *โปรแกรมบนฝั่งไมโครคอนโทรลเลอร์ ซึ่ง Project นี้ใช้บอร์ด Arduino UNO R3 สามารถเขียน Code ได้ดังนี้:**

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
