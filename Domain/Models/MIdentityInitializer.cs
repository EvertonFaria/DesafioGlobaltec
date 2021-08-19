using System;
using Microsoft.AspNetCore.Identity;
using DesafioGlobaltec.Domain.Data;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Security {
    public class IdentityInitializer {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        ) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize() {
            if (_context.Database.EnsureCreated()) {
                if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_PESSOAS).Result) {
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(Roles.ROLE_API_PESSOAS)
                    ).Result;

                    if (!resultado.Succeeded) {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_API_PESSOAS}."
                        );
                    }
                }

                CreateUser(
                    new ApplicationUser() {
                        UserName = "admin",
                        Email = "admin@teste.com.br",
                        EmailConfirmed = true
                    }, 
                    "Admin@123!", 
                    Roles.ROLE_API_PESSOAS
                );

                CreateUser(
                    new ApplicationUser() {
                        UserName = "usuario",
                        Email = "usuario@teste.com.br",
                        EmailConfirmed = true
                    }, 
                    "User@123!"
                );
            }
        }
        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null
        ) {
            if (_userManager.FindByNameAsync(user.UserName).Result == null) {
                var resultado = _userManager.CreateAsync(
                    user, 
                    password
                ).Result;

                if (resultado.Succeeded && !String.IsNullOrWhiteSpace(initialRole)) {
                    _userManager.AddToRoleAsync(
                        user, 
                        initialRole
                    ).Wait();
                }
            }
        }
    }
}