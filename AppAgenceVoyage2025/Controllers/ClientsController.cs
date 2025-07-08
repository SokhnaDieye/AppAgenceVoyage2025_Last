using System.Linq;
﻿using AppAgenceVoyage.Models;
using System.Web.Mvc;
using RestSharp;
using AppAgenceVoyage2025.Models;
using AppAgenceVoyage2025.Utils;

namespace AppAgenceVoyage2025.Controllers
{
    public class ClientsController : Controller
    {
        private readonly BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        // Afficher la vue Index
        public ActionResult Index()
        {
            return View();
        }

        // Liste des clients
        public JsonResult List()
        {
            var clients = db.Clients.ToList(); // récupérer les clients de la base de données
            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        // Ajouter un client
        // Ajouter un client
        [HttpPost]
        public JsonResult Add(Client client)
        {
            if (ModelState.IsValid)
            {
                string phoneNumber = $"+221{client.TelephoneUtilisateur}";
                string message = $"Hello 👋 {client.PreomUtilisateur} Bienvenue dans MTD VOYAGE!!";

                db.Clients.Add(client);
                db.SaveChanges();
                Mailer.SendMail(client.EmailUtilisateur, "Bienvenue sur MTD-Voyage", "<div style=\"background: #007bff; color: white; text-align: center; padding: 15px; border-radius: 10px 10px 0 0; font-size: 20px; font-weight: bold;\">\r\n            Bienvenue chez MTD_VOYAGE 🎉\r\n        </div>\r\n        <div style=\"padding: 20px; text-align: center;\">\r\n            <p>Bonjour "+client.PreomUtilisateur+" "+ client.NomUtilisateur + ",</p>\r\n            <p>Nous sommes ravis de vous accueillir chez <strong>MTD-VOYAGE</strong> !</p>\r\n            <p>Merci de nous avoir rejoints. Nous espérons que vous profiterez pleinement de nos services.</p>\r\n            <a href=\"[Lien vers votre site]\" style=\"display: inline-block; padding: 10px 20px; margin-top: 20px; color: white; background: #007bff; text-decoration: none; border-radius: 5px;\">Découvrir votre espace</a>\r\n        </div>\r\n        <div style=\"margin-top: 20px; padding: 10px; font-size: 12px; text-align: center; color: #777;\">\r\n            © 2025 MTD-VOYAGE | <a href=\"[Lien de désinscription]\" style=\"color: #007bff;\">Se désinscrire</a>\r\n        </div>\r\n    </div>");

                var whatsapp = new RestClient("https://api.wassenger.com/v1/messages");
                var request = new RestRequest()
                {
                    Method = Method.Post
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Token", "7a9a302f6bd1730e1aaf2d9fd3824c7692bad2281f5b4ed1e3d012c0c5295fcc0cf3c9c6ff5977b5");
                string jsonBody = $"{{\"phone\":\"{phoneNumber}\",\"message\":\"{message}\"}}";
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                RestResponse response = whatsapp.Execute(request);


                return Json(new { success = true, message = "Client ajouté avec succès" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join(", ", errors) });
            }
        }

        // Modifier un client
        [HttpPost]
        public JsonResult Update(Client client)
        {
            var modifieClient = db.Clients.Find(client.IdUtilisateur);
            if (modifieClient != null)
            {
                modifieClient.NomUtilisateur = client.NomUtilisateur;
                modifieClient.PreomUtilisateur = client.PreomUtilisateur;
                modifieClient.EmailUtilisateur = client.EmailUtilisateur;
                modifieClient.TelephoneUtilisateur = client.TelephoneUtilisateur;
                modifieClient.CNIClient = client.CNIClient;

                if (ModelState.IsValid)
                {
                    string phoneNumber = $"+221{modifieClient.TelephoneUtilisateur}";
                    string message = $"Hello 👋 {modifieClient.PreomUtilisateur} Vos informations ont ete modifies dans MTD VOYAGE!!";
                    db.SaveChanges();

                    var whatsapp = new RestClient("https://api.wassenger.com/v1/messages");
                    var request = new RestRequest()
                    {
                        Method = Method.Post
                    };
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Token", "7a9a302f6bd1730e1aaf2d9fd3824c7692bad2281f5b4ed1e3d012c0c5295fcc0cf3c9c6ff5977b5");
                    string jsonBody = $"{{\"phone\":\"{phoneNumber}\",\"message\":\"{message}\"}}";
                    request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                    RestResponse response = whatsapp.Execute(request);
                    return Json(new { success = true, message = "Client mis à jour avec succès" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }
            return Json(new { success = false, message = "Client non trouvé" });
        }


        // Supprimer un client
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var client = db.Clients.Find(id);
            if (client != null)
            {
                db.Clients.Remove(client);
                db.SaveChanges();
                return Json(new { success = true, message = "Client supprimé avec succès" });
            }
            return Json(new { success = false, message = "Client non trouvé" });
        }

        // Récupérer un client par ID
  
        public JsonResult GetByID(int id)
        {
            var client = db.Clients.Find(id);
            if (client != null)
            {
                return Json(new
                {
                    success = true,
                    IdUtilisateur = client.IdUtilisateur,
                    NomUtilisateur = client.NomUtilisateur,
                    PreomUtilisateur = client.PreomUtilisateur,
                    EmailUtilisateur = client.EmailUtilisateur,
                    TelephoneUtilisateur = client.TelephoneUtilisateur,
                    CNIClient = client.CNIClient
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Client non trouvé" }, JsonRequestBehavior.AllowGet);
        }
    }
}
