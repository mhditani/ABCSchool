﻿using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{

    public abstract class BaseDbContext : MultiTenantIdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
    {

        private new ABCSchoolTenantInfo TenantInfo { get; set; }

        protected BaseDbContext(IMultiTenantContextAccessor<ABCSchoolTenantInfo> tenantInfoContextAccessor, DbContextOptions options) : base(tenantInfoContextAccessor, options)
        {
            TenantInfo = tenantInfoContextAccessor.MultiTenantContext.TenantInfo;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!string.IsNullOrEmpty(TenantInfo?.ConnectionString))
            {
                optionsBuilder.UseSqlServer(TenantInfo.ConnectionString, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            }
        }
    }
}
