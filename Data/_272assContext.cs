using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _272ass.Data
{
    public class _272assContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public _272assContext() : base("name=_272assContext")
        {
        }

        public System.Data.Entity.DbSet<_272ass.Models.Attendee> Attendees { get; set; }

        public System.Data.Entity.DbSet<_272ass.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<_272ass.Models.EventType> EventTypes { get; set; }

        public System.Data.Entity.DbSet<_272ass.Models.Organiser> Organisers { get; set; }

        public System.Data.Entity.DbSet<_272ass.Models.Admin> Admins { get; set; }
    }
}
