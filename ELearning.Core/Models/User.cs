﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Models
{
	public class User:IdentityUser<int>
	{
		[Required]
		public string FirstName { get; set; } = null!;
		[Required]
		public string LastName { get; set; } = null!;
		public string? Avatar { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		
	}
}
