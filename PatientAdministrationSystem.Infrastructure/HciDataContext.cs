using Microsoft.EntityFrameworkCore;
using PatientAdministrationSystem.Application.Entities;

namespace PatientAdministrationSystem.Infrastructure
{
    public class HciDataContext : DbContext, IHciDataContext
    {
        public HciDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PatientEntity> Patients { get; set; } = null!;
        public DbSet<HospitalEntity> Hospitals { get; set; } = null!;
        public DbSet<VisitEntity> Visits { get; set; } = null!;
        public DbSet<PatientHospitalRelation> PatientHospitalRelations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure HospitalEntity
            modelBuilder.Entity<HospitalEntity>()
                .HasData(
                    new HospitalEntity
                    {
                        Id = new Guid("9ca78c33-4590-43c1-a7c4-55696a5efd44"), // Consistent with PatientHospitalRelation
                        Name = "Default Hospital"
                    }
                );

            // Configure PatientEntity
            modelBuilder.Entity<PatientEntity>()
                .HasData(
                    new PatientEntity
                    {
                        Id = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                        FirstName = "Aliaksandr",
                        LastName = "Huzen",
                        Email = "huzen.av@gmail.com"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("1ec2d3f7-8aa8-4bf5-91b8-045378919049"),
                        FirstName = "Vinny",
                        LastName = "Lawlor",
                        Email = "vinny.lawlor@hci.care"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("3c4e2e9c-7b2c-4b58-b2c7-1aefb215c0bf"),
                        FirstName = "Pauline",
                        LastName = "O'Connor Molloy",
                        Email = "pauline.oconnor@hci.care"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("6f9d4f7c-43df-4e1d-8dfe-bde35fa4beef"),
                        FirstName = "Mikey",
                        LastName = "Molloy",
                        Email = "mikey.molloy@hci.care"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("72c89f12-466f-423b-9b99-120739cdc2f2"),
                        FirstName = "Sean",
                        LastName = "Molloy",
                        Email = "sean.molloy@hci.care"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("e1d7a6f3-2545-46b2-865f-8a3452c2cb14"),
                        FirstName = "Liam",
                        LastName = "Murphy",
                        Email = "liam.murphy@hci.care"
                    },
                    new PatientEntity
                    {
                        Id = new Guid("045e2f9e-61b4-4e3b-b7c3-46d6a7f423f3"),
                        FirstName = "Emma",
                        LastName = "Ryan",
                        Email = "emma.ryan@hci.care"
                    }
                );

            // Configure VisitEntity
            modelBuilder.Entity<VisitEntity>()
                .HasData(
                    new VisitEntity
                    {
                        Id = new Guid("a7a5182a-995c-4bce-bce0-6038be112b7b"),
                        Date = new DateTime(2023, 08, 22)
                    }
                );

            // Configure PatientHospitalRelation
            modelBuilder.Entity<PatientHospitalRelation>()
                .HasKey(x => new { x.PatientId, x.HospitalId, x.VisitId });

            modelBuilder.Entity<PatientHospitalRelation>()
                .HasData(
                    new PatientHospitalRelation
                    {
                        PatientId = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                        HospitalId = new Guid("9ca78c33-4590-43c1-a7c4-55696a5efd44"), // Matching Hospital ID
                        VisitId = new Guid("a7a5182a-995c-4bce-bce0-6038be112b7b") // Matching Visit ID
                    }
                );

            // Configure relationships
            modelBuilder.Entity<PatientEntity>()
                .HasMany(x => x.PatientHospitals)
                .WithOne(x => x.Patient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HospitalEntity>()
                .HasMany(x => x.PatientHospitals)
                .WithOne(x => x.Hospital)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VisitEntity>()
                .HasMany(x => x.PatientHospitals)
                .WithOne(x => x.Visit)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
