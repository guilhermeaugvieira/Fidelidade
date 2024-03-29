﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Context;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
}