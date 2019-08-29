using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.Repository.DataBase
{
    public class VMInitializer : DropCreateDatabaseIfModelChanges<VmDbContext>
    {
        protected override void Seed(VmDbContext context)
        {
            #region Product Table

            //var pro = new Product
            //{
            //    ProductID = "1",
            //    Name = "1",
            //    Genre = "1",
            //    Starring = "1|1",
            //    SupportingActors = "1|1|1",
            //    Director = "1|1|1",
            //    ScriptWriter = "1",
            //    ProductionCountry = "China",
            //    ProductCompany = "Boone's Cop",
            //    ReleaseYear = 2019,
            //    Language = "CN",
            //    RunTime = 123,
            //    Price = 45.55M,
            //    Poster = "11",
            //    Stock = 100,
            //    Story = "开篇讲了...然后发生了...最终主角战胜邪恶...",
            //    StoryAbstract = "这是一个美好的故事"
            //};

            //context.Products.Add(pro);
            //context.SaveChanges();

            #endregion
        }
    }
}
