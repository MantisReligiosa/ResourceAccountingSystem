using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Validators;
using DomainObjects;
using Extensions;
using RepositoryInterfaces;
using ServiceInterfaces;

namespace BLL
{
    public class ConsumptionAccounter : IConsumptionAccounter
    {
        private readonly IDatabaseRepository _databaseRepository;
        public ConsumptionAccounter(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public void AddNewHouse(House house)
        {
            if (_databaseRepository.Any<House>(h => h.Address.Equals(house.Address)))
            {
                throw new AlreadyExistsException("Дом с таким адресом уже занесен в базу");
            }
            var validationResult = new HouseValidator().Validate(house);
            if (!validationResult.IsValid)
            {
                throw new ValidationErrorException(string.Join(", ", validationResult.Errors.ToList()));
            }
            UpdateHouseInformation(house);
        }

        public void DeleteHouse(int houseId)
        {
            if (!_databaseRepository.Any<House>(h => h.Id == houseId))
            {
                throw new NotFoundException($"Дом с Id {houseId} не найден");
            }
            _databaseRepository.DeleteById<House>(houseId);
        }

        public void EnterMeterReading(string meterSerial, uint readingValue)
        {
            if (!_databaseRepository.Any<Meter>(h => h.Serial.Equals(meterSerial)))
            {
                throw new NotFoundException($"Счётчик с серийным номером {meterSerial} не найден");
            }
            var meter = _databaseRepository.GetQueryable<Meter>().First(m => m.Serial.Equals(meterSerial));
            meter.ReadingValue = readingValue;
            _databaseRepository.AddOrEdit(meter);
        }

        public void EnterMeterReading(int houseId, uint readingValue)
        {
            var house = FindHouse(houseId);
            var meter = house.Meter;
            if (meter.IsNull())
            {
                throw new NotFoundException($"В доме с Id {houseId} не найден счётчик");
            }
            meter.ReadingValue = readingValue;
            _databaseRepository.AddOrEdit(meter);
        }

        public House FindHouse(int houseId)
        {
            var house = _databaseRepository.GetQueryable<House>().FirstOrDefault(h => h.Id == houseId);
            if (house.IsNull())
            {
                throw new NotFoundException($"Дом с Id {houseId} не найден");
            }
            return house;
        }

        public async Task<IEnumerable<House>> GetAllHousesAsync()
        {
            return await _databaseRepository.GetAsync<House>();
        }

        public async Task<House> GetHouseWithMaxConsumptionAsync()
        {
            return await _databaseRepository.GetMaxAsync<House>(h => h.Meter.ReadingValue);
        }

        public async Task<House> GetHouseWithMinConsumptionAsync()
        {
            return await _databaseRepository.GetMinAsync<House>(h => h.Meter.ReadingValue);
        }

        public void RegisterMeter(int houseId, string meterSerial)
        {
            var house = FindHouse(houseId);
            house.Meter = new Meter
            {
                Serial = meterSerial
            };
            UpdateHouseInformation(house);
        }

        public void UpdateHouseInformation(House house)
        {
            _databaseRepository.AddOrEdit(house);
        }
    }
}
