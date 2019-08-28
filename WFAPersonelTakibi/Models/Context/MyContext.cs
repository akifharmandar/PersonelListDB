namespace WFAPersonelTakibi.Models.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using WFAPersonelTakibi.Models.Entities;

    public class MyContext : DbContext
    {
        public MyContext()
        {
            Database.Connection.ConnectionString = @"server=SAMSUNG-SAMSUNG\SQLEXPRESS;database=PersonelDB;integrated security=yes";
        }


        public virtual DbSet<Personel> Personels { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }

}