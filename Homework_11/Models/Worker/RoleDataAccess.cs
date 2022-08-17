namespace Homework_11.Models.Worker;

public class RoleDataAccess
{
    public bool AddClient { get; }
    public bool DelClient { get; }
    public bool EditClient { get; }
    
    public RoleDataAccess(bool addClient, bool delClient, bool editClient)
    {
        AddClient = addClient;
        DelClient = delClient;
        EditClient = editClient;
    }
}