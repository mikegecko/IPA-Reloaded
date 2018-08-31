﻿using IllusionPlugin.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerBase = IllusionPlugin.Logging.Logger;

namespace IllusionInjector.Logging.Printers
{
    class PluginLogFilePrinter : GZFilePrinter
    {
        public override LoggerBase.LogLevel Filter { get; set; } = LoggerBase.LogLevel.All;

        private string name;

        protected override FileInfo GetFileInfo()
        {
            var logsDir = new DirectoryInfo(Path.Combine("Logs",name));
            logsDir.Create();
            var finfo = new FileInfo(Path.Combine(logsDir.FullName, $"{DateTime.Now:yyyy.MM.dd.HH.mm}.log"));
            return finfo;
        }

        public PluginLogFilePrinter(string name)
        {
            this.name = name;
        }

        public override void Print(IllusionPlugin.Logging.Logger.Level level, DateTime time, string logName, string message)
        {
            foreach (var line in message.Split(new string[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                fileWriter.WriteLine(string.Format("[{3} @ {2:HH:mm:ss}] {0}", line, logName, time, level.ToString().ToUpper()));
        }
    }
}
