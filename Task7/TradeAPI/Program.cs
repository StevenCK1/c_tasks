using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using TradeAPI.Models;
using TradeAPI.Lib;
using System.Reflection.Metadata;
using TradeAPI.TradesData;

class Program
{
    static void Main(string[] args)
    {
        string relativePath = @".\TradesData\pnl.csv";
        string fullPath = Path.Combine(relativePath);

        CsvConverter csvConverter = new CsvConverter();

        csvConverter.ConvertStrategy(fullPath);

    }
}