# StatsDisplay

[![GitHub version](https://img.shields.io/badge/version-v1.0.0-blue.svg)](https://github.com/narfel/StatsDisplay/blob/master/README.md)
[![License](https://img.shields.io/badge/license-MIT-green)](https://github.com/narfel/StatsDisplay/blob/master/LICENSE)

Windows application that shows system stats on a 16x2 display

## Description

> A small windows application that sends stats such as CPU temperature, memory usage, etc. to a 16x2 display over a serial connection. The accompanying firmware then receives these data and displays it on a generic 16x2 display.

## Screenshot
![image](images/screenshot.png?raw=true "Screenshot")

## How to use

* You need to have the client running on an Arduino or compatible boards driving the LCD. You can find the client in this accompanying [repository](https://github.com/narfel/StatsDisplay_client).
* Set the serial port and baud rate in the config tab and adjust the options to your preference. Port and baud rate must match the client.

* You can check the output by clicking “Show Output” and switching to the Output tab that appears.
* There is also a CPU temperature selection tab that can be shown with “Select CPU Temp” since temperature values and their respective names are different based on the CPU architecture.

## Dependencies

This program uses a library called LibreHardwareMonitorLib that is included in the repo.
It was built with and requires .NET 6.0 to run.

## Admin rights

* To show CPU temperatures, LibreHardwareMonitorLib requires admin rights, therefore the application needs admin access.
* This also means that the option “Run when Windows starts” is more complex. To allow for admin rights, a task scheduler task is created with a trigger to kick off on system startup.

## Acknowledgements

* This project is based on the idea of this [thingiverse](https://www.thingiverse.com/thing:6052915) project and their [code](https://github.com/Shift2Ace/PC-State-Display-Control-Panel/tree/master/PC%20State%20Display%20Control%20Panel) on github.

## License

Copyright (c) 2023 Narfel.

Usage is provided under the MIT License. See [LICENSE](https://github.com/narfel/StatsDisplay/blob/master/LICENSE) for the full details.
