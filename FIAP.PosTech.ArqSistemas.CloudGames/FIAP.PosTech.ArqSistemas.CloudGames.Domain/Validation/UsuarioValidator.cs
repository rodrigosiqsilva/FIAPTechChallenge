using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Domain.Validation
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("O objeto Usuario não pode ser nulo.");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do usuário é obrigatório.");

            RuleFor(x => x.Senha).NotEmpty().WithMessage("A senha do usuário é obrigatória.")
                                 .Must(senha => ValidarSenha(senha)).WithMessage("A senha deve ser segura (mínimo de 8 caracteres com números, letras e caracteres especiais).");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail do usuário é obrigatório.")
                                 .EmailAddress().WithMessage("O e-mail do usuário deve ser válido.");
        }

        public static bool ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) return false;

            // Regex explicada:
            // ^                : Início da string
            // (?=.*[A-Za-z])   : Deve conter pelo menos uma letra
            // (?=.*\d)         : Deve conter pelo menos um número
            // (?=.*[@$!%*#?&]) : Deve conter pelo menos um caractere especial
            // [A-Za-z\d@$!%*#?&]{8,} : Mínimo de 8 caracteres (letras, números ou especiais)
            // $                : Fim da string
            string padrao = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";

            return Regex.IsMatch(senha, padrao);

            //Testes

            //string[] senhas = { "Senha123!", "fraca", "12345678", "SemEspecial1", "Valid1@#" };

            //foreach (string s in senhas)
            //{
            //    Console.WriteLine($"Senha: {s} - Válida: {Validar(s)}");
            //}
        }
    }
}
