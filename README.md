![Logo of the project](https://raw.githubusercontent.com/blackbasilisk/clubmanager-tasmota-accesscontrol/master/SimplySwitchLogo.png)
# Simply Switch SDK
> Just switch it

.NET Core library to manage communication with Simply Switch device

## Getting started

1. Add a reference to the SDK library in your project
2. Create new instance
3. Assign values to SerialPort and SerialBaudrate properties
4. Call SConnect()
5. Call SActivate()
...

```csharp
SimplySwitch simplySwitch = new SimplySwitch();
simplySwitch.SerialPort = "COM5";
simplySwitch.SerialBaudRate = 115200;

simplySwitch.SConnect();
simplySwitch.SActivate();

simplySwitch.SDisconnect();
```


## Developing

Make sure you add the following Nuget packages. They will add their own dependencies if you do it this way:
SerialPortLib v1.1.1:  https://github.com/genielabs/serialport-lib-dotnet/

The library has an internal processing queue to process the commands, which means when connecting and disconnecting, 
the log event might still report serial debugging information. This is normal. 

ALWAYS call the Dispose() method when closing down to ensure that the internal objects and threads are cleaned up properly. 

## Simple sample project

You can find a very simpe console application showing the basic process [here](https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/tree/master/.NET%20Core/Sample/SM.ClubManager.AccessControl.SDK.Sample)

Here is an extract of the program for reference:
```csharp
        Console.WriteLine("Create new instance of SimplySwitch");
        SimplySwitch sw = new SimplySwitch();
        sw.SerialBaudRate = 115200;
        sw.SerialPort = "COM5";

        sw.OnConnected += Sw_OnConnected;
        sw.OnDisconnected += Sw_OnDisconnected;
        sw.OnLogMessage += Sw_OnLogMessage;          

        sw.SConnect();
        Console.WriteLine("Activating the unit (closing relay)");            
        sw.SActivate();
        Console.WriteLine("Pausing 2 seconds...");
        Thread.Sleep(2000);
        Console.WriteLine("Deactivating the unit (opening relay)");            
        sw.SRelease();
        Console.WriteLine("Disconnecting... ");
        sw.SDisconnect();
        Console.WriteLine("Press any key to dispose object and exit");
        Console.ReadLine();
        sw.Dispose();
```

## Features

* Async command processing queue to ensure it doesn't lock up the main thread
* Handles serial connection interruptions gracefully and tries to reestabelish dropped connections as reasonable as possible
* When checking for the 'isDebug' property on log messages, you can distinguish between RAW serial responses and internal class log messages. This allows for more granular reporting and easier troubleshooting

## Configuration

There are some basic hardware requirements for the SimplySwitch module i.e. the BaudRate value always needs to be 115200. It remains a value that can be set in case the baud rate needs to be adjusted in the device firmware

#### SerialBaudRate
Type: `int`  
Default: `115200`

#### SerialPort
Type: `string`  
Default: `empty`

Example:
```csharp
SimplySwitch simplySwitch = new SimplySwitch();
simplySwitch.SerialPort = "COM5";
simplySwitch.SerialBaudRate = 115200;
```

## Contributing

If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome.

## Links

**MASSIVE THANK YOU** TO Arendst and all his efforts with the [Tasmota firmware](https://github.com/arendst/Tasmota).
Without him this would not be possible!

Also, massive thank you to Generoso Martello for his [SerialPortLib](https://github.com/genielabs/serialport-lib-dotnet) library.

#### Project links
- Project homepage: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/
- Repository: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol
- Issue tracker: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/issues
- In case of sensitive bugs like security vulnerabilities, please create an issue stating it such and the author will get in touch ASAP. We value any efforts to help with security issues
  
## Licensing

This is open source software, licensed under the terms of Apache License 2.0. See the [LICENSE](https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/blob/main/LICENSE.md) file for details.
