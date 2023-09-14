//-------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID.Library
{
    public class Program
    {
        private static List<Product> productCatalog = new List<Product>();
        private static List<Equipment> equipmentCatalog = new List<Equipment>();

        public static void Main(string[] args)
        {
            PopulateCatalogs();

            Recipe recipe = new Recipe();
            recipe.FinalProduct = GetProduct("Café con leche");
            recipe.AddStep(new Step(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120));
            recipe.AddStep(new Step(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60));

            double costoTotalProduccion = CalculateProductionCost(recipe);

            Console.WriteLine($"El costo total de producir 1 '{recipe.FinalProduct.Description}' es: {costoTotalProduccion}");
        }

        private static void PopulateCatalogs()
        {
            AddProductToCatalog("Café", 100);
            AddProductToCatalog("Leche", 200);
            AddProductToCatalog("Café con leche", 300);

            AddEquipmentToCatalog("Cafetera", 1000);
            AddEquipmentToCatalog("Hervidor", 2000);
        }

        private static void AddProductToCatalog(string description, double unitCost)
        {
            productCatalog.Add(new Product(description, unitCost));
        }

        private static void AddEquipmentToCatalog(string description, double hourlyCost)
        {
            equipmentCatalog.Add(new Equipment(description, hourlyCost));
        }

        private static Product GetProduct(string description)
        {
            return productCatalog.Find(product => product.Description == description);
        }

        private static Equipment GetEquipment(string description)
        {
            return equipmentCatalog.Find(equipment => equipment.Description == description);
        }

        private static double CalculateProductionCost(Recipe recipe)
        {
            double costoInsumos = 0;
            foreach (Step step in recipe.Steps)
            {
                costoInsumos += step.Input.UnitCost * step.Quantity;
            }
            double costoEquipamiento = 0;
            foreach (Step step in recipe.Steps)
            {
                double costoHoraEquipo = step.Equipment.HourlyCost;
                double tiempoUsoEquipoHoras = step.Time / 60.0;
                costoEquipamiento += tiempoUsoEquipoHoras * costoHoraEquipo;
            }

            double costoTotalProduccion = costoInsumos + costoEquipamiento;
            Console.WriteLine ($"Costo total de producción: {costoTotalProduccion}"); 
            return costoTotalProduccion;
        }
        
    }
}
//*Es la clase experta en devolver resultados en base a las demas clases.