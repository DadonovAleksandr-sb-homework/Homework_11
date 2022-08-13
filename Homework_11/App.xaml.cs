using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NLog;

namespace Homework_11
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public App()
        {
            logger.Debug("Запуск приложения");
            logger.Debug($"Вызов конструктора {this.GetType().Name}");
        }
    }
}