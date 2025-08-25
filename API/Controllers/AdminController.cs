using System;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AdminController(UserManager<AppUser> userManager) : BaseApiController
{
    [HttpGet("users-with-roles")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await userManager.Users.OrderBy(x => x.UserName)
        .Select(x => new
        {
            x.Id,
            Username = x.UserName,
            Roles = x.UserRoles.Select(r => r.Role.Name).ToList()
        }).ToListAsync();

        return Ok(users);
    }

     [HttpGet("photos-to-moderate")]
    [Authorize(Policy = "ModeratePhotoRole")]
    public ActionResult GetPhotosForModeration()
    {
        return Ok("Only admins or moderators can see this");
    }
}
