﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectCollection.WebUI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectCollectionEntities : DbContext
    {
        public ProjectCollectionEntities()
            : base("name=ProjectCollectionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CustomProject> CustomProject { get; set; }
        public virtual DbSet<data_dictionary> data_dictionary { get; set; }
        public virtual DbSet<ProjectPlan> ProjectPlan { get; set; }
        public virtual DbSet<user_info> user_info { get; set; }
        public virtual DbSet<role_authority> role_authority { get; set; }
        public virtual DbSet<user_authority> user_authority { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectPlanRecord> ProjectPlanRecord { get; set; }
        public virtual DbSet<ProjectRecord> ProjectRecord { get; set; }
        public virtual DbSet<BatchProject> BatchProject { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TempCourse> TempCourse { get; set; }
        public virtual DbSet<TempCustomer> TempCustomer { get; set; }
        public virtual DbSet<TempOrder> TempOrder { get; set; }
    }
}
