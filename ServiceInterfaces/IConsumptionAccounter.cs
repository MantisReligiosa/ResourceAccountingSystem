using DomainObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IConsumptionAccounter
    {
        House AddNewHouse(House house);
        void DeleteHouse(int houseId);
        House FindHouse(int houseId);
        void UpdateHouseInformation(House house);
        Task<List<House>> GetAllHousesAsync();
        List<House> GetAllHouses();
        void RegisterMeter(int houseId, string meterSerial);
        Task<House> GetHouseWithMaxConsumptionAsync();
        Task<House> GetHouseWithMinConsumptionAsync();
        void EnterMeterReading(string meterSerial, int readingValue);
        void EnterMeterReading(int houseId, int readingValue);
    }
}
