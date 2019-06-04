using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Invoice_handler.Interfaces;
using Invoice_handler.Models;
using Invoice_handler.Utilities;

namespace Invoice_handler
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlHandler = new XmlHandler();
            var logic = new Logic(xmlHandler);

            Console.WriteLine("First task is under execution");
            logic.CreateExcelFromXml();
            Console.WriteLine($"First task is completed, location of the generated excel {Path.Combine(Environment.CurrentDirectory)}");

            Console.WriteLine("Second task is under execution");
            logic.CreateExcelFromKeyValuePair();
            Console.WriteLine($"Second task is completed, location of the generated excel {Path.Combine(Environment.CurrentDirectory)}");

            Console.WriteLine("Second task is under execution");
            logic.GenerateSeparateXmls();
            Console.WriteLine($"First task is completed, location of the generated xmls {Path.Combine(Environment.CurrentDirectory)}");

            Console.ReadLine();
        }
    }
}
