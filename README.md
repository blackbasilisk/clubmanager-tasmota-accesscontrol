![Logo of the project](https://raw.githubusercontent.com/blackbasilisk/clubmanager-tasmota-accesscontrol/blob/dev/SimplySwitchLogo.png)
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
```
The above code will create a new instance of the SimplySwitch class, connect and activate the relay of the device

### Initial Configuration

Some projects require initial configuration (e.g. access tokens or keys, `npm i`).
This is the section where you would document those requirements.

## Developing

Here's a brief intro about what a developer must do in order to start developing
the project further:

```shell
git clone https://github.com/your/awesome-project.git
cd awesome-project/
packagemanager install
```

And state what happens step-by-step.

### Building

If your project needs some additional steps for the developer to build the
project after some code changes, state them here:

```shell
./configure
make
make install
```

Here again you should state what actually happens when the code above gets
executed.

### Deploying / Publishing

In case there's some step you have to take that publishes this project to a
server, this is the right time to state it.

```shell
packagemanager deploy awesome-project -s server.com -u username -p password
```

And again you'd need to tell what the previous code actually does.

## Features

What's all the bells and whistles this project can perform?
* What's the main functionality
* You can also do another thing
* If you get really randy, you can even do this

## Configuration

Here you should write what are all of the configurations a user can enter when
using the project.

#### Argument 1
Type: `String`  
Default: `'default value'`

State what an argument does and how you can use it. If needed, you can provide
an example below.

Example:
```bash
awesome-project "Some other value"  # Prints "You're nailing this readme!"
```

#### Argument 2
Type: `Number|Boolean`  
Default: 100

Copy-paste as many of these as you need.

## Contributing

When you publish something open source, one of the greatest motivations is that
anyone can just jump in and start contributing to your project.

These paragraphs are meant to welcome those kind souls to feel that they are
needed. You should state something like:

"If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are warmly welcome."

If there's anything else the developer needs to know (e.g. the code style
guide), you should link it here. If there's a lot of things to take into
consideration, it is common to separate this section to its own file called
`CONTRIBUTING.md` (or similar). If so, you should say that it exists here.

## Links

Even though this information can be found inside the project on machine-readable
format like in a .json file, it's good to include a summary of most useful
links to humans using your project. You can include links like:

- Project homepage: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/
- Repository: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol
- Issue tracker: https://github.com/blackbasilisk/clubmanager-tasmota-accesscontrol/issues
  - In case of sensitive bugs like security vulnerabilities, please create an issue stating such and the author will get in touch ASAP. We value your effort


## Licensing

One really important part: Give your project a proper license. Here you should
state what the license is and how to find the text version of the license.
Something like:

"The code in this project is licensed under MIT license."
