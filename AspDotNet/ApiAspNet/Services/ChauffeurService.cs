using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Chauffeurs;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IChauffeurService
    {
        IEnumerable<Chauffeur> GetAll();
        Chauffeur GetById(int id);
        void Create(CreateRequestC model);
        void Update(int id, UpdateRequestC model);
        void Delete(int id);
    }

    public class ChauffeurService : IChauffeurService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public ChauffeurService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Chauffeur> GetAll() => _context.Chauffeurs;

        public Chauffeur GetById(int id) => getChauffeur(id);

        public void Create(CreateRequestC model)
        {
            var chauffeur = _mapper.Map<Chauffeur>(model);
            _context.Chauffeurs.Add(chauffeur);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestC model)
        {
            var chauffeur = getChauffeur(id);
            _mapper.Map(model, chauffeur);
            _context.Chauffeurs.Update(chauffeur);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var chauffeur = getChauffeur(id);
            _context.Chauffeurs.Remove(chauffeur);
            _context.SaveChanges();
        }

        private Chauffeur getChauffeur(int id)
        {
            var entity = _context.Chauffeurs.Find(id);
            if (entity == null) throw new KeyNotFoundException("Chauffeur not found");
            return entity;
        }
    }

}
