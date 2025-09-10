1. Modelo: Aluno.cs
using System.ComponentModel.DataAnnotations;

public class Aluno
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O RA é obrigatório.")]
    [RaValidation(ErrorMessage = "O RA deve começar com 'RA' seguido de 6 dígitos.")]
    public string Ra { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public bool Ativo { get; set; }
}

2. Validação customizada: RaValidation.cs
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class RaValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string ra)
        {
            // Verifica se começa com RA e depois tem exatamente 6 dígitos
            return Regex.IsMatch(ra, @"^RA\d{6}$");
        }
        return false;
    }
}

3. Controller: AlunoController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AlunoController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarAluno([FromBody] Aluno aluno)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Simula criação
        return Ok("Aluno criado com sucesso!");
    }
}

✅ Questão 5 - ProdutoController + Validação de código do produto
1. Modelo: Produto.cs
using System.ComponentModel.DataAnnotations;

public class Produto
{
    [Required(ErrorMessage = "Descrição é obrigatória.")]
    public string Descricao { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Estoque deve ser zero ou maior.")]
    public int Estoque { get; set; }

    [Required(ErrorMessage = "Código do Produto é obrigatório.")]
    [CodigoProdutoValidation(ErrorMessage = "O código deve estar no formato 'AAA-1234'.")]
    public string CodigoProduto { get; set; }
}

2. Validação customizada: CodigoProdutoValidation.cs
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CodigoProdutoValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string codigo)
        {
            // Verifica o padrão: 3 letras maiúsculas, hífen, 4 números
            return Regex.IsMatch(codigo, @"^[A-Z]{3}-\d{4}$");
        }
        return false;
    }
}

3. Controller: ProdutoController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    [HttpPost]
    public IActionResult CriarProduto([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Simula criação
        return Ok("Produto criado com sucesso!");
    }
}
