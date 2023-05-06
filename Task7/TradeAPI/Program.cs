using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using TradeAPI.Models;
using TradeAPI.Lib;

class Program
{
    static void Main(string[] args)
    {
       CsvConverter csvConverter = new CsvConverter();

        csvConverter.convertStrategy();

    }
}