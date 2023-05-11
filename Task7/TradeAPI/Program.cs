using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using TradeAPI.Models;
using TradeAPI.Lib;
using TradeAPI.TradesData;
using Microsoft.Extensions.DependencyInjection;
using TradeAPI.Db;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        string fullPath = Path.Combine(Constants.PnlPath);

        CsvConverter csvConverter = new CsvConverter();

        csvConverter.ConvertStrategy(fullPath);

        // Build the service provider
        var serviceProvider = new ServiceCollection()
            .AddDbContext<MyDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TradeApi;Integrated Security=True"))
            .BuildServiceProvider();

        // Use the database context
        using (var context = serviceProvider.GetService<MyDbContext>())
        {
            // Perform database operations here
        }

    }
}