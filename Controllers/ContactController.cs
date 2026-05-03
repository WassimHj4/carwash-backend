using CarwashBackend.Data;
using CarwashBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarwashBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(AppDbContext db) : ControllerBase
{
    // POST api/contact
    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var message = new ContactMessage
        {
            Name = dto.Name.Trim(),
            Email = dto.Email.Trim(),
            Phone = dto.Phone?.Trim() ?? string.Empty,
            Message = dto.Message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        db.ContactMessages.Add(message);
        await db.SaveChangesAsync();

        return Ok(new { message = "Votre message a bien été envoyé. Nous vous répondrons rapidement." });
    }
}

public record ContactDto(
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 2)]
    string Name,

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.EmailAddress]
    string Email,

    string? Phone,

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(2000, MinimumLength = 10)]
    string Message
);
