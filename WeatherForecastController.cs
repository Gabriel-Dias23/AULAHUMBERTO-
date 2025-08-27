using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        public class Produto
        {
            public string Nome { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
            public float Largura { get; set; }
            public float Comprimento { get; set; }
            public string UF { get; set; }
        }

        public class ResultadoFrete
        {
            public string Produto { get; set; }
            public float Volume { get; set; }
            public float ValorFrete { get; set; }
        }

        [HttpPost("calcular")]
        public ActionResult<ResultadoFrete> CalcularFrete([FromBody] Produto produto)
        {
            if (produto == null || produto.Altura <= 0 || produto.Largura <= 0 || produto.Comprimento <= 0)
                return BadRequest("Dados inválidos");

            // 1) Calcular volume
            float volume = produto.Altura * produto.Largura * produto.Comprimento;

            // 2) Taxa fixa por cm³
            float taxaPorCm3 = 0.01f;
            float valorBase = volume * taxaPorCm3;

            // 3) Taxa por estado
            float taxaEstado = produto.UF.ToUpper() switch
            {
                "SP" => 50f,
                "RJ" => 60f,
                "MG" => 55f,
                _ => 70f
            };

            float valorFinal = valorBase + taxaEstado;

            return Ok(new ResultadoFrete
            {
                Produto = produto.Nome,
                Volume = volume,
                ValorFrete = valorFinal
            });
        }
    }
}
