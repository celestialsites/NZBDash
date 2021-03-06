﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: ServerInformationViewModel.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.ComponentModel.DataAnnotations;

using Humanizer;

namespace NZBDash.UI.Models.Hardware
{
    public class ServerInformationViewModel
    {
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_OSFullName", ResourceType = typeof(Resources.Resources))]
        public string OsFullName { get;set; }
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_OSPlatform", ResourceType = typeof(Resources.Resources))]
        public string OsPlatform { get;set; }
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_OSVersion", ResourceType = typeof(Resources.Resources))]
        public string OsVersion { get; set; }
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_Uptime", ResourceType = typeof(Resources.Resources))]
        public TimeSpan Uptime { get; set; }
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_CpuPercentage", ResourceType = typeof(Resources.Resources))]
        public float CpuPercentage { get; set; }
        [Display(Name = "UI_Models_Hardware_ServerInformationViewModel_AvailableMemory", ResourceType = typeof(Resources.Resources))]
        public float AvailableMemory { get; set; }

        public string UptimeString
        {
            get { return Uptime.Humanize(3); }
        }
    }
}
