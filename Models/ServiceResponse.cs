namespace role_play_proj01.Models;

public class ServiceResponse<T>
{
    public T Date { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = null;
}