using BLL;
using BLL.Exceptions;
using DatabaseRepository;
using DomainObjects;
using Extensions;
using ResourceAccountingSystemInterface.Common;
using ResourceAccountingSystemInterface.DTO;
using ResourceAccountingSystemInterface.Models;
using ServiceInterfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ResourceAccountingSystemInterface.Controllers
{
    public class HomeController : AsyncController
    {
        private readonly IConsumptionAccounter _consumptionAccounter;
        public HomeController(IConsumptionAccounter consumptionAccounter)
        {
            _consumptionAccounter = consumptionAccounter;
        }

        public HomeController()
            : this(new ConsumptionAccounter(
                new Repository($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Database.mdf")};Integrated Security=True;Connect Timeout=30")))
        {
        }

        public async Task<ActionResult> Index()
        {
            var model = new MainModel();
            model.Houses = await _consumptionAccounter.GetAllHousesAsync();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddHouse(string houseAddress)
        {
            try
            {
                var house = _consumptionAccounter.AddNewHouse(new House
                {
                    Address = houseAddress
                });
                return Json(new Result
                {
                    Success = true,
                    Message = "Дом добавлен",
                    Data = house
                });
            }
            catch (Exception ex)
            {
                if (ex is AlreadyExistsException ||
                    ex is ValidationErrorException)
                    return Json(new Result
                    {
                        Success = false,
                        Message = ex.Message
                    });
                else
                    return Json(new Result
                    {
                        Success = false,
                        Message = "Ошибка выполнения"
                    });
            }
        }

        [HttpPost]
        public ActionResult DeleteHouse(int houseId)
        {
            try
            {
                _consumptionAccounter.DeleteHouse(houseId);
                return Json(new Result
                {
                    Success = true,
                    Message = "Дом удален"
                });
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                    return Json(new Result
                    {
                        Success = false,
                        Message = ex.Message
                    });
                else
                    return Json(new Result
                    {
                        Success = false,
                        Message = "Ошибка выполнения"
                    });
            }
        }

        [HttpPost]
        public ActionResult UpdateHouseMeter(MeterUpdatingInfo info)
        {
            try
            {
                var house = _consumptionAccounter.FindHouse(info.houseIdUpdate);
                if (house.Meter.IsNull())
                {
                    house.Meter = new Meter
                    {
                        Serial = info.meterSerialUpdate
                    };
                }
                else
                {
                    house.Meter.Serial = info.meterSerialUpdate;
                    house.Meter.ReadingValue = default(int);
                }
                _consumptionAccounter.UpdateHouseInformation(house);
                return Json(new Result
                {
                    Success = true,
                    Message = "Счётчик добавлен",
                    Data = house.Meter.Serial
                });
            }
            catch (Exception ex)
            {
                return Json(new Result
                {
                    Success = false,
                    Message = "Ошибка выполнения"
                });
            }
        }

        [HttpPost]
        public ActionResult UpdateMeterReadingValue(MeterReadingsValueUpdatingInfo info)
        {
            try
            {
                _consumptionAccounter.EnterMeterReading(info.meterSerialUpdateVal, info.meterValueUpdate);
                return Json(new Result
                {
                    Success = true,
                    Message = "Данные переданы"
                });
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException ||
                    ex is ValidationErrorException)
                    return Json(new Result
                    {
                        Success = false,
                        Message = ex.Message
                    });

                return Json(new Result
                {
                    Success = false,
                    Message = "Ошибка выполнения"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetMaxConsumption()
        {
            try
            {
                var house = await _consumptionAccounter.GetHouseWithMaxConsumptionAsync();
                return Json(new Result
                {
                    Success = true,
                    Data = house
                });
            }
            catch
            {
                return Json(new Result
                {
                    Success = false,
                    Message = "Ошибка выполнения"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetMinConsumption()
        {
            try
            {
                var house = await _consumptionAccounter.GetHouseWithMinConsumptionAsync();
                return Json(new Result
                {
                    Success = true,
                    Data = house
                });
            }
            catch
            {
                return Json(new Result
                {
                    Success = false,
                    Message = "Ошибка выполнения"
                });
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDataContractResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}