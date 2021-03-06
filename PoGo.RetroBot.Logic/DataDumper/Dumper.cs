﻿#region using directives

using System;
using System.IO;
using PoGo.RetroBot.Logic.State;
using System.Globalization;
using System.Text;

#endregion

namespace PoGo.RetroBot.Logic.DataDumper
{
    public static class Dumper
    {
        /// <summary>
        ///     Clears the specified dumpfile.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="filename" />
        /// File to clear/param>
        public static void ClearDumpFile(ISession session, string filename, string extension = "csv")
        {
            var path = Path.Combine(session.LogicSettings.ProfilePath, "Dumps");
            var file = Path.Combine(path,
                $"RetroBot-{filename}-{DateTime.Today.ToString("yyyy-MM-dd")}-{DateTime.Now.ToString("HH")}.{extension}");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            // Clears all contents of a file first if overwrite is true
            File.WriteAllText(file, string.Empty);
        }

        /// <summary>
        ///     Dumps data to a file
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data">Dumps the string data to the file</param>
        /// <param name="filename">Filename to be used for naming the file.</param>
        /// <param name="extension">FileExt.</param>
        public static void Dump(ISession session, string[] data, string filename, string extension = "csv")
        {
            string uniqueFileName = $"{filename}";

            DumpToFile(session, data, uniqueFileName, extension);
        }

        /// <summary>
        ///     This is used for dumping contents to a file stored in the Logs folder.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data">Dumps the string data to the file</param>
        /// <param name="filename">Filename to be used for naming the file.</param>
        private static void DumpToFile(ISession session, string[] data, string filename, string extension = "csv")
        {
            var path = Path.Combine(session.LogicSettings.ProfilePath, "Dumps",
                $"RetroBot-{filename}-{DateTime.Today.ToString("yyyy-MM-dd")}-{DateTime.Now.ToString("HH")}.{extension}");

            CultureInfo culture = CultureInfo.CurrentUICulture;
            string listSeparator = culture.TextInfo.ListSeparator;

            using (
                var dumpFile =
                    File.AppendText(path)
                )
            {
                var sb = new StringBuilder();
                foreach (string str in data)
                {
                    if (sb.Length != 0)
                        sb.Append(listSeparator);

                    if (str.Contains("\""))
                    {
                        sb.AppendFormat("\"{0}\"", str.Replace("\"", "\"\""));
                    }
                    else if (str.Contains(listSeparator))
                    {
                        sb.AppendFormat("\"{0}\"", str);
                    }
                    else
                    {
                        sb.Append(str);
                    }
                }
                dumpFile.WriteLine(sb.ToString());
                dumpFile.Flush();
            }
        }

        /// <summary>
        ///     Set the dumper.
        /// </summary>
        /// <param name="dumper"></param>
        /// <param name="subPath"></param>
        public static void SetDumper(IDumper dumper, string subPath = "")
        {
        }
    }
}