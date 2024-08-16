public class VerifyPasswordRequest
{
    public string Username { get; set; }
    public string CurrentPassword { get; set; }
}

public class UpdatePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
