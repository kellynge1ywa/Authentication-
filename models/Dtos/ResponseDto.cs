namespace authentication;

public class ResponseDto
{
    public string Error { get; set; } = "";
    public object Result { get; set; } = default!;
    public bool Success { get; set; } = true;

}
