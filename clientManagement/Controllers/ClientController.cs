using clientManagement.Models;
using clientManagement.Services;
using Microsoft.AspNetCore.Mvc;
namespace clientManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clienteService)
        {
            _clientService = clienteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> ListClients()
        {
            var clients = _clientService.ListClients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public ActionResult CreateClient([FromBody] Client client)
        {
            try
            {
                _clientService.CreateUser(client);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] Client client)
        {
            if (id != client.Id)
            {
                return BadRequest(new { message = "O ID fornecido na URL não coincide com o ID do cliente." });
            }

            try
            {
                _clientService.UpdateClient(client);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                _clientService.DeleteClient(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
