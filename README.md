# Local WLAN v1.0 for Windows
<img align="right" src="https://github.com/AlexIII/lwf/blob/master/lwf/lwf/lwf.png" />

### A windows tool that allows your PC to become a Wi-Fi access point when there's no active network connections.

**Note: The program should be run as an administrator.** Otherwise it'll fail to setup access point.

Windows 10 allows you to set up an access point via default network manager but only when there's another active network connection present. (e.g. wired) It makes sense since the AP is usually used to share internet connection via wi-fi to your other devices.
However, local wi-fi network can be useful even when there's no internet. That's when this util comes in handy.
My primary use case is when I want to transfer files between my phone and my laptop without presence of internet connection, but the util may also be useful for network development and testing.
The program is essentially a small GUI over `netsh`.

#### Target platform
- Tested in Windows 10

#### Dependencies
- .NET 4.0 (comes with Windows 10)
- `netsh` command-line tool (comes with Windows 10)

#### Screencaps
![alt text](https://github.com/AlexIII/lwf/blob/master/sc1.png)

License
[WTFPL v2](http://www.wtfpl.net)

Created with Microsoft Studio 2015