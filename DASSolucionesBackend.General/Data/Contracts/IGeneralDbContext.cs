﻿using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;
using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Data.Contracts;

public interface IGeneralDbContext
{
    DbSet<DocumentType> DocumentTypes { get; }
    DbSet<Country> Countries { get; }
    DbSet<Region> Regions { get; }
    DbSet<Locality> Localities { get; }
    DbSet<VoucherType> VoucherTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}