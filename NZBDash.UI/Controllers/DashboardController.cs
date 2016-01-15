﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Mapping;
using NZBDash.Core.Interfaces;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.Hardware;
using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers
{
    public class DashboardController : BaseController
    {
        private IThirdPartyService Api { get; set; }
        private IHardwareService Service { get; set; }
        private ILinksConfiguration LinksConfiguration { get; set; }

        public DashboardController(IHardwareService service, IThirdPartyService api, ILinksConfiguration linksConfiguration, ILogger logger)
            : base(logger)
        {
            Api = api;
            Service = service;
            LinksConfiguration = linksConfiguration;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDriveInformation()
        {
            var drives = Service.GetDrives();
            return PartialView("Partial/_DriveInformation", drives);
        }

        public ActionResult GetLinks()
        {
            var allLinks = LinksConfiguration.GetLinks();

            var model = allLinks.Select(link =>
                (DashboardLinksViewModel)new DashboardLinksViewModel().InjectFrom(link))
                .ToList();

            return PartialView("Partial/_Links", model);
        }

        public ActionResult GetRam()
        {
            var ramModel = Service.GetRam();

            return PartialView("Partial/_Ram", ramModel);
        }

        public ActionResult GetServerInformation()
        {
            var ramInfo = Service.GetRam();

            var model = new ServerInformationViewModel();
            model.InjectFromJson<ServerInformationViewModel>(ramInfo);
            model.CpuPercentage = Service.GetCpuPercentage();
            model.AvailableMemory = Service.GetAvailableRam();
            model.Uptime = Service.GetUpTime();


            return PartialView("Partial/_ServerInformation", model);
        }

        public ActionResult GetCpu()
        {
            var js = new JavaScriptSerializer().Serialize(CpuCounter.Counter.Last());
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCpu()
        {
            var js = new JavaScriptSerializer().Serialize(CpuCounter.Counter);
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}