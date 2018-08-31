﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllusionPlugin.Logging;
using LoggerBase = IllusionPlugin.Logging.Logger;

namespace IllusionInjector.Logging.Printers
{
    public class ColoredConsolePrinter : LogPrinter
    {
        LoggerBase.LogLevel filter = LoggerBase.LogLevel.All;
        public override LoggerBase.LogLevel Filter { get => filter; set => filter = value; }

        ConsoleColor color = Console.ForegroundColor;
        public ConsoleColor Color { get => color; set => color = value; }

        public override void Print(LoggerBase.Level level, DateTime time, string logName, string message)
        {
            if (((byte)level & (byte)StandardLogger.PrintFilter) == 0) return;
            Console.ForegroundColor = color;
            foreach (var line in message.Split(new string[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                Console.WriteLine(string.Format(LoggerBase.LogFormat, line, logName, time, level.ToString().ToUpper()));
            Console.ResetColor();
        }
    }
}
