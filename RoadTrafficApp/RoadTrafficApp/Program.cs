using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoadTrafficApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string boundingBox = "40.78132133668076%2C-73.84323120117186%2C40.64469860601899%2C-74.17144775390625";
            HttpWebRequest request = WebRequest.CreateHttp($"https://www.mapquestapi.com/traffic/v2/incidents?&outFormat=json&boundingBox={boundingBox}&key=mpvuon2G6tIdTuDj2rxs03EXS14MHAAl");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                using (StreamWriter writer = new StreamWriter("data.txt"))
                {
                    foreach (var incident in rootObject.Incidents)
                    {
                        writer.WriteLine($"Широта - {incident.Latitude}");
                        writer.WriteLine($"Долгота - {incident.Longitude}");
                        writer.WriteLine($"Начало - {incident.StartTime}");
                        writer.WriteLine($"Конец - {incident.EndTime}");
                        writer.WriteLine($"Трудность - {incident.Severity}");

                        writer.Write("Тип - ");
                        if (incident.Type == 1)
                        {
                            writer.WriteLine("стройка");
                        }
                        else if (incident.Type == 2)
                        {
                            writer.WriteLine("движение перекрыто из-за события");
                        }
                        else if (incident.Type == 3)
                        {
                            writer.WriteLine("пробка");
                        }
                        else
                        {
                            writer.WriteLine("авария");
                        }

                        if (incident.Impacting )
                        {
                            writer.WriteLine("Инцидент влияет на дорожный трафик");
                        }
                        else
                        {
                            writer.WriteLine("Инцидент не влияет на дорожный трафик");
                        }
                        writer.WriteLine($"Проблема - {incident.ShortDescription}");
                        writer.WriteLine("Подробнее:");
                        if (string.IsNullOrWhiteSpace(incident.ParameterizedDescription.FromLocation))
                        {
                            writer.WriteLine("\tОт неизвестно");
                        }
                        else
                        {
                            writer.WriteLine($"\tОт {incident.ParameterizedDescription.FromLocation}");
                        }
                        if (string.IsNullOrEmpty(incident.ParameterizedDescription.ToLocation))
                        {
                            writer.WriteLine($"\tДо неизвестно");
                        }
                        else
                        {
                            writer.WriteLine($"\tДо {incident.ParameterizedDescription.ToLocation}");
                        }
                        writer.WriteLine($"\tНазвание дороги - {incident.ParameterizedDescription.RoadName}");
                        if (incident.Distance == 0)
                        {
                            writer.WriteLine($"\tДистанция в милях - неизвестно");
                        }
                        else
                        {
                            writer.WriteLine($"\tДистанция в милях - {incident.Distance}");
                        }
                        writer.WriteLine($"\tОписание - {incident.ParameterizedDescription.EventText}");
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}