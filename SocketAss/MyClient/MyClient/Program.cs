using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using MyClient;

public class clnt
{
    public static void Main()
    {
        

            try
            {
            
            while (true)
            {
                TcpClient tcpclnt = new TcpClient();

                Console.WriteLine("Connecting.....");

                string serverIp = "127.0.0.1";
                int port = 8003;

                tcpclnt.Connect(serverIp, port);
                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted (For Exit enter:-\"exit\" ): ");
                string str = Console.ReadLine();
                if (str == "exit")
                {
                    break;
                }
                string encryptedData =EncryptDecryptData.EncryptString(str);
                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(encryptedData);
               
                stm.Write(ba, 0, ba.Length);
               
                byte[] bb = new byte[1024];
                int k = 0;

               
                while ((k = stm.Read(bb, 0, bb.Length)) > 0)
                {
                    string receivedData = Encoding.ASCII.GetString(bb, 0, k);
                    
                    string decryptedData = EncryptDecryptData.DecryptString(receivedData);
                    Console.WriteLine($"Received: {decryptedData}");
                }

                Console.WriteLine("\nData transfer complete.");
                tcpclnt.Close();

            }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                
            }

        }
    
    }


