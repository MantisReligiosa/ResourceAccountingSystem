using BLL;
using BLL.Exceptions;
using DatabaseRepository;
using DomainObjects;
using RepositoryInterfaces;
using ResourceAccountingSystemInterface.Common;
using ResourceAccountingSystemInterface.Models;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior jsonRequestBehavior)
        {
            return new JsonDataContractResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = jsonRequestBehavior
            };
        }
    }
}