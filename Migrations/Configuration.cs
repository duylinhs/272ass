namespace _272ass.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using _272ass.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<_272ass.Data._272assContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(_272ass.Data._272assContext context)
        {
            //  This method will be called after migrating to the latest version.
            var et = new List<EventType>
            {
                new EventType{Title ="Student Seminar"},
                new EventType{Title ="Employee Seminar"},
                new EventType{Title ="Instructor Seminar"},
                new EventType{Title ="Faculty Seminar"}
            };
            et.ForEach(s => context.EventTypes.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();
            var a = new List<Admin>
            {
                new Admin{Username ="admin",Password="admin"},
                
            };
            et.ForEach(s => context.EventTypes.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
