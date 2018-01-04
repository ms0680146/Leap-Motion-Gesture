/* Example program for from IRLib – an Arduino library for infrared encoding and decoding
 * Version 1.5  June 2014
 * Copyright 2014 by Chris Young http://cyborg5.com
 * Based on original example sketch for IRremote library 
 * Version 0.11 September, 2009
 * Copyright 2009 Ken Shirriff
 * http://www.righto.com/
 */
/*
 * IRrecord: record and play back IR signals 
 * An IR detector/demodulator must be connected to the input RECV_PIN.
 * An IR LED must be connected to the appropriate pin.(See IRLibTimer.h) for your 
 * machine's timers and erupts.
 * Record a value by pointing your remote at the device then send any character
 * from the serial monitor to send the recorded value.
 * Also demonstrates how to use toggle bits which must be controlled outside
 * the library routines.
 * The logic is:
 * If an IR code is received, record it.
 * If a serial character is received, send the IR code.
 */

#include <IRLib.h>

int RECV_PIN = 11;

IRrecv My_Receiver(RECV_PIN);
IRdecode My_Decoder;
IRsend My_Sender;
/*
 * Because this version of the library separated the receiver from the decoder,
 * technically you would not need to "store" the code outside the decoder object
 * for this overly simple example. All of the details would remain in the object.
 * However we are going to go ahead and store them just to show you how.
 */
// Storage for the recorded code
IR_types_t codeType;          // The type of code
unsigned long codeValue;   // The data bits if type is not raw
int codeBits;              // The length of the code in bits

// These values are only stored if it's an unknown type and we are going to use
// raw codes to resend the information.
unsigned int rawCodes[RAWBUF]; // The durations if raw
unsigned int windLevelCode[] = {1200, 450, 1200, 450, 400, 1300, 1200, 500, 1200, 450, 350, 1350, 350, 1300, 400, 1300, 350, 1350, 350, 1300, 1200, 500, 350, 7850, 1200, 500, 1200, 450, 350, 1350, 1200, 450, 1200, 450, 400, 1300, 350, 1350, 350, 1300, 400, 1300, 350, 1350, 1200, 450, 350};
unsigned int lightCode[] = {1200, 500, 1200, 450, 350, 1350, 1200, 450, 1200, 450, 400, 1300, 350, 1350, 350, 1300, 400, 1300, 1200, 500, 350, 1300, 400, 7850, 1200, 450, 1200, 450, 400, 1300, 1200, 500, 1200, 450, 350, 1350, 350, 1300, 400, 1300, 350, 1350, 1200, 450, 350, 1350, 350};
unsigned int offCode[] = {1200, 500, 1150, 500, 350, 1350, 1200, 450, 1200, 450, 400, 1300, 350, 1350, 350, 1300, 1200, 500, 350, 1300, 400, 1300, 350, 7900, 1150, 500, 1200, 450, 400, 1300, 1200, 450, 1200, 500, 350, 1300, 400, 1300, 350, 1350, 1200, 450, 350, 1350, 350, 1300, 400};

bool GotOne, GotNew; 

void setup()
{
  GotOne=false; GotNew=false;
  codeType=UNKNOWN; 
  codeValue=0; 
  Serial.begin(9600);
  delay(2000);while(!Serial);
}

void loop() {
  int Msg = -1;
  if ((Msg=Serial.read()) != -1) {
    switch(Msg)
    {
      case 0x00:
        My_Sender.IRsendRaw::send(windLevelCode, 47, 37);
        break;
       case 0x01:
        My_Sender.IRsendRaw::send(lightCode, 47, 37);
        break;
       case 0x02:
        My_Sender.IRsendRaw::send(offCode, 47, 37);
        break;
       default:
        Serial.println("Wrong msg from host!");
    }
  } 
}

