
namespace SMS
{
    
    using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;


	[Table("AdminCustomerMaster")]
	public sealed class AdminCustomerMaster : BaseModel
	{
		#region Properties

		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the FirstName value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string Name { get; set; }
		
		/// <summary>
		/// Gets or sets the FirstName value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the MiddleName value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string Organization { get; set; }

		/// <summary>
		/// Gets or sets the MiddleName value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string ProductName { get; set; }

		/// <summary>
		/// Gets or sets the Email value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the PhoneNo value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string Contact { get; set; }


		[StringLength(2048, ErrorMessage = "*")]
		public string ContryCode { get; set; }

		/// <summary>
		/// Gets or sets the LicenseCreationDate value.
		/// </summary>
		[DataType(DataType.Date)]

		public DateTime? LicenseCreationDate { get; set; }

		/// <summary>
		/// Gets or sets the LicenseCreationDate value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string LicenseExprieyDate { get; set; }

		/// <summary>
		/// Gets or sets the NationalityId value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string LicenseKey { get; set; }
		
		/// <summary>
		/// Gets or sets the NationalityId value.
		/// </summary>
		[StringLength(2048, ErrorMessage = "*")]
		public string MAC { get; set; }


		/// <summary>
		/// Gets or sets the IssueDate value.
		/// </summary>
		public DateTime? SoftwareRegistrationDate { get; set; }

		/// <summary>
		/// Gets or sets the IsActive value.
		/// </summary>
		public bool? IsApproved { get; set; }

		/// <summary>
		/// Gets or sets the CreatedDate value.
		/// </summary>
		


		public DateTime? CreatedDate { get; set; }



		/// <summary>
		/// Gets or sets the ModifiedDate value.
		/// </summary>
		[DataType(DataType.DateTime)]
		public DateTime? ModifiedDate { get; set; }


		/// <summary>
		/// Gets or sets the IsActive value.
		/// </summary>
		public bool? IsDeleted { get; set; }


		#endregion
	}
}
