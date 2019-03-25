/*
* AtmegaPlugIR.c
*
* Created: 19-Sep-17 10:15:28
* Author : yahor.halubchyk
v1.0 - release
v1.1 - fix bug for other codes: byteCode[0] may be 0, but receiving successfull. Therefore returned numbit or index of byte array (token for receiving successfull)
v1.2 - fix bug: clear byteCode[last+i] after receiving
v1.3 - added powerOn by signal (not full without testing
v1.4 - deprecated last changes and fixed v1.3 bugs
*/
#define F_CPU 8000000

#include <avr/io.h>
#include "extensions.h"
//todo #include <avr/eeprom.h>
//#include <string.h>

//uart
#define UHARD_BAUD 38400
#define UHARD_RXEN false
#define UHARD_BUFFER_EN false
#include "uart_hard.h"

//#define outIR PORTD.5
#define IO_OutPC D, 6
#define IO_TSOP D, 2
#define IO_InPowOn B, 7

#define inTSOP io_getPin(IO_TSOP)
#define inPowOn io_getPin(IO_InPowOn)

#define setTime() TCNT1H=0xFFB0 >> 8; TCNT1L=0xFFB0 & 0xff;
#define startTimer() TCCR1B=(0<<CS12) | (0<<CS11) | (1<<CS10); setTime();
#define stopTimer()	TCCR1B=0;

#define timeValue 10 //10 us
#define timeWait 10*1000/10 //10 ms
#define timePrecision 10 // time from period of 1 bit

volatile unsigned int counter;
#define bufferSize 10
unsigned char byteCode[bufferSize];

//todo uint8_t EEMEM _e_powerCode[bufferSize];
//todo uint8_t powerCode[bufferSize];

char WaitTSOP(unsigned char pinState)
{
	while ((pinState && !inTSOP) || (!pinState && inTSOP))
	{
		if (counter >= timeWait)
		return 0;
	}
	return 1;
}

uint8_t cntIrCode;
char getIRcode()
{
	unsigned char i = 0;
	unsigned char numBit = 0;
	unsigned int delay0 = 0;
	unsigned int delay1 = 0;
	unsigned int delay_prec = 0;
	//all clear
	byteCode[0] = 0;

	counter = 0;
	while (inTSOP); //wait first bit
	startTimer();

	while(1)  //get all bits
	{
		if (!WaitTSOP(1)) { return i;}//byteCode[0]; //it's may be send error
		delay0 = counter;
		counter = 0;

		if (!WaitTSOP(0)) {return i;} //if byteCode != 0 then successfull (it's may be end)
		delay1 = counter;
		counter = 0;

		delay_prec = (delay0 + delay1) / timePrecision;
		if (delay0 + delay_prec < delay1) //this is 1
		{
			byteCode[i] |= 1 << numBit;
		}

		++numBit;
		if (numBit == 8) //next index of array;
		{
			numBit = 0;
			++i;
			byteCode[i] = 0;
			if (i >= bufferSize) {return i;} //success but isn't properly because data > buffer size
		}
	}
}

#define startByte '>'
#define endByte 0x0D
#define endByte2 0x0A
void sendCode()
{
	uhard_putCharf(startByte);
	for (uint8_t i = 0; i <= cntIrCode; ++i)
	{
		uhard_putChar(byteCode[i]);
	}
	uhard_putCharf(endByte);
	uhard_putCharf(endByte2);
}

//todo bool isPowerCode()
//{
//if (cntIrCode > bufferSize)
//return false;
//
//for (uint8_t i = 0; i < cntIrCode; ++i)
//{
//if (byteCode[i] != powerCode[i]) //todo because if byteCode not full we can have the error
//{
//return false;
//}
//}
//return true;
//}

//void uart_fix_init(void)
//{
//#if F_CPU < 2000000UL && defined(U2X)
////UCSRA = _BV(U2X);             /* improve baud rate error by using 2x clk */
//UBRRL = (F_CPU / (8UL * UHARD_BAUD)) - 1;
//#else
//UBRRL = (F_CPU / (16UL * UHARD_BAUD)) - 1;
//#endif
//UCSRB = _BV(TXEN) | _BV(RXEN); /* tx/rx enable */
//}
int main(void)
{
	//todo eeprom_read_block((void*)&powerCode,(const void*)&_e_powerCode , bufferSize);


	DDRD=(0<<DDD7) | (0<<DDD6) | (0<<DDD5) | (0<<DDD4) | (0<<DDD3) | (0<<DDD2) | (0<<DDD1) | (0<<DDD0);
	// State: Bit7=T Bit6=0 Bit5=0 Bit4=T Bit3=T Bit2=P Bit1=T Bit0=T
	PORTD=(0<<PORTD7) | (0<<PORTD6) | (0<<PORTD5) | (0<<PORTD4) | (0<<PORTD3) | (1<<PORTD2) | (0<<PORTD1) | (0<<PORTD0);
	
	// Timer/Counter 1 initialization
	// Clock source: System Clock
	// Clock value: 8000,000 kHz
	// Timer Period: 0,01 ms
	// Timer1 Overflow Interrupt: On
	TCCR1A=(0<<COM1A1) | (0<<COM1A0) | (0<<COM1B1) | (0<<COM1B0) | (0<<WGM11) | (0<<WGM10);
	TCCR1B=(0<<ICNC1) | (0<<ICES1) | (0<<WGM13) | (0<<WGM12) | (0<<CS12) | (0<<CS11) | (1<<CS10);

	// Timer(s)/Counter(s) Interrupt(s) initialization
	TIMSK=(0<<OCIE2) | (0<<TOIE2) | (0<<TICIE1) | (0<<OCIE1A) | (0<<OCIE1B) | (1<<TOIE1) | (0<<TOIE0);

	sei();
	
	uhard_init();
	//uart_fix_init();
	uhard_putStringf(">>I'm IR");
	uhard_putCharf(endByte);
	uhard_putCharf(endByte2);


	while (1)
	{
		cntIrCode = getIRcode();
		if (cntIrCode)
		{
			stopTimer();
			sendCode();

			//todo if (inPowOn)
			//{
			//stopTimer();
			//sendCode();
			//}
			//else if (isPowerCode())
			//{
			//io_setPort(IO_OutPC);
			//for (uint8_t i = 0; i< 100; ++i)
			//{
			//if (inPowOn)
			//{
			//break;
			//}
			//_delay_ms(10);
			//}
			//io_resetPort(IO_OutPC);
			//}
		}
	}
}

ISR(TIMER1_OVF_vect) //0.01ms
{
	setTime();
	++counter;
}

uint8_t rxBuffer[bufferSize];
int8_t rxCounter = -1;

void rxClearBuffer()
{
	rxCounter = -1;
}

//todo void setPowerCode()
//{
//memcpy(rxBuffer, powerCode, rxCounter);
//rxBuffer[rxCounter] = 0; // set end in line;
//eeprom_write_block((const void*)&_e_powerCode , (void*)&powerCode, bufferSize);
//
//rxClearBuffer();
//}

//UHARD_ISR_newByte(c)
//{
////todo uint8_t b = c;
////if (rxCounter == -1) //wait start char
////{
////if (b == startByte)
////{
////++rxCounter;
////}
////return;
////}
////
////if (b == endByte)  //end line
////{
////if (rxCounter >= 3)
////{
////setPowerCode();
////}
////else
////{
////rxClearBuffer();
////}
////}
////else
////{
////rxBuffer[rxCounter] = b;
////++rxCounter;
////if (rxCounter == bufferSize) rxClearBuffer();
////}
//
//}