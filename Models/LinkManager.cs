using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Boilerplate.Models {

    public class LinkManager : DbContext  {

        private DbSet<Category> tblCategory { get; set; }
        private DbSet<Link> tblLink { get; set; }
        public List<Category> categories {
            get {
                return tblCategory.OrderBy(c => c.categoryId).ToList();
            }
        }

        public Category getCategoryByCategoryId(int categoryId) {
            return tblCategory.Where(c => c.categoryId == categoryId).FirstOrDefault();
        }

        public List<Link> getLinksByCategoryId(bool pinned, int categoryId) {
            return tblLink.Where(l => (l.categoryId == categoryId && l.pinned == pinned)).OrderBy(l => l.label).ToList();
        }

        public Link getLinkByLinkId(int linkId) {
            return tblLink.Where(l => l.linkId == linkId).FirstOrDefault();
        }

        public SelectList getSelectList() {
            List<Category> listData = tblCategory.OrderBy(c => c.categoryId).ToList();
            return new SelectList(listData, "categoryId", "categoryName");
        }

        // -------------------------------------------------- private methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(Connection.CONNECTION_STRING, new MySqlServerVersion(new Version(8, 0, 11)));
        }

    }

}