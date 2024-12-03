using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyClient
{
    public class EncryptDecryptData
    {
        //public static readonly string publicKeyPem = File.ReadAllText("E:\\SocketAss\\public_key.pem");
        //public static readonly string privateKeyPem = File.ReadAllText("E:\\SocketAss\\private_key.pem");
       

        public static readonly string privateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCP5GoCVQfW0/N5\r\n+Du6pb0P/z+CXV0xs/nClZiS5YG86VGo0i5dHtx/y4MKWIW8JY51k+bYHQs6vFkL\r\n/jRxfRppMitwUmRq7DnSAGUGWajko9lCIQhJQarUo3JuWXlvO0pD/Ps/G5adr83D\r\nnrZEU1e8/nxmm+iQoGy8IAG90IZdMXCJSFuFqoQw3AAkviYhSAl0FTWRnuf4fqoN\r\nEril+iQkk/bvt5aj56G3BScwq2dwi9dlnrkyUR00LF7cFFsBMDRMvxgaSj3iuK7e\r\nzdjdKktmvFeaQUx/IaCLQChvFSyaUpfP4LoiTbL0R6Q7gA47q6o2GPhqjnXUSFRr\r\n2Gs4krH1AgMBAAECggEAPbrkna6D5+5S2EMdwLOEuf2rbc6HAiEvZ4KNC4wJThWc\r\ncjwFJu7r8qlIxOH4MaC9DoUOdi1zmVWfMA10yzE76OBckVnPea1xGr4PmRtS4Q/H\r\nz15jcyYIVyPtnhilh4ockLFRyh+YTHrU3/TRRdupWsuka9K8AXvJWiWzCY8RbZ34\r\nyq/qYVDTlP0vLvi1Vk/lH3caHf9G91c2NVcfs0iYNoy3+oVJX8VChWwDGbUVHuU8\r\ndNBQL+GjAxXIurnwH0xdKD9XicIHjrRmKmd9YCIFLKBh2jlpwoybRKj9C2jnADVL\r\nNngNB0W3LKM/8mAUuT0KyiiFUJ4saNOlpMzWwUWnWwKBgQDI+5B633v+FGXV+MRs\r\npQDNLh6EJXg4tp+HWBrh/6JUL12o7vwm9glmdSork1LQOotYk2AX7AtA/ZBrK9m9\r\nYEGZCcJPjjJ9eix59SnuF/w9hGXcrd5fkWshO55Mh1b3zvxVWNwvnKKKLrQ8zGat\r\nBlgiKPotUKcMOJ5QKbJn0FMjOwKBgQC3SBMTWZKKvQR19Ck3CJ7jX/+JwRvq/Kt0\r\nFcSgF3gXNJu/Hs8hyGd9vhL5xyWVmE7/gYhn2HpvZypATgkxjRimjkd2Ynn1XgI5\r\n53Z00a/dnG5ujYi8QtbrGzOdYCNmFbX9KFT33V5cchg1ZjP4iRMMJ1TkikXFPmg7\r\nKxpnTz3MjwKBgAVsA/HMpaOtGTI0i9gCe4sNe3VFLYEaLCOSlOcT5mUvAe/Uwqiq\r\naoaBEqoqR0mmyquRDj6W95bBQGwjurQJLLvcL4nF5EbOTPTdUExECWH/eGADsMQT\r\nQDidSx/1Zrs4skX7J7WBHEFuER6yxPJWAXqqcFVYn6kMvfibp1x1mkiRAoGAHVhH\r\n3ZhA0z/SQAR4uNwDfIo5QtgNXUFZPjviAHPhgW2l5iLLrTAGCebt6NoO1XXOaDBY\r\nI7BQYhfVdvhHloxtvvUW0giQWE3V82lw2OFd1BCgyciVdPcKrBft2quy3zxoqADW\r\nId1qp0iJQbt8ZLWlvBNsDy9oXl1QmPCcyJ+yWFcCgYAenCq97E1xiFn57026dM0t\r\nbpDNAPfn7h3BMZtWml3k3ETuMpgjF/zBoerat7TCR5rQyHgHFuwKJiiGrMYMMGhU\r\nZKC2kCoID08g5YcUqbPepgA3+2la4qmF2R8raVsm49GSN2Js2r1xOUIcpaj0YqDt\r\nvR05Q0vCI9eXWXav1yD8Cg==\r\n-----END PRIVATE KEY-----\r\n";
        public static readonly string publicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAj+RqAlUH1tPzefg7uqW9\r\nD/8/gl1dMbP5wpWYkuWBvOlRqNIuXR7cf8uDCliFvCWOdZPm2B0LOrxZC/40cX0a\r\naTIrcFJkauw50gBlBlmo5KPZQiEISUGq1KNybll5bztKQ/z7PxuWna/Nw562RFNX\r\nvP58ZpvokKBsvCABvdCGXTFwiUhbhaqEMNwAJL4mIUgJdBU1kZ7n+H6qDRK4pfok\r\nJJP277eWo+ehtwUnMKtncIvXZZ65MlEdNCxe3BRbATA0TL8YGko94riu3s3Y3SpL\r\nZrxXmkFMfyGgi0AobxUsmlKXz+C6Ik2y9EekO4AOO6uqNhj4ao511EhUa9hrOJKx\r\n9QIDAQAB\r\n-----END PUBLIC KEY-----\r\n";       
        public static string EncryptString(string originalData)
        {
         
            byte[] data = Encoding.UTF8.GetBytes(originalData);
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(publicKeyPem);
                byte[] encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string DecryptString(string encryptedData)
        {
         
            byte[] data = Convert.FromBase64String(encryptedData);
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(privateKeyPem);
                byte[] decryptedData = rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

    }
}
