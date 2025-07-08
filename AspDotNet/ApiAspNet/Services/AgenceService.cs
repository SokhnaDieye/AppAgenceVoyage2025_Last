using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Agences;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IAgenceService
    {
        IEnumerable<Agence> GetAll();
        Agence GetById(int id);
        void Create(CreateRequestA model);
        void Update(int id, UpdateRequestA model);
        void Delete(int id);
    }

    public class AgenceService : IAgenceService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public AgenceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Agence> GetAll() => _context.Agences.ToList();

        public Agence GetById(int id) => GetAgence(id);

        public void Create(CreateRequestA model)
        {
            var agence = _mapper.Map<Agence>(model);
            _context.Agences.Add(agence);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestA model)
        {
            var agence = GetAgence(id);
            _mapper.Map(model, agence);
            _context.Agences.Update(agence);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var agence = GetAgence(id);
            _context.Agences.Remove(agence);
            _context.SaveChanges();
        }

        private Agence GetAgence(int id)
        {
            var agence = _context.Agences.Find(id);
            if (agence == null) throw new KeyNotFoundException("Agence not found");
            return agence;
        }
    }
}
