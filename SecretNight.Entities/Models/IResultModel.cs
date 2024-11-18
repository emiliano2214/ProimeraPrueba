namespace SecretNight.Entities.Models
{
    public interface IResultModel
    {
        object Data { get; set; }
        string ErrorMessage { get; set; }
        bool isSuccess { get; set; }
    }
}