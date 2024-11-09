﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Web.Security;
using System.Configuration;

namespace devarts.App_Start
{
    public static class InitializeMembership
    {
        public static void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            CreateAdministrator();
        }

        private static void CreateAdministrator()
        {
            string roleName = ConfigurationManager.AppSettings["AdminRole"];
            string userName = ConfigurationManager.AppSettings["AdminLogin"];
            string password = ConfigurationManager.AppSettings["AdminPassword"];

            // Utworzenie roli "Administrator" jeśli nie istnieje.
            if (!Roles.RoleExists(roleName))
                Roles.CreateRole(roleName);

            // Utworzenie użytkownika "Administrator" jeśli nie istnieje.
            if (!WebSecurity.UserExists(userName))
                WebSecurity.CreateUserAndAccount(userName, password);

            // Dodanie użytkownika "Administrator" do roli "Administrator".
            if (!Roles.GetRolesForUser(userName).Contains(roleName))
                Roles.AddUsersToRoles(new[] { userName }, new[] { roleName });
        }
    }
}