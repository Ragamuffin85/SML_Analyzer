using System;
using System.IO.Ports;
using System.Collections.Generic;
using SML_Analyzer;


public class SerialPortTest
{ 
    public static void Main(string[] args)
    {
        Hager_EHZ ehz1 = new Hager_EHZ();
        while (!Console.KeyAvailable)
        {
            ehz1.ReadSequence();
            ehz1.Print();
        }
    }
}
