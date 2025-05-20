// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KartMaster.Areas.Identity.Pages.Account.Manage {
    public class IndexModel : PageModel {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Required(ErrorMessage = "O Nome é obrigatório.")]
            [StringLength(50, ErrorMessage = "O Nome não pode ter mais do que {1} caracteres.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "O Email não é válido.")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Número de Telefone")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Senha Atual")]
            public string CurrentPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Nova Senha")]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Nova Senha")]
            [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação não coincidem.")]
            public string ConfirmPassword { get; set; }
        }

        private async Task LoadAsync(IdentityUser user) {
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel {
                Nome = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Não foi possível carregar o utilizador com o ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Não foi possível carregar o utilizador com o ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) {
                await LoadAsync(user);
                return Page();
            }

            // Atualizar Nome
            if (Input.Nome != user.UserName) {
                user.UserName = Input.Nome;
                var setUserNameResult = await _userManager.UpdateAsync(user);
                if (!setUserNameResult.Succeeded) {
                    foreach (var error in setUserNameResult.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Atualizar Email
            if (Input.Email != user.Email) {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded) {
                    foreach (var error in setEmailResult.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Atualizar Número de Telefone
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber) {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded) {
                    foreach (var error in setPhoneResult.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Atualizar Senha
            if (!string.IsNullOrEmpty(Input.NewPassword)) {
                if (string.IsNullOrEmpty(Input.CurrentPassword)) {
                    ModelState.AddModelError("Input.CurrentPassword", "A senha atual é obrigatória para alterar a senha.");
                    return Page();
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.CurrentPassword, Input.NewPassword);
                if (!changePasswordResult.Succeeded) {
                    foreach (var error in changePasswordResult.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado com sucesso!";
            return RedirectToPage();
        }
    }
}
