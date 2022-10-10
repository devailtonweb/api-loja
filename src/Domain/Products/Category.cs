using Flunt.Validations;

namespace AppStore.Domain.Products;

public class Category : Entity
{
    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();

    }

    public string Name { get; private set; }
    public bool Active { get; private set; }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
                    .IsGreaterOrEqualsThan(Name, 3, "Name", "Nome tem ser maior que 3 caracteres")
                    .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "Nome do criador é obrigatório")
                    .IsNotNullOrEmpty(EditedBy, "EditedBy", "Nome do editor é obrigatório");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy)
    {
        Name = name;
        Active = active;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }

}
