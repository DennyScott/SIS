﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Load();
            Console.ReadLine();
        }
    }
}
