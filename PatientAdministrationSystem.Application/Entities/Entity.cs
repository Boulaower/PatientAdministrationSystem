using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAdministrationSystem.Application.Entities
{
    // Base entity class with a Guid identifier
    public abstract class Entity : Entity<Guid>
    {
        // Any additional common properties or behaviors for Guid-based entities can be added here
    }

    // Generic base entity class that supports different key types
    public abstract class Entity<TKey> where TKey : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        // This is automatically set to the current time when the entity is created
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        // This will be updated automatically when the entity is modified
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
