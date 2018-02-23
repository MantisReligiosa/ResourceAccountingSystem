using DomainObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceInterfaces
{
    public interface IConsumptionAccounter
    {
        void AddNewHouse(House house);
        void DeleteHouse(int houseId);
        House FindHouse(int houseId);
        void UpdateHouseInformation(House house);
        Task<IEnumerable<House>> GetAllHousesAsync();
        void RegisterMeter(int houseId, string meterSerial);
        Task<House> GetHouseWithMaxConsumptionAsync();
        Task<House> GetHouseWithMinConsumptionAsync();
        void EnterMeterReading(string meterSerial, uint readingValue);
        void EnterMeterReading(int houseId, uint readingValue);
    }
}
