﻿using Financial.Infra.Repos.Interfaces;
using Financial.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infra.Repos
{
    public class CategoryRepository(FinancialDbContext dbContext) : ICategoryRepository
    {
        public List<Category>? Get(int uid) => [.. dbContext.Category.Where(x => x.UserId == uid || (x.UserId == null && x.SystemDefault))];

        public Category? GetById(int uid, int id) => dbContext.Category.Where(x => (x.UserId == uid || (x.UserId == null && x.SystemDefault)) && x.Id == id).FirstOrDefault();

        public Category? GetByName(int uid, string name) => dbContext.Category.Where(x => (x.UserId == uid || (x.UserId == null && x.SystemDefault)) && x.Name == name).FirstOrDefault();

        public int Create(Category category)
        {
            dbContext.Category.Add(category);
            return dbContext.SaveChanges();
        }

        public int Delete(Category category)
        {
            //dbContext.ChangeTracker?.Clear();

            dbContext.Category.Remove(category);

            return dbContext.SaveChanges();
        }

        public int Update(Category category)
        {
            //dbContext.ChangeTracker?.Clear();

            dbContext.Category.Update(category);

            return dbContext.SaveChanges();
        }

    }
}
