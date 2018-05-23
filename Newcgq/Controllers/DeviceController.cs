using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newcgq.Extends;
using Newcgq.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Newcgq.Controllers
{
    public class DeviceController : UserBase
    {
        WIFIcgqContext _context;
        IHostingEnvironment _hostingEnvironment;

        struct ChartData
        {
            public string name;
            public string[] value;
        }

        public DeviceController(WIFIcgqContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Device()
        {
            return View();
        }

        public IActionResult Moduel()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult ExportAd(IFormCollection form)
        {
            try
            {
                DateTime begin, stop;
                begin = Convert.ToDateTime(form["begin"]);
                stop = Convert.ToDateTime(form["stop"]);
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataAd.Where(x => x.Time <= stop.AddDays(1) && x.Time >= begin).OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                double voltage;
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        voltage = 0;
                        test[0] = vtemp[12];
                        test[1] = vtemp[13];
                        test[2] = vtemp[14];
                        var testV = test[0] + test[1] + test[2];

                        voltage = Convert.ToInt32(testV, 16);

                        voltage = Math.Round(voltage / Math.Pow(2, 23) * 5, 7);
                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "电压";
                        worksheet.Cells[1, 3].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = voltage;
                        worksheet.Cells["C" + (i + 2)].Value = time;



                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }
        public IActionResult ExportAdAll()
        {
            try
            {
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataAd.OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                double voltage;
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        voltage = 0;
                        test[0] = vtemp[12];
                        test[1] = vtemp[13];
                        test[2] = vtemp[14];
                        var testV = test[0] + test[1] + test[2];

                        voltage = Convert.ToInt32(testV, 16);

                        voltage = Math.Round(voltage / Math.Pow(2, 23) * 5, 7);
                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "电压";
                        worksheet.Cells[1, 3].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = voltage;
                        worksheet.Cells["C" + (i + 2)].Value = time;



                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }

        public IActionResult ExportDa(IFormCollection form)
        {
            try
            {
                DateTime begin, stop;
                begin = Convert.ToDateTime(form["begin"]);
                stop = Convert.ToDateTime(form["stop"]);
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataDa.Where(x => x.Time <= stop.AddDays(1) && x.Time >= begin).OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        test[0] = vtemp[10];
                        test[1] = vtemp[11];
                        test[2] = vtemp[12];

                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "DA1";
                        worksheet.Cells[1, 3].Value = "DA2";
                        worksheet.Cells[1, 4].Value = "DA3";
                        worksheet.Cells[1, 5].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = test[0];
                        worksheet.Cells["C" + (i + 2)].Value = test[1];
                        worksheet.Cells["D" + (i + 2)].Value = test[2];
                        worksheet.Cells["E" + (i + 2)].Value = time;



                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }
        public IActionResult ExportDaAll()
        {
            try
            {

                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataDa.OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        test[0] = vtemp[10];
                        test[1] = vtemp[11];
                        test[2] = vtemp[12];

                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "DA1";
                        worksheet.Cells[1, 3].Value = "DA2";
                        worksheet.Cells[1, 4].Value = "DA3";
                        worksheet.Cells[1, 5].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = test[0];
                        worksheet.Cells["C" + (i + 2)].Value = test[1];
                        worksheet.Cells["D" + (i + 2)].Value = test[2];
                        worksheet.Cells["E" + (i + 2)].Value = time;



                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }

        public IActionResult ExportIo(IFormCollection form)
        {
            try
            {
                DateTime begin, stop;
                begin = Convert.ToDateTime(form["begin"]);
                stop = Convert.ToDateTime(form["stop"]);
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataDa.Where(x => x.Time <= stop.AddDays(1) && x.Time >= begin).OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        test[0] = vtemp[10];
                        test[1] = vtemp[11];

                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "port0";
                        worksheet.Cells[1, 3].Value = "port1";
                        worksheet.Cells[1, 4].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = test[0];
                        worksheet.Cells["C" + (i + 2)].Value = test[1];
                        worksheet.Cells["D" + (i + 2)].Value = time;

                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }
        public IActionResult ExportIoAll()
        {
            try
            {
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

                var data = new SendDataAd();
                var temp = _context.SendDataDa.OrderBy(y => y.Time).ToList();
                var result = new ChartData[temp.Count()];
                string device;
                string time;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    for (int i = 0; i < temp.Count(); i++)
                    {


                        var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                        string[] test = new string[3];
                        test[0] = vtemp[10];
                        test[1] = vtemp[11];

                        device = vtemp[1] + vtemp[2] + vtemp[3];
                        time = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");

                        //添加头
                        worksheet.Cells[1, 1].Value = "设备号";
                        worksheet.Cells[1, 2].Value = "port0";
                        worksheet.Cells[1, 3].Value = "port1";
                        worksheet.Cells[1, 4].Value = "时间";
                        //添加值
                        worksheet.Cells["A" + (i + 2)].Value = device;
                        worksheet.Cells["B" + (i + 2)].Value = test[0];
                        worksheet.Cells["C" + (i + 2)].Value = test[1];
                        worksheet.Cells["D" + (i + 2)].Value = time;

                    }
                    package.Save();
                }

                return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            catch
            {
                return View("/Shared/Error");
            }
        }

        public JsonResult ShowDevice()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                var deviceInfo = _context.DeviceInfo.Where(x => x.UserName.Equals(userName)).Select(x => x.DeviceId).ToList();
                return Json(deviceInfo);
            }
            catch
            {
                return null;
            }
        }

        public JsonResult AddDevice(string deviceId)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                if (_context.DeviceInfo.Where(x => x.DeviceId.Equals(deviceId)).Count() != 0)
                    return Json("-1");
                else
                {
                    DeviceInfo newDevice = new DeviceInfo();
                    newDevice.UserName = userName;
                    newDevice.DeviceId = deviceId;
                    if (_context.DeviceInfo.Count() != 0)
                        newDevice.Id = _context.DeviceInfo.Max(x => x.Id) + 1;
                    else
                        newDevice.Id = 1;
                    _context.Add(newDevice);
                    _context.SaveChanges();
                }

                return Json("1");
            }
            catch
            {
                return null;
            }
        }
        public JsonResult DelDevice(string deviceId)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                var device = _context.DeviceInfo.Where(x => x.UserName.Equals(userName)).Where(y => y.DeviceId.Equals(deviceId)).Single();
                _context.Remove(device);
                _context.SaveChanges();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public JsonResult SaveModuel(string deviceId, bool ad, bool da, bool io)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                var device = _context.DeviceInfo.Where(x => x.UserName.Equals(userName)).Where(y => y.DeviceId.Equals(deviceId)).Single();
                if (ad)
                    device.Admoduel = !device.Admoduel;
                if (da)
                    device.Damoduel = !device.Damoduel;
                if (io)
                    device.Iomoduel = !device.Iomoduel;
                _context.SaveChanges();
                return Json(1);
            }
            catch
            {
                return null;
            }
        }

        public JsonResult DeviceList()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                var deviceList = _context.DeviceInfo.Where(x => x.UserName.Equals(userName)).Select(y => y.DeviceId).ToList();
                return Json(deviceList);
            }
            catch
            {
                return null;
            }
        }

        public JsonResult ModuelList(string deviceId)
        {
            try
            {
                ModuelInfo moduel = new ModuelInfo();
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                var device = _context.DeviceInfo.Where(x => x.UserName.Equals(userName) && x.DeviceId.Equals(deviceId)).SingleOrDefault();
                moduel.ADModuel = device.Admoduel;
                moduel.DAModuel = device.Damoduel;
                moduel.IOModuel = device.Iomoduel;
                return Json(moduel);
            }
            catch
            {
                return null;
            }
        }

        public JsonResult ControllerMessage(string deviceId, string func, int interval, int intervalUnit, string data)
        {
            try
            {
                ControllerMessage message = new ControllerMessage();
                if (_context.ControllerMessage.Count() != 0)
                    message.Id = _context.ControllerMessage.Max(x => x.Id) + 1;
                else
                    message.Id = 1;
                var userId = HttpContext.Session.GetInt32("UserId");
                var userName = _context.UserInfo.Where(x => x.Id.Equals(userId)).SingleOrDefault().UserName;
                message.UserName = userName;
                message.DeviceInfo = deviceId;
                message.Func = func;
                message.Interval = interval;
                message.IntervalUnit = intervalUnit;
                message.Data = data;
                message.Time = DateTime.Now;
                _context.Add(message);
                _context.SaveChanges();
                return Json(1);
            }
            catch
            {
                return Json(-1);
            }
        }

        public JsonResult HistoryData(DateTime begin, DateTime stop)
        {
            var data = new SendDataAd();
            var temp = _context.SendDataAd.Where(x => x.Time <= stop.AddDays(1) && x.Time >= begin).OrderBy(y => y.Time).ToList();
            var result = new ChartData[temp.Count()];
            for (int i = 0; i < temp.Count(); i++)
            {
                result[i].name = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");
                result[i].value = new string[2];
                double voltage;
                try
                {
                    var vtemp = Regex.Split(temp[i].Data, "(?<=\\G.{2})");
                    string[] test = new string[3];
                    voltage = 0;
                    test[0] = vtemp[12];
                    test[1] = vtemp[13];
                    test[2] = vtemp[14];
                    var testV = test[0] + test[1] + test[2];

                    voltage = Convert.ToInt32(testV, 16);

                    voltage = Math.Round(voltage / Math.Pow(2, 23) * 5, 7);
                }
                catch
                {
                    voltage = 0;
                }
                result[i].value[0] = Convert.ToDateTime(temp[i].Time).ToString("yyyy-MM-dd hh:mm:ss");
                result[i].value[1] = voltage.ToString();
            }
            return Json(result);
        }
    }
}
