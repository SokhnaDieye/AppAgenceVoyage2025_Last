using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using AppAgenceVoyage.Models;
using AppAgenceVoyage2025.Models;

namespace AppAgenceVoyage2025.App_Start
{
    public class Utils
    {
        //Déclaration de la variable permettant d'accéder à la base de données
        BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        /// <summary>
        /// rédiger les erreurs au niveau de la base de données
        /// </summary>
        /// <param name="titre">la titre provequant l'erreur</param>
        /// <param name="erreur">le message d'erreur</param>
        public void WriteDataError(string TitreErreur, string erreur)
        {
            try
            {
                Td_Erreur log = new Td_Erreur();
                log.DateErreur = DateTime.Now;
                log.DescriptionErreur = erreur.Length > 1000 ? erreur.Substring(0, 1000) : erreur;
                log.TitreErreur = TitreErreur;
                db.td_Erreurs.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString(), "WriteDataError");
            }
        }

        /// <summary>
        /// Rédiger le message d'erreur dans un fichier
        /// </summary>
        /// <param name="message">le message d'erreur</param>
        public static void WriteFileError(string message)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Error/erreur.txt");
                System.IO.TextWriter writeFile = new StreamWriter(path, true);
                writeFile.WriteLine("" + DateTime.Now);
                writeFile.WriteLine(message);
                writeFile.WriteLine("---------------------------------------------------------------------------------------");
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (IOException e)
            {
                WriteLogSystem(e.ToString(), "WriteFileError");
            }
        }

        /// <summary>
        /// Pour créer un fichier d'erreur
        /// </summary>
        /// <param name="message">le message d'erreur</param>
        /// <returns>retounre si le fichier est créer</returns>
        public bool CreateFile(string message)
        {
            bool rep = false;
            string fileName = string.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Error/" + fileName + ".txt");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Create(path);
                bool fileUse = true;
                while (fileUse)
                {
                    try
                    {
                        System.IO.TextWriter writeFile = new StreamWriter(path, true);
                        writeFile.WriteLine("" + DateTime.Now);
                        writeFile.WriteLine(message);
                        writeFile.WriteLine("-------------------------------------------");
                        writeFile.Flush();
                        writeFile.Close();
                        writeFile = null;
                        fileUse = false;
                    }
                    catch (Exception e)
                    {
                        WriteLogSystem(e.ToString(), "CreateFile");
                    }
                }
                rep = true;
            }
            catch (IOException e)
            {
                WriteLogSystem(e.ToString(), "WriteFileError");
            }
            return rep;
        }

        /// <summary>
        /// Permet de rédiger une liste d'erreur dans un fichier
        /// </summary>
        /// <param name="message">liste message d'erreur</param>
        /// <param name="theFile">nom du fichier</param>
        public void WriteErrorLoad(List<string> message, string theFile)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Error/" + theFile + ".txt");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                System.IO.TextWriter writeFile = new StreamWriter(path, true);
                //writeFile.WriteLine("" + DateTime.Now);
                //while (!IsFileReady(path))
                //{
                //    System.Threading.Thread.Sleep(1000);
                //}
                writeFile.WriteLine("---------------------DEBUT----------------------");
                foreach (var item in message)
                {
                    writeFile.WriteLine(item);
                }
                writeFile.WriteLine("----------------------FIN---------------------");
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (IOException e)
            {
                WriteLogSystem(e.ToString(), "WriteErrorLoad");
            }
        }

        /// <summary>
        /// Ecrire un message d'erreur au niveau du Systéme
        /// </summary>
        /// <param name="erreur">Message d'erreur</param>
        /// <param name="libelle">titre du message</param>
        public static void WriteLogSystem(string erreur, string libelle)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "AppAgenceVoyage";
                eventLog.WriteEntry(string.Format("date: {0}, libelle: {1}, description {2}", DateTime.Now, libelle, erreur), EventLogEntryType.Information, 101, 1);
            }
        }

    }
}