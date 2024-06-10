using clientManagement.Data;
using clientManagement.Models;

namespace clientManagement.Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;

        public ClientService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> ListClients()
        {
            return _clientRepository.ListClients();
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetClientById(id);
        }

        public void CreateUser(Client client)
        {
            if (_clientRepository.ClientAlreadyExist(client.CPF))
            {
                throw new System.Exception("CPF já existe.");
            }

            _clientRepository.CreateClient(client);
        }

        public void UpdateClient(Client client)
        {

            if (_clientRepository.ClientAlreadyExist(client.CPF, client.Id))
            {
                throw new System.Exception("CPF já existe em outro cliente.");
            }

            _clientRepository.UpdateClient(client);
        }

        public void DeleteClient(int id)
        {
            Client client = _clientRepository.GetClientById(id);
            if (client == null)
            {
                throw new System.Exception("Cliente não encontrado.");
            }

            _clientRepository.DeleteClient(id);
        }
    }
}
