using EFC_.Models.Entities;

namespace EFC_.Services;

internal class MenuService
{
    //Dessa fält är 'initialized' genom att skapa nya object med hjälp av 'new()' 
    private readonly CustomerService _customerService = new();
    private readonly CaseService _caseService = new();

    //Metoden skapar en huvudmeny i consolen,med sex val.I metoden finns det en while-loop som kör tills användaren väljer "6"
    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Welcome to Folksam!");
            Console.WriteLine("Choose your option:");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Add new case");
            Console.WriteLine("3. List all cases");
            Console.WriteLine("4. Update case status");
            Console.WriteLine("5. Show case deatils");
            Console.WriteLine("6. Exit");
            var choice = Console.ReadLine();

            //I switchen finns det 5 metoder och en som avslutar programmet om användaren väljer det.
            //Och en 'defulat' som skirver ut ett meddelande om användaren väljer nåt annat valen
            switch (choice)
            {
                case "1":
                    await AddCustomerAsync();
                    break;
                case "2":
                    await AddCaseAsync();
                    break;

                case "3":
                    await ListCasesAsync();
                    break;
                case "4":
                    await UpdateCaseStatusAsync();
                    break;
                case "5":
                    await ShowCaseDetailsAsync();
                    break;

                case "6":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
            Console.ReadKey();
        }

    }
    //Metoden är märkt med 'async' det innebär att det finns 'await' uttryck för att anropa andra metoder
    //SKrivs ut i consolen för användaren att skirva in sina uppgifter
    private async Task AddCustomerAsync()
    {
        var _customer = new CustomerEntity();
        Console.Clear();
        Console.WriteLine("\nEnter cutomer information:");
        Console.WriteLine("First name:");
        _customer.FirstName = Console.ReadLine() ?? "";
        Console.WriteLine("Last name:");
        _customer.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("E-mail:");
        _customer.Email = Console.ReadLine() ?? "";
        Console.WriteLine("Phone number:");
        _customer.PhoneNumber = Console.ReadLine() ?? "";

        await _customerService.CreateAsync(_customer);
        Console.WriteLine("Customer created");

    }

    //Metoden är märkt med 'async' det innebär att det finns 'await' uttryck för att anropa andra metoder
    //SKrivs ut i consolen för användaren att skirva in sina uppgifter
    public async Task AddCaseAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter case details:");

        Console.Write("Customer email: ");
        var email = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        CustomerEntity customer = await _customerService.GetAsync(x => x.Email == email);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        //Skapar en ny exemplar av 'CaseEntity' klassen och tilldelar den till variabeln 'newCase'
        var newCase = new CaseEntity
        {
            Description = description,
            CustomerId = customer.Id,
            Status = "New"
        };

        //'Await' finns för att anropa 'async' metoden 
        await _caseService.CreateAsync(newCase);

        Console.WriteLine("Case added successfully.");



    }

    //Metoden listar upp alla nuvarande ärenden som finns i databasen 
    public async Task ListCasesAsync()
    {
        Console.WriteLine("Loading...");
        var cases = await _caseService.GetAllAsync();
        Console.Clear();
        foreach (var caseEntity in cases)
        {
            Console.WriteLine($"Case ID: {caseEntity.Id}");
            Console.WriteLine($"Customer name: {caseEntity.Customer.FirstName} {caseEntity.Customer.LastName} ");
            Console.WriteLine($"Case desription: {caseEntity.Description}");
            Console.WriteLine($"Case Status: {caseEntity.Status}");
            Console.WriteLine(new string('-', Console.WindowWidth));
        }

    }
    //Metoden uppdaterar statusen som finns i databasen skrivs ut i consolen 
    public async Task UpdateCaseStatusAsync()
    {
        Console.Clear();
        // Användaren måste ange sin ID, om ID:et inte matchar får användaren ett felmeddelande 
        await ListCasesAsync();
        Console.Write("Enter case ID: ");
        int.TryParse(Console.ReadLine() ?? "", out var caseId);

        var existingCase = await _caseService.GetAsync(c => c.Id == caseId);
        if (existingCase == null)
        {
            Console.WriteLine($"Case with ID {caseId} not found");
            return;

        }
        Console.WriteLine($"Found case {caseId} with status: {existingCase.Status}");

        //Skrivs ut i consolen för att ange statusen av sitt ärenden 
        Console.WriteLine("Choose new status: ");
        Console.WriteLine("1. New");
        Console.WriteLine("2. In Progress");
        Console.WriteLine("3. Finished");
        var choice = Console.ReadLine();


        switch (choice)
        {

            case "1":
                await _caseService.UpdateStatusAsync(caseId, "New");
                break;

            case "2":
                await _caseService.UpdateStatusAsync(caseId, "In Progress");
                break;

            case "3":
                await _caseService.UpdateStatusAsync(caseId, "Finished");
                break;

            default:
                Console.WriteLine("Invalid choice, please try again.");
                return;
        }

        Console.WriteLine("Status updated");

    }


    //Metoden visar detaljer från användarens/kundens ärenden men måste ange för sin ID som skrivs ut i consolen 
    //Medtoden läser in det användaren ID och konventarar till en 'int' med hjälp av 'int.TryParse'
    public async Task ShowCaseDetailsAsync()
    {
        Console.Clear();

        await ListCasesAsync();
        Console.Write("Enter case ID: ");
        int.TryParse(Console.ReadLine() ?? "", out var caseId);

        var existingCase = await _caseService.GetAsync(c => c.Id == caseId);
        if (existingCase == null)
        {
            Console.WriteLine($"Case with ID {caseId} not found");
            return;

        }
        Console.Clear();
        Console.WriteLine($"Customer name: {existingCase.Customer.FirstName} {existingCase.Customer.LastName} ");
        Console.WriteLine($"Customer E-mail: {existingCase.Customer.Email}");
        Console.WriteLine($"Customer Phone number: {existingCase.Customer.PhoneNumber}");
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine($"Case ID: {existingCase.Id}");
        Console.WriteLine($"Case desription: {existingCase.Description}");
        Console.WriteLine($"Case Status: {existingCase.Status}");
        Console.WriteLine($"Case created: {existingCase.CreatedTime}");
        Console.WriteLine(new string('-', Console.WindowWidth));


    }
}
