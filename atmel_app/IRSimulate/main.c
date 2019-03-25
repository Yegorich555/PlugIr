/*
* IRSimulate.c
*
* Created: 19-Sep-17 11:56:57
* Author : yahor.halubchyk
*/
#define F_CPU 8L*1000L*1000L
#include <avr/io.h>
#include <util/delay.h>

#define SetPortBit(port,bit) port|=_BV(bit)
#define SetOut() SetPortBit(PORTD, 0)
#define ResetOut() ResetPortBit(PORTD, 0)

#define ResetPortBit(port,bit) port&=~_BV(bit)
#define TogglePortBit(port,bit) port^=_BV(bit)

void delay_ms(unsigned int delay)
{
	unsigned int i;
	for (i=0;i<delay;++i)
	_delay_ms(1);
}

void setSamsung(char b)
{
	ResetOut();
	_delay_ms(0.7);
	SetOut();
	if (b) _delay_ms(1.56);
	else _delay_ms(0.44);
}

void SendSamsungCode()
{
	ResetOut();
	_delay_ms(4.6);
	SetOut();
	_delay_ms(4.6);

	//address 1
	setSamsung(1);
	setSamsung(1);
	setSamsung(1);

	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	//address 2	= address 1
	setSamsung(1);
	setSamsung(1);
	setSamsung(1);

	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	setSamsung(0);
	//command
	setSamsung(0);
	setSamsung(1);
	setSamsung(0);
	setSamsung(0);
	setSamsung(0);

	setSamsung(1);
	setSamsung(1);
	setSamsung(0);
	setSamsung(1);
	setSamsung(0);

	setSamsung(1);
	setSamsung(1);
	setSamsung(1);
	setSamsung(0);
	setSamsung(0);
	setSamsung(1);

	//End
	ResetOut();
	_delay_ms(0.44);
	SetOut();
}

void setDvd(char b)
{
	ResetOut();
	_delay_ms(0.642);
	SetOut();
	if (b) _delay_ms(1.6);
	else _delay_ms(0.544);
}

void SendOtherDVDCode()
{
	ResetOut();
	_delay_ms(9);
	SetOut();
	_delay_ms(4.4);

	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);

	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);

	setDvd(0);
	setDvd(0);
	setDvd(0);
	setDvd(0);

	setDvd(1);

	setDvd(0);
	setDvd(0);
	setDvd(0);
	
	setDvd(1);
	setDvd(1);
	setDvd(1);
	setDvd(1);

	setDvd(0);
	setDvd(1);
	setDvd(1);
	setDvd(1);

	ResetOut();
	_delay_ms(0.544);
	SetOut();
}

int main(void)
{
	DDRD=(1<<DDD0);
	PORTD=(1<<PORTD0);

	while (1)
	{
		delay_ms(100);
		//start	Samsung IR code
		//SendSamsungCode()
		SendOtherDVDCode();
	}
}

