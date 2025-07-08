using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Data.Entity;


namespace MetierAgenceVoyage.Models
{
    public class BdAgenceVoyageContext:DbContext
    {
        public  BdAgenceVoyageContext():base("ConnAgenceVoyage1")
        {
        
        }
        public DbSet<Chauffeur> Chauffeurs { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agence> Agences { get; set; }
        public DbSet<Offre> Offres {  get; set; }
        public DbSet<Voyage> Voyages { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Td_Erreur> td_Erreurs { get; set; }

        /* public DbSet<Reservation> Reservations { get; set; }
         public DbSet<AppelOffre> AppelOffres { get; set; }*/



    }
}