namespace Domain.Entities;

public class Comment
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string AdId { get; set; }
    public DateTime DateOfComment { get; set; }
    public string Text { get; set; }
}
