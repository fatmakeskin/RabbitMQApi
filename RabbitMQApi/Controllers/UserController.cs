using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQApi.RabbitMQ.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("Get")]
        public IActionResult Get(User user)
        {
            var messeage = JsonConvert.SerializeObject(user);
            RabbitmqProducer producer = new RabbitmqProducer();
            producer.PublishData(messeage,"ApiQueue");
            return Ok();
        }

    }
}
