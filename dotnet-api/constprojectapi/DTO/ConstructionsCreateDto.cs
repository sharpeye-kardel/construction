using System.ComponentModel.DataAnnotations;

namespace constprojectapi.Dto
{
    /// <summary>
    /// Data transfer object for creating new construction projects.
    /// Contains all required fields for construction project creation with validation attributes.
    /// </summary>
    public class ConstructionCreateDto
    {
        /// <summary>
        /// Gets or sets the name of the construction project.
        /// </summary>
        /// <remarks>Required field with a maximum length of 200 characters.</remarks>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the physical location or address of the construction project.
        /// </summary>
        /// <remarks>Required field with a maximum length of 500 characters.</remarks>
        [Required]
        [MaxLength(500)]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the current stage of the construction project.
        /// </summary>
        /// <remarks>
        /// Stage values: 1 = Concept, 2 = Design &amp; Documentation, 3 = Pre-Construction, 4 = Construction.
        /// For stages less than 4, the start date must be a future date.
        /// </remarks>
        [Required]
        public int Stage { get; set; }

        /// <summary>
        /// Gets or sets the category of the construction project.
        /// </summary>
        /// <remarks>Examples: Education, Health, Office, Other.</remarks>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the construction project.
        /// </summary>
        /// <remarks>Required field with a maximum length of 2000 characters.</remarks>
        [Required]
        [MaxLength(2000)]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the planned start date of the construction project.
        /// </summary>
        /// <remarks>
        /// Required field. For non-Construction stage projects (stage &lt; 4),
        /// this must be a future date.
        /// </remarks>
        [Required]
        public DateTime? StartDate { get; set; }
    }
}
