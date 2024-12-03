using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using MyServer;

public class Server
{
    private static readonly Dictionary<string, Dictionary<string, int>> dataSet = new()
    {
        { "SetA", new Dictionary<string, int> { { "One", 1 }, { "Two", 2 } } },
        { "SetB", new Dictionary<string, int> { { "Three", 3 }, { "Four", 4 } } },
        { "SetC", new Dictionary<string, int> { { "Five", 5 }, { "Six", 6 } } },
        { "SetD", new Dictionary<string, int> { { "Seven", 7 }, { "Eight", 8 } } },
        { "SetE", new Dictionary<string, int> { { "Nine", 9 }, { "Ten", 10 } } }
    };

    




    public static void Main()
    {
        IPAddress ipAd = IPAddress.Any;
        int port = 8003;

        TcpListener listener = new TcpListener(ipAd, port);
        try
        {
            listener.Start();
            Console.WriteLine(" connecting...");

            while (true)
            {
                Socket mySocket = listener.AcceptSocket();
                

                Thread clientThread = new Thread(()=>HandleClient(mySocket));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
          
        }
        finally
        {
            listener.Stop();
            Console.WriteLine("server stopped.");
        }
    }

    private static void HandleClient(Socket mySocket)
    {
        try
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = mySocket.Receive(buffer);

            Console.WriteLine("Received data:");
            string receivedData = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
            


            //decrypting data
            string decryptedata =  EncryptDevryptData.DecryptString(receivedData);
            Console.WriteLine("decryptedata :" + decryptedata);


            int loopData = 0;
            string[] parts = decryptedata.Split('-');
            if (dataSet.ContainsKey(parts[0]))
            {
                if (dataSet[parts[0]].ContainsKey(parts[1]))
                {
                     loopData = dataSet[parts[0]][parts[1]];
                    Console.WriteLine("Loop Data: " + loopData);
                    for (int i = 0; i < loopData; i++)
                    {
                        string res = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");


                        //encrypt data
                        string response =  EncryptDevryptData.EncryptString(res);
                       
                        byte[] responseBytes = Encoding.ASCII.GetBytes(response);

                        try
                        {
                            if (mySocket.Connected)
                            {
                                mySocket.Send(responseBytes);
                                Console.WriteLine($"Sent: {response}");
                            }
                            else
                            {
                                Console.WriteLine("Socket is no longer connected.");
                                break;
                            }
                        }
                        catch (Exception sendEx)
                        {
                            Console.WriteLine($"Send error: {sendEx.Message}");
                            break;
                        }

                        Thread.Sleep(1000);  
                    }
                }
                else
                {
                    Console.WriteLine("Does not contain nested key.");
                    string response = EncryptDevryptData.EncryptString("EMPTY");

                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                        
                            mySocket.Send(responseBytes);
                        
                }
            }
            else
            {
                Console.WriteLine("Does not contain key.");
                string response = EncryptDevryptData.EncryptString("EMPTY");

                byte[] responseBytes = Encoding.ASCII.GetBytes(response);


                mySocket.Send(responseBytes);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Client handling error: " + e.Message);
        }
        finally
        {
            
                mySocket.Close();
            
        }
    }

}
