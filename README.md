![Logo of the project](https://raw.githubusercontent.com/blackbasilisk/clubmanager-tasmota-accesscontrol/dev/SimplySwitchLogo.png)
# Simply Switch SDK
> Just switch it

.NET Core library to manage communication with Simply Switch device

## Installing / Getting started

1. Add a reference to the library in your project
2. Create new instance
3. Assign values to SerialPort and SerialBaudrate properties
4. Call SConnect()
5. Call SOpen()

```csharp
SimplySwitch simplySwitch = new SimplySwitch();
simplySwitch.SerialPort = "COM5";
simplySwitch.SerialBaudRate = 115200;

simplySwitch.SConnect();
simplySwitch.SActivate();

simplySwitch.SDisconnect();

//remember to call the Dispose method on the instance to allow the library to clean up background processes and serial connections
simplySwitch.Dispose();
```
The above code will create a new instance of the SimplySwitch class, connect, activate the relay of the device, disconnect and dispose of the object instance
