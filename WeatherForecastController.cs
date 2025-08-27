using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        // Modelo para receber os dados
        public class Pessoa
        {
            public string Nome { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
        }

        // Modelo de resposta para o IMC
        public class ResultadoIMC
        {
            public string Nome { get; set; }
            public float IMC { get; set; }
            public string Classificacao { get; set; }
        }

        // ------------------------------
        // Ação 1: Calcular IMC
        // ------------------------------
        [HttpPost("calcular-imc")]
        public ActionResult<ResultadoIMC> CalcularIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa == null || pessoa.Altura <= 0)
                return BadRequest("Dados inválidos");

            float imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);

            return Ok(new ResultadoIMC
            {
                Nome = pessoa.Nome,
                IMC = imc,
                Classificacao = ConsultarTabelaIMC(imc)
            });
        }

        // ------------------------------
        // Ação 2: Consulta Tabela IMC
        // ------------------------------
        private string ConsultarTabelaIMC(float imc)
        {
            if (imc < 18.5) return "Abaixo do peso";
            else if (imc < 24.9) return "Peso normal";
            else if (imc < 29.9) return "Sobrepeso";
            else if (imc < 34.9) return "Obesidade grau I";
            else if (imc < 39.9) return "Obesidade grau II";
            else return "Obesidade grau III (mórbida)";
        }
    }
}
