using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace teladelogin
{
    static class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // No método Main ou Load de algum form:
            logger.Info("=== APLICAÇÃO INICIADA ===");
            logger.Info("Teste de log funcionando!");
            Application.Run(new FrmLogin());
        }
    }
}
