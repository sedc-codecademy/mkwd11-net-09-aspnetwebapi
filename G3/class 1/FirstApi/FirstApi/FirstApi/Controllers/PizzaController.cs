using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private static List<Pizza> pizzas = new List<Pizza>();

        [HttpGet]
        public IActionResult GetPizzas()
        {
            return Ok(pizzas);
        }
        //api/pizza/1
        [HttpGet("{pizzaId}")]
        public IActionResult GetPizza(int pizzaId)
        {
            var pizza = pizzas.FirstOrDefault(x => x.Id == pizzaId);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pizza pizza)
        {
            pizza.Id = pizzas.Any() ? pizzas.Max(x => x.Id) + 1 : 1;
            pizzas.Add(pizza);
            return Created("api/pizza", pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [Bind("Name")] Pizza pizza)
        {
            var toUpdate = pizzas.FirstOrDefault(x => x.Id == id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Name = pizza.Name;
            return Ok(toUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = pizzas.FirstOrDefault(x => x.Id == id);

            if(pizza == null)
            {
                return NotFound();
            }

            pizzas.Remove(pizza);
            return Ok();
        }
        
    }
}
