namespace assigment_3
{

    

    internal class Driver
    {
        public string Name { get; set; }

        public Driver(string name)
        {
            Name = name;
        }
    }

    internal class Shipment
    {
        public string TrackingCode { get; set; }

        public string Description { get; set; }
        public double Weight { get; set; }
        public double DeliveryFee { get; set; }

        public virtual double EstimatedCost
        {
            get { return DeliveryFee + Weight * 5; }
        }

        public Shipment(string trackingCode, string description, double weight, double deliveryFee)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
        }

        public void UpdateWeight(double weight)
        {
            Weight = weight;
        }

        public void UpdateWeight(double weight, double packingWeight)
        {
            Weight = weight + packingWeight;
        }

        public virtual void PrintShipment()
        {
            Console.WriteLine("Tracking Code : " + TrackingCode);
            Console.WriteLine("Description : " + Description);
            Console.WriteLine("Weight : " + Weight + " KG");
            Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
            Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
        }
    }

    internal class StandardShipment : Shipment
    {
        public StandardShipment(string trackingCode, string description,
            double weight, double deliveryFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
        }

        public override void PrintShipment()
        {
            Console.WriteLine("Standard Shipment");
            base.PrintShipment();
        }
    }

    internal class ExpressShipment : Shipment
    {
        public double ExtraFee { get; set; }

        public ExpressShipment(string trackingCode, string description,
            double weight, double deliveryFee, double extraFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
            ExtraFee = extraFee;
        }

        public override double EstimatedCost
        {
            get { return DeliveryFee + Weight * 5 + ExtraFee; }
        }

        public override void PrintShipment()
        {
            Console.WriteLine("Express Shipment");
            Console.WriteLine("Tracking Code : " + TrackingCode);
            Console.WriteLine("Description : " + Description);
            Console.WriteLine("Weight : " + Weight + " KG");
            Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
            Console.WriteLine("Extra Fee : " + ExtraFee + " EGP");
            Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
        }
    }

    internal class InternationalShipment : Shipment
    {
        public string DestinationCountry { get; set; }
        public double CustomsFee { get; set; }

        public InternationalShipment(string trackingCode, string description,
            double weight, double deliveryFee, string country, double customsFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
            DestinationCountry = country;
            CustomsFee = customsFee;
        }

        public override double EstimatedCost
        {
            get { return DeliveryFee + Weight * 5 + CustomsFee; }
        }

        public override void PrintShipment()
        {
            Console.WriteLine("International Shipment");
            Console.WriteLine("Tracking Code : " + TrackingCode);
            Console.WriteLine("Description : " + Description);
            Console.WriteLine("Weight : " + Weight + " KG");
            Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
            Console.WriteLine("Destination Country : " + DestinationCountry);
            Console.WriteLine("Customs Fee : " + CustomsFee + " EGP");
            Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
        }

        public virtual void GenerateCustomsReport()
        {
            Console.WriteLine("Customs Report Generated");
        }
    }

    internal sealed class PriorityInternationalShipment : InternationalShipment
    {
        public PriorityInternationalShipment(string trackingCode, string description,
            double weight, double deliveryFee, string country, double customsFee)
            : base(trackingCode, description, weight, deliveryFee, country, customsFee)
        {
        }

        public override sealed void GenerateCustomsReport()
        {
            Console.WriteLine("Priority Customs Report Generated");
        }
    }

    internal class DeliveryCenter
    {
        private Shipment[] shipments;
        private int count;

        public Driver Driver { get; set; }

        public DeliveryCenter(int size)
        {
            shipments = new Shipment[size];
        }

        public void AddShipment(Shipment shipment)
        {
            shipments[count] = shipment;
            count++;
        }

        public void RemoveShipment(string code)
        {
            for (int i = 0; i < count; i++)
            {
                if (shipments[i].TrackingCode == code)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        shipments[j] = shipments[j + 1];
                    }

                    shipments[count - 1] = null;
                    count--;
                    break;
                }
            }
        }

        public Shipment this[int index]
        {
            get { return shipments[index]; }
            set { shipments[index] = value; }
        }

        public Shipment this[string code]
        {
            get
            {
                for (int i = 0; i < count; i++)
                {
                    if (shipments[i].TrackingCode == code)
                        return shipments[i];
                }

                return null;
            }
        }

        public void PrintAllShipments()
        {
            for (int i = 0; i < count; i++)
            {
                shipments[i].PrintShipment();
                Console.WriteLine();
            }
        }
    }

    internal static class DeliveryHelper
    {
        public static void PrintShipmentDetails(Shipment shipment)
        {
            shipment.PrintShipment();
        }
    }

    internal class Program
    {
        static void Main()
        {
            Driver driver = new Driver("Ahmed Mohamed");

            DeliveryCenter center = new DeliveryCenter(10);

            center.Driver = driver;

            StandardShipment standard =
                new StandardShipment("SH001", "Laptop", 3, 80);

            ExpressShipment express =
                new ExpressShipment("SH002", "Mobile Phone", 2, 60, 30);

            InternationalShipment international =
                new InternationalShipment(
                    "SH003", "Television", 8, 120, "Germany", 100);

            center.AddShipment(standard);
            center.AddShipment(express);
            center.AddShipment(international);

            Console.WriteLine("===============================");
            Console.WriteLine("Delivery Center");
            Console.WriteLine("================================");

            Console.WriteLine("Driver : " + center.Driver.Name);
            Console.WriteLine();

            center.PrintAllShipments();

            Console.WriteLine("==============================");
            Console.WriteLine("Printing Using DeliveryHelper...");
            Console.WriteLine();

            DeliveryHelper.PrintShipmentDetails(standard);
            Console.WriteLine();

            DeliveryHelper.PrintShipmentDetails(express);
            Console.WriteLine();

            DeliveryHelper.PrintShipmentDetails(international);

            Console.WriteLine();
            Console.WriteLine("==============================");
            Console.WriteLine("Updating Weight...");
            Console.WriteLine();

            Console.WriteLine("Original Weight : " + standard.Weight + " KG");

            standard.UpdateWeight(5);

            Console.WriteLine("Updated Weight : " + standard.Weight + " KG");

            standard.UpdateWeight(5, 0.5);

            Console.WriteLine("Updated Weight After Packing : "
                + standard.Weight + " KG");

            Console.WriteLine();
            Console.WriteLine("===============================");
            Console.WriteLine("Printing Using Shipment[]...");
            Console.WriteLine();

            Shipment[] list = { standard, express, international };

            for (int i = 0; i < list.Length; i++)
            {
                list[i].PrintShipment();
                Console.WriteLine();
            }

            PriorityInternationalShipment priority =
                new PriorityInternationalShipment(
                    "SH004", "Camera", 4, 100, "France", 80);

            priority.GenerateCustomsReport();

            
            
        }
    }






}
