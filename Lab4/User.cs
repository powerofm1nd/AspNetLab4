namespace Lab4;
public class User
{
    public int id {get; set;}
    public string name {get; set;}

    public override string ToString()
    {
        return "Id: " + id + " " + "Name: " + name + "\n";
    }
}