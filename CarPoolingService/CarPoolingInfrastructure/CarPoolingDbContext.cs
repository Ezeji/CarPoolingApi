using CarPoolingInfrastructure.Models;
using Lib.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingInfrastructure
{
    public class CarPoolingDbContext : MongoDbContext
    {
        public CarPoolingDbContext(IMongoDatabase database) : base(database)
        {
        }

        public IMongoCollection<AvailableCar> AvailableCars => Database.GetCollection<AvailableCar>("availableCars");
        public IMongoCollection<CarDetail> CarDetails => Database.GetCollection<CarDetail>("carDetails");
        public IMongoCollection<JourneyDetail> JourneyDetails => Database.GetCollection<JourneyDetail>("journeyDetails");
    }
}
