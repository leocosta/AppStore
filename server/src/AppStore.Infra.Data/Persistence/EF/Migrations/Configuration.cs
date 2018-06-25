namespace AppStore.Infra.Data.Persistence.EF.Migrations
{
    using System.Data.Entity.Migrations;
    using Domain.Users;
    using System;
    using Contexts;
    using Domain.Apps;
    using CrossCutting.Encryption.Extension;
    using Domain.CreditCards;

    internal sealed class Configuration : DbMigrationsConfiguration<AppStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppStoreContext context)
        {
            //  This method will be called after migrating to the latest version. 
            var user = new User()
            {
                UserId = 1,
                Name = "Leonardo Costa",
                Birthdate = new DateTime(1981, 04, 21),
                Email = "leoccosta@outlook.com",
                Password = "123456".Encrypt(),
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                Gender = Gender.Male,
                SSN = "05522078707",
                Address = new Address()
                {
                    Street = "Rua Ven�ncio Veloso",
                    Complement = "Ap 103",
                    Number = "180",
                    State = "RJ",
                    City = "Rio de Janeiro",
                    ZipCode = "22790420"
                }
            };

            user.AddCreditCard(Guid.NewGuid(), CreditCardBrand.Visa, "1111", 4, 2022);

            context.Users.AddOrUpdate(u => u.Email, user);

            context.Apps.AddOrUpdate(e => e.AppId,
              new App
              {
                  AppId = 1,
                  Developer = "WhatsApp Inc.",
                  Name = "WhatsApp",
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  ThumbUrl = @"https://lh3.ggpht.com/mp86vbELnqLi2FzvhiKdPX31_oiTRLNyeK8x4IIrbF5eD1D5RdnVwjQP0hwMNR_JdA=s180-rw",
                  Description = @"O WhatsApp Messenger � um aplicativo gratuito para a troca de mensagens. 
                    O WhatsApp pode ser usado para enviar mensagens e fazer chamadas 
                    para seus amigos e familiares.",
                  Price = 9.90M
              },
              new App
              {
                  AppId = 2,
                  Developer = "Facebook",
                  Name = "Facebook",
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  ThumbUrl = @"https://lh3.googleusercontent.com/ZZPdzvlpK9r_Df9C3M7j1rNRi7hhHRvPhlklJ3lfi5jk86Jd1s0Y5wcQ1QgbVaAP5Q=s180-rw",
                  Description = @"Manter-se em contato com os amigos est� mais r�pido que nunca.
                    Compartilhe atualiza��es, fotos e v�deos e receba notifica��es quando amigos curtem e comentam 
                    suas publica��es.",
                  Price = 5.40M
              },
              new App
              {
                  AppId = 3,
                  Developer = "Kiloo Arcade",
                  Name = "Subway Surfers",
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  ThumbUrl = @"https://lh3.googleusercontent.com/VJsamnFu9WTzjL6Zi8y0r5xIRcj4_9iBUofNveN2WD7IG6rYku3TB1TyRkUsRyq1aQ=s180-rw",
                  Description = @"ESCAPE o mais r�pido poss�vel!
                    EVITE os trens que v�m! 
                    Ajude Jake, Tricky e Fresh para escapar do Inspetor rabugento e o c�o dele.",
                  Price = 1.40M
              },
              new App
              {
                  AppId = 4,
                  Developer = "Netflix Inc.",
                  Name = "Netflix",
                  CreateDate = DateTime.Now,
                  ModifyDate = DateTime.Now,
                  ThumbUrl = @"https://lh3.googleusercontent.com/jcbqFma-5e91cY9MlEasA-fvCRJK493MxphrqbBd8oS74FtYg00IXeOAn0ahsLprxIA=s180-rw",
                  Description = @"A Netflix � l�der mundial em servi�o de assinatura de filmes e s�ries de 
                    TV para celulares. Este aplicativo foi criado para proporcionar a 
                    melhor experi�ncia poss�vel para voc�.",
                  Price = 7.40M
              }
            );
        }
    }
}
