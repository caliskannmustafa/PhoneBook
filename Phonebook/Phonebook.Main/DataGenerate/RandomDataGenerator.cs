using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Phonebook.Main.DAL;
using Phonebook.Main.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Phonebook.Main.DataGenerate
{
    public class RandomDataGenerator : IRandomDataGenerator
    {
        List<FirstNameEntity> firstNames = new List<FirstNameEntity>();
        List<LastNameEntity> lastNames = new List<LastNameEntity>();
        List<PhoneEntity> phones = new List<PhoneEntity>();
        private readonly Random _random = new Random();
        private readonly IUnitOfWork _unitOfWork;

        public RandomDataGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ReadDataFromFiles();
        }

        public void Generate(int dataCount)
        {
            dataCount = dataCount > 10000 ? 10000 : dataCount;
            List<Person> people = new List<Person>();

            for (int i = 0; i < dataCount; i++)
            {
                string firstName = GenerateFirstName();
                string lastName = GenerateLastName();
                string email = (firstName + lastName + "@phonebook.com").ToLower();
                Coordinate coord = GenerateLocation();

                people.Add(new Person()
                {
                    CompanyName = "Phonebook Ltd.",
                    CreateDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    ContactInfos = new List<ContactInfo>()
                    {
                         //Add Phone
                         new ContactInfo()
                         {
                             CreateDate=DateTime.Now,
                             ContactType=Entity.Enums.EnumContactType.PhoneType,
                             Value=GeneratePhone()
                         },

                          //Add Email
                         new ContactInfo()
                         {
                             CreateDate=DateTime.Now,
                             ContactType=Entity.Enums.EnumContactType.EmailAddress,
                             Value=email
                         },

                         //Add Email
                         new ContactInfo()
                         {
                             CreateDate=DateTime.Now,
                             ContactType=Entity.Enums.EnumContactType.GeoLocation,
                             Value=string.Join(",",coord.Latitude,coord.Longitude)
                         },
                    },
                });
            }

            _unitOfWork.PersonRepository.context.AddRange(people);
            _unitOfWork.Save();
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private void ReadDataFromFiles()
        {
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"..\Phonebook.Main\Content\firstname.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray jArray = (JArray)JToken.ReadFrom(reader);
                firstNames = jArray.ToObject<List<FirstNameEntity>>();
            }

            using (StreamReader file = File.OpenText(@"..\Phonebook.Main\Content\lastname.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray jArray = (JArray)JToken.ReadFrom(reader);
                lastNames = jArray.ToObject<List<LastNameEntity>>();
            }

            using (StreamReader file = File.OpenText(@"..\Phonebook.Main\Content\phone.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray jArray = (JArray)JToken.ReadFrom(reader);
                phones = jArray.ToObject<List<PhoneEntity>>();
            }
        }
        private string GenerateFirstName()
        {
            var index = RandomNumber(0, firstNames.Count - 1);
            return firstNames[index].FirstName;
        }
        private string GenerateLastName()
        {
            var index = RandomNumber(0, lastNames.Count - 1);
            return lastNames[index].LastName;
        }
        private string GeneratePhone()
        {
            var index = RandomNumber(0, phones.Count - 1);
            return phones[index].PhoneNumber;
        }
        private Coordinate GenerateLocation()
        {
            Coordinate location1 = new Coordinate() { Latitude = -90, Longitude = 0 };
            Coordinate location2 = new Coordinate() { Latitude = 90, Longitude = 0 };
            Coordinate location3 = new Coordinate() { Latitude = 0, Longitude = -180 };
            Coordinate location4 = new Coordinate() { Latitude = 0, Longitude = 180 };

            var coordinates = LocationCalculate(location1, location2, location3, location4);
            return coordinates[0];
        }

        //https://stackoverflow.com/questions/41342183/generate-random-coordinates-with-boundaries/41342490
        //
        private Coordinate[] LocationCalculate(Coordinate location1, Coordinate location2, Coordinate location3,
       Coordinate location4)
        {
            Coordinate[] allCoords = { location1, location2, location3, location4 };
            double minLat = allCoords.Min(x => x.Latitude);
            double minLon = allCoords.Min(x => x.Longitude);
            double maxLat = allCoords.Max(x => x.Latitude);
            double maxLon = allCoords.Max(x => x.Longitude);

            Random r = new Random();

            Coordinate[] result = new Coordinate[1];
            for (int i = 0; i < result.Length; i++)
            {
                Coordinate point = new Coordinate();
                do
                {
                    point.Latitude = r.NextDouble() * (maxLat - minLat) + minLat;
                    point.Longitude = r.NextDouble() * (maxLon - minLon) + minLon;
                } while (!IsPointInPolygon(point, allCoords));
                result[i] = point;
            }
            return result;
        }

        //took it from http://codereview.stackexchange.com/a/108903
        //you can use your own one
        private bool IsPointInPolygon(Coordinate point, Coordinate[] polygon)
        {
            int polygonLength = polygon.Length, i = 0;
            bool inside = false;
            // x, y for tested point.
            double pointX = point.Longitude, pointY = point.Latitude;
            // start / end point for the current polygon segment.
            double startX, startY, endX, endY;
            Coordinate endPoint = polygon[polygonLength - 1];
            endX = endPoint.Longitude;
            endY = endPoint.Latitude;
            while (i < polygonLength)
            {
                startX = endX;
                startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.Longitude;
                endY = endPoint.Latitude;
                //
                inside ^= ((endY > pointY) ^ (startY > pointY)) /* ? pointY inside [startY;endY] segment ? */
                          && /* if so, test if it is under the segment */
                          (pointX - endX < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            return inside;
        }
    }
    public class Coordinate
    {
        public double Latitude { set; get; }
        public double Longitude { set; get; }
    }
    public class FirstNameEntity
    {
        public string FirstName { get; set; }
    }
    public class LastNameEntity
    {
        public string LastName { get; set; }
    }
    public class PhoneEntity
    {
        public string PhoneNumber { get; set; }
    }
}
