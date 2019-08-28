namespace WFAPersonelTakibi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WFAPersonelTakibi.Models.Context;
    using WFAPersonelTakibi.Models.Entities;
  
    

    internal sealed class Configuration : DbMigrationsConfiguration<WFAPersonelTakibi.Models.Context.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        

        protected override void Seed(WFAPersonelTakibi.Models.Context.MyContext context)
        {
            string[] departmanlar = { "yazýlým", "muhasebe", "lojistik", "planlama" };

            for (int i = 0; i < departmanlar.Length; i++)
            {
                Department departman = new Department();
                departman.Name = departmanlar[i];
                context.Departments.Add(departman);
                context.SaveChanges();               
                

            }
            
            
        }
       
    }
}
