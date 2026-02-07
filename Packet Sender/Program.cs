using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Remote_Packet_Editor_Client
{
    class Program
    {
        public static double GetStringSizeInKilobytes(string data)
        {
            if (data == null)
                return 0;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            double kilobytes = bytes.Length / 1024.0;
            return Math.Round(kilobytes, 1);
        }
        static void Main(string[] args)
        {
            bool attack = false;
            bool ttsvalid = true;
            bool delayvalid = true;
            Console.Title = "Packet Sender - Made By yk3thn";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetWindowSize(158, Console.WindowHeight);
            Console.WriteLine("---PACKET SENDER---"); Thread.Sleep(500);
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type:");
            Console.WriteLine("[1] UDP");
            Console.WriteLine("[2] TCP");
            Console.WriteLine("");
            Console.Write(">");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string selection = Console.ReadLine();
            string type = "";
            if (selection == "1")
            {
                type = "UDP";
            }
            else if (selection == "2")
            {
                type = "TCP";
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Port: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sport = Console.ReadLine();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Delay (ms): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sdelay = Console.ReadLine();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Times to Send (max 100): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string stts = Console.ReadLine();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("IP: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string ip = Console.ReadLine();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Data to send: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sdata = Console.ReadLine();
            Int32.TryParse(sport, out int port);
            Int32.TryParse(sdelay, out int delay);
            Int32.TryParse(stts, out int TTS);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Settings Gathered!");
            Console.WriteLine("");
            double sizeinKB = GetStringSizeInKilobytes(sdata);
            var stopwatch = new Stopwatch();
            if (type == "UDP")
            {
                byte[] packetData = System.Text.ASCIIEncoding.ASCII.GetBytes(sdata);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                for (int i = 0; i <= TTS; i++)
                {
                    client.SendTo(packetData, ep);
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Sent packetData ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("'");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(sdata);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("'");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Sent To: " + ip);
                    Console.WriteLine("");
                    Console.WriteLine("Total Sent: " + i);
                    Console.WriteLine("");
                    Console.WriteLine("Total KB Sent: " + i * sizeinKB);
                    Thread.Sleep(delay);
                    Console.Clear();
                }
                attack = false;

                if (ttsvalid == true)
                {
                    if (delayvalid == true)
                    {
                        //disabled for now
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: '" + delay + "' is OVER the maximum amount (250)");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: '" + TTS + "' is OVER the maximum amount (100)");
                    Console.ReadKey();
                }
            }
            else if (type == "TCP")
            {
                byte[] packetData = System.Text.ASCIIEncoding.ASCII.GetBytes(sdata);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Tcp);
                if (ttsvalid == true)
                {
                    if (delayvalid == true)
                    {
                        for (int i = 0; i <= TTS; i++)
                        {
                            client.SendTo(packetData, ep);
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Sent packetData ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("'");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(sdata);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("'");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Sent To: " + ip);
                            Console.WriteLine("");
                            Console.WriteLine("Total Sent: " + i);
                            Console.WriteLine("");
                            Console.WriteLine("Total KB Sent: " + i * sizeinKB);
                            Thread.Sleep(delay);
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: '" + delay + "' is OVER the maximum amount (250)");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: '" + TTS + "' is OVER the maximum amount (100)");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Beep(500, 1000);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: 'UDP' or 'TCP' were not found.");
                Console.ReadKey();
            }
        }
    }
}