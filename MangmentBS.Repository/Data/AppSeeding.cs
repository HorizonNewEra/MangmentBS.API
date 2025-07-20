using MangmentBS.Core.Entities;
using MangmentBS.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MangmentBS.Repository.Data
{
    public class AppSeeding
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                var Roledata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Roles.json");
                var Roles = JsonSerializer.Deserialize<List<Roles>>(Roledata);
                if (Roles is not null && Roles.Count() > 0)
                {
                    await context.Roles.AddRangeAsync(Roles);
                }
            }
            if (!context.Users.Any())
            {
                var Userdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Users.json");
                var Users = JsonSerializer.Deserialize<List<User>>(Userdata);
                if (Users is not null && Users.Count() > 0)
                {
                    await context.Users.AddRangeAsync(Users);
                }
            }
            if (!context.Building.Any())
            {
                var Buildingdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Building.json");
                var Buildings = JsonSerializer.Deserialize<List<Building>>(Buildingdata);
                if (Buildings is not null && Buildings.Count() > 0)
                {
                    await context.Building.AddRangeAsync(Buildings);
                }
            }
            if (!context.BuildingContractImages.Any())
            {
                var LandContractImagesdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\LandContractImage.json");
                var LandContractImages = JsonSerializer.Deserialize<List<BuildingContractImages>>(LandContractImagesdata);
                if (LandContractImages is not null && LandContractImages.Count() > 0)
                {
                    await context.BuildingContractImages.AddRangeAsync(LandContractImages);
                }
            }
            if (!context.Client.Any())
            {
                var Clientdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Client.json");
                var Clients = JsonSerializer.Deserialize<List<Client>>(Clientdata);
                if (Clients is not null && Clients.Count() > 0)
                {
                    await context.Client.AddRangeAsync(Clients);
                }
            }
            if (!context.Payment.Any())
            {
                var Paymentdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Payment.json");
                var Payments = JsonSerializer.Deserialize<List<Payment>>(Paymentdata);
                if (Payments is not null && Payments.Count() > 0)
                {
                    await context.Payment.AddRangeAsync(Payments);
                }
            }
            if (!context.Flat.Any())
            {
                var Flatdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Flat.json");
                var Flats = JsonSerializer.Deserialize<List<Flat>>(Flatdata);
                if (Flats is not null && Flats.Count() > 0)
                {
                    await context.Flat.AddRangeAsync(Flats);
                }
            }
            if (!context.FlatContractImages.Any())
            {
                var FlatContractImagesdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\FlatContractImage.json");
                var FlatContractImages = JsonSerializer.Deserialize<List<FlatContractImages>>(FlatContractImagesdata);
                if (FlatContractImages is not null && FlatContractImages.Count() > 0)
                {
                    await context.FlatContractImages.AddRangeAsync(FlatContractImages);
                }
            }
            await context.SaveChangesAsync();
            /*if (!context.Installment.Any())
            {
                var Installmentdata = File.ReadAllText(@"..\MangmentBS.Repository\Data\DataSeed\Installment.json");
                var Installments = JsonSerializer.Deserialize<List<Installment>>(Installmentdata);
                if (Installments is not null && Installments.Count() > 0)
                {
                    await context.Installment.AddRangeAsync(Installments);
                }
            }*/

            await context.SaveChangesAsync();
        }
    }
}
