using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MongoDbContext _context;

    public UserController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var user = await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPut("by-id/{id}")]
    public async Task<IActionResult> UpdateUserById(string id, [FromBody] User userUpdate)
    {
        if (userUpdate == null)
        {
            return BadRequest("User update data is null");
        }

        var updateDefinitionBuilder = Builders<User>.Update;
        var updateDefinition = new List<UpdateDefinition<User>>();

        if (!string.IsNullOrEmpty(userUpdate.FirstName))
            updateDefinition.Add(updateDefinitionBuilder.Set(u => u.FirstName, userUpdate.FirstName));

        if (!string.IsNullOrEmpty(userUpdate.LastName))
            updateDefinition.Add(updateDefinitionBuilder.Set(u => u.LastName, userUpdate.LastName));

        if (!string.IsNullOrEmpty(userUpdate.Email))
            updateDefinition.Add(updateDefinitionBuilder.Set(u => u.Email, userUpdate.Email));

        if (!string.IsNullOrEmpty(userUpdate.PhoneNumber))
            updateDefinition.Add(updateDefinitionBuilder.Set(u => u.PhoneNumber, userUpdate.PhoneNumber));

        if (!string.IsNullOrEmpty(userUpdate.Username))
            updateDefinition.Add(updateDefinitionBuilder.Set(u => u.Username, userUpdate.Username));

        if (updateDefinition.Count == 0)
        {
            return BadRequest("No valid fields to update.");
        }

        var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinition);

        var result = await _context.Users.UpdateOneAsync(u => u.Id == id, combinedUpdate);

        if (result.MatchedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Endpoint to verify the password using the user ID
    [HttpPost("verify-password/{id}")]
    public async Task<IActionResult> VerifyPassword(string id, [FromBody] VerifyPasswordRequest request)
    {
        var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null || user.Password != request.CurrentPassword)
        {
            return BadRequest(new { isValid = false });
        }

        return Ok(new { isValid = true });
    }

    // Endpoint to change the password using the user ID
    [HttpPost("change-password/{id}")]
    public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordRequest request)
    {
        var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null || user.Password != request.CurrentPassword)
        {
            return BadRequest(new { message = "Current password is incorrect." });
        }

        var updateDefinition = Builders<User>.Update.Set(u => u.Password, request.NewPassword);

        var result = await _context.Users.UpdateOneAsync(u => u.Id == id, updateDefinition);

        if (result.ModifiedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    public class VerifyPasswordRequest
    {
        public string CurrentPassword { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
