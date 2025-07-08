using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Offres;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IOffreService
    {
        IEnumerable<Offre> GetAll();
        Offre GetById(int id);
        void Create(CreateRequestO model);
        void Update(int id, UpdateRequestO model);
        void Delete(int id);
    }

    public class OffreService : IOffreService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public OffreService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Offre> GetAll() => _context.Offres;

        public Offre GetById(int id) => getOffre(id);

        public void Create(CreateRequestO model)
        {
            var offre = _mapper.Map<Offre>(model);
            _context.Offres.Add(offre);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestO model)
        {
            var offre = getOffre(id);
            _mapper.Map(model, offre);
            _context.Offres.Update(offre);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var offre = getOffre(id);
            _context.Offres.Remove(offre);
            _context.SaveChanges();
        }

        private Offre getOffre(int id)
        {
            var o = _context.Offres.Find(id);
            if (o == null) throw new KeyNotFoundException("Offre not found");
            return o;
        }
    }
}
