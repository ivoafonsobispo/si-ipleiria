using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Worksheet6_Ex2._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();

            byte[] originalData = Encoding.UTF8.GetBytes(File.ReadAllText("dados.txt"));
            byte[] hashOriginalData = sha256.ComputeHash(originalData);
            byte[] signature = Encoding.UTF8.GetBytes(File.ReadAllText

        }
    }
}
