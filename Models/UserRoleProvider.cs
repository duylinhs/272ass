using _272ass.Data;
using _272ass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _272ass.Models
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (_272assContext _conx = new _272assContext())
            {
                Debug.WriteLine("Reached");
                string rol = "";
                    if(_conx.Users.Any(e => e.Username == username && e is Admin && e.Deleted!=true))
                    {
                    rol = "Admin";
                }
                    else if (_conx.Users.Any(e => e.Username == username && e is Organiser && e.Deleted != true))
                {
                    rol = "Organiser";
                }
                    else if (_conx.Users.Any(e => e.Username == username && e is Attendee && e.Deleted != true))
                {
                    rol = "Attendee";
                        }
                //_conx.Users.Where(c=>c.Username == username).Where(c=>!c.Deleted).Select(e=>EF.Property<string>(e,"Discriminator")).ToString();
                string[] l = new string[] {rol };
                return l;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}