using System;
using System.Text.RegularExpressions;
using static System.IO.File;
using static System.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using CsvHelper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    static void Main(string[] args)
    {
        //_1ex_SimpleTools();
        //_2ex_TextFieldParser();
        //_3rd_CsvHelper();

        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        MyApplication app = serviceProvider.GetService<MyApplication>();
        try
        {
            app.Start();
        }
        catch (Exception ex)
        {
            app.HandleError(ex);
        }
        finally
        {
            app.Stop();
        }
    }
    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddLogging(configure => configure.AddConsole())
        .AddTransient<MyApplication>();
    }

    static void _1ex_SimpleTools()
    {
        var ages = new List<int>();
        using (StreamReader sr = new StreamReader(@".\test.csv"))
        {
            string currentLine;
            int lineCounter = 0;
            while ((currentLine = sr.ReadLine()) != null)
            {
                if (lineCounter++ == 0)
                    continue;
                var datafields = currentLine.Split(',');
                ages.Add(int.Parse(datafields[^1]));
            }
        }

        Console.WriteLine(calcAverage(ages));
    }

    static void _2ex_TextFieldParser()
    {
        using (TextFieldParser parser = new TextFieldParser(@".\test.csv"))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                if (parser.LineNumber == 1)
                {
                    parser.ReadFields();
                    continue;
                }

                string[] fields = parser.ReadFields();
                foreach (string field in fields)
                {
                    Console.WriteLine(field);
                }
            }
        }
    }

    static void _3rd_CsvHelper()
    {
        using (var reader = new StreamReader(@".\test.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>();
            foreach (var record in records)
            {
                Console.WriteLine(record.id);
                Console.WriteLine(record.name);
                Console.WriteLine(record.age);
            }
        }
    }

    static double calcAverage(List<int> arr)
    {
        int sum = 0;
        foreach (var n in arr)
            sum += n;

        return sum / arr.Count;
    }
}

class MyApplication
{
    private readonly ILogger _logger;
    public MyApplication(ILogger<MyApplication> logger)
    {
        _logger = logger;
    }
    public void Start()
    {
        _logger.LogInformation($"MyApplication Started at {DateTime.Now}");
        LoadDashboard();
    }

    private void LoadDashboard()
    {
        try
        {
            _logger.LogWarning("MyApplication->LoadDashboard() can throw Exception!");
            int[] a = new int[] { 1, 2, 3, 4, 5 };
            int b = a[5];
            Console.WriteLine($"Value of B: {b}");
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _logger.LogCritical($"MyApplication->LoadDashboard() Code needs to be fixed");
        }
    }

    public void Stop()
    {
        _logger.LogInformation($"MyApplication Stopped at {DateTime.Now}");
    }

    public void HandleError(Exception ex)
    {
        _logger.LogError($"MyApplication Error Encountered at {DateTime.Now} & Error is: {ex.Message}");
    }
}