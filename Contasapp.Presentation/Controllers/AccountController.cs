using Bogus;
using Contasapp.Presentation.Models;
using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Messages.Model;
using ContasApp.Messages.Services;
using ContasApp.Presentation.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Contasapp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    if (usuario != null)
                    {
                        var auth = new AuthViewModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now
                        };

                        var authJson = JsonConvert.SerializeObject(auth);

                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, authJson)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Dashboard", "Principal");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado!";
                    }

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }


            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        ModelState.AddModelError("Email", "Email já cadastrado!");
                    }
                    else
                    {
                        var usuario = new Usuario
                        {
                            Id = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = MD5Helper.Encrypt(model.Senha)
                        };

                        usuarioRepository.Add(usuario);

                        TempData["Mensagem"] = "Conta criada com sucesso!";

                        ModelState.Clear();
                    }

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //obter o usuário no banco de dados através do email
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    //verificar se o usuário foi encontrado
                    if (usuario != null)
                    {
                        //gerando uma nova senha para o usuário
                        Faker faker = new Faker();
                        var novaSenha = $"@{faker.Internet.Password(8)}{new Random().Next(999)}";

                        //criando o conteudo da mensagem que será enviada por email
                        var emailMessageModel = new EmailMessageModel
                        {
                            EmailDestinatario = usuario.Email,
                            Assunto = "Recuperação de senha de usuário - Contas App",
                            Corpo = $"Prezado, {usuario.Nome},\nsua nova senha de acesso é: {novaSenha}\nAtt\nEquipe Contas App"
                        };

                        //enviando a mensagem
                        EmailMessageService.Send(emailMessageModel);

                        //atualizando a senha do usuário no banco de dados
                        usuario.Senha = MD5Helper.Encrypt(novaSenha);
                        usuarioRepository.Update(usuario);

                        TempData["Mensagem"] = "Recuperação de senha realizada com sucesso. Verifique sua caixa de email.";
                        ModelState.Clear(); //limpar o formulário
                    }
                    else
                    {
                        TempData["Mensagem"] = "Usuário não encontrado. Verifique o email informado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");

        }
    }
}
