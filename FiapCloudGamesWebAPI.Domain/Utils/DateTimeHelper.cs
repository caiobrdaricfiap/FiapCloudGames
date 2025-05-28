using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI.Domain.Utils
{
    public class DateTimeHelper
    {
        public static DateTime BrasiliaTime()
        {
            // Identificador padrão para Windows
            string windowsTimeZoneId = "E. South America Standard Time";
            // Identificador padrão para Linux/Mac
            string linuxTimeZoneId = "America/Sao_Paulo";

            TimeZoneInfo tz;
            try
            {
                // Tenta identificar pelo sistema operacional
                if (OperatingSystem.IsWindows())
                {
                    tz = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
                }
                else
                {
                    tz = TimeZoneInfo.FindSystemTimeZoneById(linuxTimeZoneId);
                }
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            }
            catch
            {
                // Se não achar o timezone, retorna Utc-3
                return DateTime.UtcNow.AddHours(-3);
            }
        }
    }
}
