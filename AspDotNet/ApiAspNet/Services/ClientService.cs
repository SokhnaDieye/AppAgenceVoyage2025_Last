using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Clients;
using AutoMapper;

namespace ApiAspNet.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Create(CreateRequestCl model);
        void Update(int id, UpdateRequestCl model);
        void Delete(int id);
    }

    public class ClientService : IClientService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ClientService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Client> GetAll() => _context.Clients;

        public Client GetById(int id) => GetClient(id);


        public void Create(CreateRequestCl model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new AppException("Un utilisateur avec l'email '" + model.Email + "' existe déjà.");
            var client = _mapper.Map<Client>(model);

            client.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Clients.Add(client);
            _context.SaveChanges();
        }


        public void Update(int id, UpdateRequestCl model)
        {
            var client = GetClient(id);
            _mapper.Map(model, client);
            _context.Clients.Update(client);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = GetClient(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        private Client GetClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null) throw new KeyNotFoundException("Client not found");
            return client;
        }
    }
}
