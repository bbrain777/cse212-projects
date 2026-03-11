/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
public static void Run() {
    Console.WriteLine("Test 1 - Invalid max size defaults to 10");
    var t1 = new CustomerService(0);
    Console.WriteLine(t1); // expect max_size=10
    Console.WriteLine("Defect(s) Found: none (constructor already correct)");
    Console.WriteLine("=================");

    Console.WriteLine("Test 2 - Full queue should reject add when size is 1");
    var t2 = new CustomerService(1);
    Console.SetIn(new StringReader("Tom\nA1\nLogin issue\nSue\nA2\nBilling issue\n"));
    t2.AddNewCustomer();
    t2.AddNewCustomer(); // expect error message: Maximum Number of Customers in Queue.
    Console.WriteLine(t2); // expect size=1
    Console.WriteLine("Defect(s) Found: fixed full-queue check (>=)");
    Console.WriteLine("=================");

    Console.WriteLine("Test 3 - Serve should be FIFO");
    var t3 = new CustomerService(3);
    Console.SetIn(new StringReader("First\nF1\nIssue 1\nSecond\nS2\nIssue 2\n"));
    t3.AddNewCustomer();
    t3.AddNewCustomer();
    t3.ServeCustomer(); // expect First...
    Console.WriteLine(t3); // expect only Second remains
    Console.WriteLine("Defect(s) Found: fixed serve order (capture first before remove)");
    Console.WriteLine("=================");

    Console.WriteLine("Test 4 - Serving empty queue should not crash");
    var t4 = new CustomerService(2);
    t4.ServeCustomer(); // expect: No customers in the queue.
    Console.WriteLine("Defect(s) Found: fixed empty-queue guard");
    Console.WriteLine("=================");
}


    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
private void ServeCustomer() {
    if (_queue.Count == 0) {
        Console.WriteLine("No customers in the queue.");
        return;
    }

    var customer = _queue[0];
    _queue.RemoveAt(0);
    Console.WriteLine(customer);
}

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
