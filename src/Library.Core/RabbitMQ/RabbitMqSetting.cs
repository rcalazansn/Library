namespace Library.Core.RabbitMQ;

public class RabbitMqSetting
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Port => 5672;
    
    public override string ToString()
        => $"{HostName} : {Port} - {UserName}";
}