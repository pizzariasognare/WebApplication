using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CustomerAddressCreate",
                url: "CustomerAddress/Create/{customer_id}",
                defaults: new { controller = "CustomerAddress", action = "Create" }
            );

            routes.MapRoute(
                name: "CustomerCreateUser",
                url: "Customer/CreateUser/{customer_id}",
                defaults: new { controller = "Customer", action = "CreateUser" }
            );

            routes.MapRoute(
                name: "CustomerEditUser",
                url: "Customer/EditUser/{customer_id}",
                defaults: new { controller = "Customer", action = "EditUser" }
            );

            routes.MapRoute(
                name: "EmployerCreateUser",
                url: "Employer/CreateUser/{employer_id}",
                defaults: new { controller = "Employer", action = "CreateUser" }
            );

            routes.MapRoute(
                name: "EmployerEditUser",
                url: "Employer/EditUser/{employer_id}",
                defaults: new { controller = "Employer", action = "EditUser" }
            );

            routes.MapRoute(
                name: "OrderCreate",
                url: "Order/Create/{customer_id}",
                defaults: new { controller = "Order", action = "Create" }
            );

            routes.MapRoute(
                name: "OrderPizzaCreate",
                url: "OrderPizza/Create/{order_id}",
                defaults: new { controller = "OrderPizza", action = "Create" }
            );

            routes.MapRoute(
                name: "OrderPizzaEdit",
                url: "OrderPizza/Edit/{order_id}",
                defaults: new { controller = "OrderPizza", action = "Edit" }
            );

            routes.MapRoute(
                name: "OrderDrinkCreate",
                url: "OrderDrink/Create/{order_id}",
                defaults: new { controller = "OrderDrink", action = "Create" }
            );

            routes.MapRoute(
                name: "OrderDrinkEdit",
                url: "OrderDrink/Edit/{order_id}",
                defaults: new { controller = "OrderDrink", action = "Edit" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
