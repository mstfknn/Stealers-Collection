#LastPassRecovery

#### Info

Imagine you set up a [LastPass](https://www.lastpass.com) account, created a fairly complicated password, installed LastPass extension, 
saved the password for convenience (bad idea) and a week later you realized that you no longer can recall your master password.

This happened to a friend of mine, whom I introduced to LastPass. We tried to reset their password but it did not work.

As a long time LastPass user I knew we had two choices - either crack the password with oclHashcat or try to retrieve and decrypt it from the Firefox.

Fortunately, [Martin Vigo](http://www.martinvigo.com/a-look-into-lastpass/) did all the hard work and by analysing the [Metasploit](http://www.metasploit.com) plugin he authored as well as reading the [hashcat forum](http://hashcat.net/forum/) I quickly knocked off a small tool which tries to decrypt passwords stored in Firefox profiles.

#### Requirements
* VS 2013/2015 (Community Edition works fine)
* Firefox 
* LastPass plugin installed
* Master password saved in the plugin
* (optionally) forgotten master password - if you still remember your password you are welcome to try this tool and see if it is work ;)

#### Usage
Just compile and run. If a password is found, it will be written to the console. Make sure no one is snooping on you ;)

Please note - you have to run the program on **the very same PC** and under **the very same account** you were using when the password was saved, as the LastPass plugin utilizes [Protected Data](https://msdn.microsoft.com/en-us/library/system.security.cryptography.protecteddata(v=vs.110).aspx) storage.

#### FAQ (short)
Q: Can I retrieve my master password if I have not saved it on my PC?

A: Nope. 


Q: Can I retrieve my password from Chrome, Safari, ~~Internet Explorer~~ ~~Spartan~~ Edge, Opera?

A: Nope. But you can try to improve it - check the metasploit plugin mentioned earlier. Pull requests are welcome ;)


Q: Does it work on Linux/OSX/iPhone/Android/ZX81?

A: Nope.

##### Disclaimer and legal boring stuff
I am not affiliated with LastPass, but I am a happy user.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>