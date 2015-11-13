﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Api.Controllers;
using NZBDash.Common;
using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Configuration;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Services;
using NZBDash.Core.Settings;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;

using UrlHelper = NZBDash.UI.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers
{
    public class DashboardController : BaseController
    {
        private IStatusApi Api { get; set; }
        private IHardwareService Service { get; set; }

        public DashboardController(IHardwareService service, IStatusApi statusApi)
            : base(typeof(DashboardController))
        {
            Api = statusApi;
            Service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetEnabledDashlets()
        //{
        //    var admin = new
        //}

        public ActionResult GetNzbGetDownloadInformation()
        {
            var admin = new NzbGetSettingsConfiguration();
            var config = admin.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress).ToString();
            try
            {
                var statusInfo = Api.GetNzbGetStatus(formattedUri, config.Username, config.Password);

                var downloadInfo = Api.GetNzbGetList(formattedUri, config.Username, config.Password);

                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                var model = new DownloaderViewModel
                {
                    Application = Applications.NzbGet,
                    DownloadSpeed = downloadSpeed.ToString(CultureInfo.CurrentUICulture),
                    DownloadItem = new List<DownloadItem>()
                };

                var results = downloadInfo.result;
                foreach (var result in results)
                {
                    var percentage = (result.DownloadedSizeMB / (result.RemainingSizeMB + (double)result.DownloadedSizeMB) * 100);

                    model.DownloadItem.Add(new DownloadItem
                    {
                        FontAwesomeIcon = IconHelper.ChooseIcon(EnumHelper<DownloadStatus>.Parse(result.Status)),
                        DownloadPercentage = Math.Ceiling(percentage).ToString(),
                        DownloadingName = result.NZBName,
                        Status = EnumHelper<DownloadStatus>.Parse(result.Status),
                        NzbId = result.NZBID
                    });
                }

                return PartialView("Partial/_Download", model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView("Partial/DashletError");
            }
        }



        public ActionResult GetDriveInformation()
        {
            var drives = Service.GetDrives();
            return PartialView("Partial/_DriveInformation", drives);
        }

        public ActionResult GetLinks()
        {
            var config = new LinksConfiguration();
            var allLinks = config.GetLinks();

            var model = allLinks.Select(link => new DashboardLinksViewModel { LinkEndpoint = link.LinkEndpoint, LinkName = link.LinkName }).ToList();

            return PartialView("Partial/_Links", model);
        }

        public ActionResult GetRam()
        {
            var ramModel = Service.GetRam();

            return PartialView("Partial/_Ram", ramModel);
        }

        //public ActionResult GetServerInformation()
        //{
        //    var ramInfo = Api.GetRamInfo();
        //    var uptime = Api.UpTime();
        //    var model = new RamViewModel
        //    {
        //        OSFullName = ramInfo.OSFullName,
        //        OSPlatform = ramInfo.OSPlatform,
        //        OSVersion = ramInfo.OSVersion,
        //        Uptime = uptime
        //    };

        //    return PartialView("Partial/_ServerInformation", model);
        //}
    }
}