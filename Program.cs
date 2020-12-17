using System;
using Services;
using Models;

namespace RouteFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new Reader("TestExport.kml");
            reader.UpdateTimeStamps(DateTime.UtcNow);
            //Console.Write(reader.GetKmlDoc());
            Console.Write(reader.GetCsvDoc());
        }
    }
}
