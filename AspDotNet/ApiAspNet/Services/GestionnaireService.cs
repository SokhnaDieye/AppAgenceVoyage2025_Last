using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Clients;
using ApiAspNet.Models.Gestionnaires;
using AutoMapper;

namespace ApiAspNet.Services
{

    public interface IGestionnaireService
    {
        IEnumerable<Gestionnaire> GetAll();
        Gestionnaire GetById(int id);
        void Create(CreateRequestG model);
        void Update(int id, UpdateRequestG model);
        void Delete(int id);
    }

    public class GestionnaireService : IGestionnaireService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public GestionnaireService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Gestionnaire> GetAll() => _context.Gestionnaires;

        public Gestionnaire GetById(int id) => getGestionnaire(id);

        public void Create(CreateRequestG model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new AppException("Un utilisateur avec l'email '" + model.Email + "' existe déjà.");
            var gestionnaire = _mapper.Map<Gestionnaire>(model);

            gestionnaire.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Gestionnaires.Add(gestionnaire);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestG model)
        {
            var gestionnaire = getGestionnaire(id);
            _mapper.Map(model, gestionnaire);
            _context.Gestionnaires.Update(gestionnaire);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var gestionnaire = getGestionnaire(id);
            _context.Gestionnaires.Remove(gestionnaire);
            _context.SaveChanges();
        }

        private Gestionnaire getGestionnaire(int id)
        {
            var g = _context.Gestionnaires.Find(id);
            if (g == null) throw new KeyNotFoundException("Gestionnaire not found");
            return g;
        }
    }
}
