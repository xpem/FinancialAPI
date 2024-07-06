using Financial.Infra.Repos.Interfaces;
using Financial.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Financial.Models.TransactionTypes;

namespace Financial.Infra.Repos
{
    public class InitializeDBRepo(FinancialDbContext inventoryDbContext) : IInitializeDBRepo
    {
        public void CreateInitialValues()
        {
            inventoryDbContext.Database.EnsureCreated();

            CreateBaseCategories();
            //CreateBaseSubCategories(inventoryDbContext);
            //CreateBaseItemSituation(inventoryDbContext);
            //CreateBaseAcquisitionType(inventoryDbContext);

            inventoryDbContext.SaveChanges();
        }

        private void CreateBaseCategories()
        {
            if (inventoryDbContext.Category.Count() is not 0) return;

            Category[] categories = [
                new Category() { Name = "Salário", Color = "#922B21", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },
                new Category() { Name = "13° salário", Color = "#943126", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },
                new Category() { Name = "Rembolso", Color = "#ABE2CE", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },
                new Category() { Name = "Juros de investimentos", Color = "#AFE2AB", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },
                new Category() { Name = "Férias", Color = "#DDE2AB", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },
                new Category() { Name = "Estorno", Color = "#E2ABAB", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Income },

                new Category() { Name = "Alimentação", Color = "#A93226", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Carro", Color = "#CB4335", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Casa", Color = "#884EA0", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                //new Category() { Name = "Gastos Pessoais", Color = "#E2ABAB", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransacaoType.Expense },
                new Category() { Name = "Lazer", Color = "#7D3C98", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Saúde", Color = "#2471A3", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Transporte", Color = "#2E86C1", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Vestimenta", Color = "#17A589", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Eletronicos", Color = "#BA4A00", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Assinaturas", Color = "#229954", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
                new Category() { Name = "Doações", Color = "#28B463", SystemDefault = true, CreatedAt = DateTime.Now,Type = TransactionType.Expense },
            ];

            inventoryDbContext.Category.AddRange(categories);
        }

        private void CreateBaseSubCategories()
        {
            if (inventoryDbContext.SubCategory.Count() is not 0) return;


        }
    }
}
