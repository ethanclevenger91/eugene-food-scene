using System;
using System.Collections.Generic;

namespace EugeneFoodScene.Data
{

    // Places, Categories, Cuisines, Delivery Services, Order Delivery Links

    /// <summary>
    /// Name, Category, Phone, URL, Address, Notes, Order Delivery Links, Menu,
    /// </summary>
    public class Place
    {
        public string Name { get; set; }
        public List<String> Category { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public List<OrderDeliveryLink> OrderDeliveryLinks { get; set; }
        public string Menu { get; set; }
        
    }

    /// <summary>
    /// Category, Places, 
    /// </summary>
    public class Category
    {
        public string Name { get; set; }
        public List<Place> Places { get; set; }
    }

    public class Cuisine
    {
        public string Name { get; set; }
        public List<Place> Places { get; set; }
    }

    public class DeliveryService
    {
        public string Name { get; set; }
        public List<Place> Places { get; set; }

        public List<OrderDeliveryLink> OrderDeliveryLinks { get; set; }
    }

    public class OrderDeliveryLink
    {
        public string URL { get; set; }
        public List<Place> Places { get; set; }

        public List<DeliveryService> DeliveryServices { get; set; }
    }
}
