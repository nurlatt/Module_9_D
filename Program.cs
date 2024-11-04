//1
public abstract class Beverage
{
    public abstract double Cost(); // Метод для расчета стоимости напитка
    public abstract string GetDescription(); // Метод для получения описания напитка
}
public class Espresso : Beverage
{
    public override double Cost()
    {
        return 50.0; // Базовая стоимость эспрессо
    }

    public override string GetDescription()
    {
        return "Espresso";
    }
}

public class Tea : Beverage
{
    public override double Cost()
    {
        return 30.0; // Базовая стоимость чая
    }

    public override string GetDescription()
    {
        return "Tea";
    }
}
public abstract class BeverageDecorator : Beverage
{
    protected Beverage _beverage;

    public BeverageDecorator(Beverage beverage)
    {
        _beverage = beverage;
    }

    public override double Cost()
    {
        return _beverage.Cost(); // Декоратор делегирует вызов метода стоимости напитка
    }

    public override string GetDescription()
    {
        return _beverage.GetDescription(); // Декоратор делегирует описание напитка
    }
}
public class Milk : BeverageDecorator
{
    public Milk(Beverage beverage) : base(beverage) { }

    public override double Cost()
    {
        return base.Cost() + 10.0; // Добавляем стоимость молока
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Milk";
    }
}

public class Sugar : BeverageDecorator
{
    public Sugar(Beverage beverage) : base(beverage) { }

    public override double Cost()
    {
        return base.Cost() + 5.0; // Добавляем стоимость сахара
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar";
    }
}

public class WhippedCream : BeverageDecorator
{
    public WhippedCream(Beverage beverage) : base(beverage) { }

    public override double Cost()
    {
        return base.Cost() + 15.0; // Добавляем стоимость взбитых сливок
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Whipped Cream";
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Создаем базовый напиток — эспрессо
        Beverage beverage = new Espresso();
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.Cost()}");

        // Добавляем молоко
        beverage = new Milk(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.Cost()}");

        // Добавляем сахар
        beverage = new Sugar(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.Cost()}");

        // Добавляем взбитые сливки
        beverage = new WhippedCream(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.Cost()}");
    }
}

//2
public interface IPaymentProcessor
{
    void ProcessPayment(double amount); // Обработка платежа
}
public class PayPalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Обработка платежа {amount} через PayPal.");
    }
}
public class StripePaymentService
{
    public void MakeTransaction(double totalAmount)
    {
        Console.WriteLine($"Обработка транзакции на {totalAmount} с помощью Stripe.");
    }
}
public class StripePaymentAdapter : IPaymentProcessor
{
    private StripePaymentService _stripePaymentService;

    public StripePaymentAdapter(StripePaymentService stripePaymentService)
    {
        _stripePaymentService = stripePaymentService;
    }

    public void ProcessPayment(double amount)
    {
        _stripePaymentService.MakeTransaction(amount); // Адаптируем метод Stripe к интерфейсу IPaymentProcessor
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Используем PayPal для обработки платежа
        IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
        paypalProcessor.ProcessPayment(100.0);

        // Используем Stripe через адаптер
        StripePaymentService stripeService = new StripePaymentService();
        IPaymentProcessor stripeProcessor = new StripePaymentAdapter(stripeService);
        stripeProcessor.ProcessPayment(200.0);
    }
}
