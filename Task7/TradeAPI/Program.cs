using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using TradeAPI.Models;
using TradeAPI.Lib;
using TradeAPI.TradesData;

class Program
{
    static void Main(string[] args)
    {
        string fullPath = Path.Combine(Constants.PnlPath);

        CsvConverter csvConverter = new CsvConverter();

        csvConverter.ConvertStrategy(fullPath);

    }
}