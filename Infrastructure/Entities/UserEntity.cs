﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;
}
