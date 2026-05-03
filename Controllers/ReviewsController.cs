using CarwashBackend.Data;
using CarwashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarwashBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(AppDbContext db) : ControllerBase
{
    // GET api/reviews  — returns only approved reviews
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reviews = await db.Reviews
            .Where(r => r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new { r.Id, r.Name, r.Rating, r.Text, r.CreatedAt })
            .ToListAsync();

        return Ok(reviews);
    }

    // POST api/reviews  — submit a new review (pending approval)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var review = new Review
        {
            Name = dto.Name.Trim(),
            Rating = dto.Rating,
            Text = dto.Text.Trim(),
            IsApproved = true,
            CreatedAt = DateTime.UtcNow
        };

        db.Reviews.Add(review);
        await db.SaveChangesAsync();

        return Ok(new { message = "Merci pour votre avis ! Il sera publié après validation." });
    }
}

public record CreateReviewDto(
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 2)]
    string Name,

    [System.ComponentModel.DataAnnotations.Range(1, 5)]
    int Rating,

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(1000, MinimumLength = 3)]
    string Text
);
