using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RecipeManager recipeManager = new RecipeManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Recipe App!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. View all recipes");
                Console.WriteLine("3. Scale a recipe");
                Console.WriteLine("4. Reset a recipe");
                Console.WriteLine("5. Clear all recipes");
                Console.WriteLine("6. Exit");

                int choice = GetIntInput("Enter your choice (1-6): ");

                switch (choice)
                {
                    case 1:
                        recipeManager.AddRecipe();
                        break;
                    case 2:
                        recipeManager.DisplayRecipes();
                        break;
                    case 3:
                        recipeManager.ScaleRecipe();
                        break;
                    case 4:
                        recipeManager.ResetRecipe();
                        break;
                    case 5:
                        recipeManager.ClearRecipes();
                        break;
                    case 6:
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static int GetIntInput(string prompt)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return input;
        }
    }

    class RecipeManager
    {
        private List<Recipe> recipes;

        public RecipeManager()
        {
            recipes = new List<Recipe>();
        }

        public void AddRecipe()
        {
            Console.WriteLine("Enter a name for the recipe:");
            string name = GetValidString("Recipe name");

            int numIngredients = GetIntInput("Enter the number of ingredients:");
            List<Ingredient> ingredients = new List<Ingredient>();

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");
                string ingredientName = GetValidString("Ingredient name");
                double quantity = GetDoubleInput($"Enter the quantity for {ingredientName}:");
                string unit = GetValidString("Unit of measurement");
                int calories = GetIntInput($"Enter the number of calories in {ingredientName}:");
                string foodGroup = GetValidString("Food group");
                ingredients.Add(new Ingredient(ingredientName, quantity, unit, calories, foodGroup));
            }

            int numSteps = GetIntInput("Enter the number of steps:");
            List<string> steps = new List<string>();

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                steps.Add(Console.ReadLine());
            }

            recipes.Add(new Recipe(name, ingredients, steps));
            Console.WriteLine("Recipe added successfully!");
        }

        public void DisplayRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("All Recipes:");
            recipes.Sort((a, b) => a.Name.CompareTo(b.Name));

            foreach (var recipe in recipes)
            {
                Console.WriteLine($"- {recipe.Name}");
            }

            int selectedIndex = GetIntInput("Enter the index of the recipe you want to view (1-{recipes.Count}): ") - 1;

            if (selectedIndex < 0 || selectedIndex >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe index.");
                return;
            }

            Recipe selectedRecipe = recipes[selectedIndex];
            DisplayRecipe(selectedRecipe);
        }

        public void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("All Recipes:");
            recipes.Sort((a, b) => a.Name.CompareTo(b.Name));

            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int selectedIndex = GetIntInput("Enter the index of the recipe you want to scale (1-{recipes.Count}): ") - 1;

            if (selectedIndex < 0 || selectedIndex >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe index.");
                return;
            }

            Recipe selectedRecipe = recipes[selectedIndex];
            double scaleFactor = GetScaleFactor();
            selectedRecipe.ScaleRecipe(scaleFactor);
            Console.WriteLine("Recipe scaled successfully!");
            DisplayRecipe(selectedRecipe);
        }

        public void ResetRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("All Recipes:");
            recipes.Sort((a, b) => a.Name.CompareTo(b.Name));

            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int selectedIndex = GetIntInput("Enter the index of the recipe you want to reset (1-{recipes.Count}): ") - 1;

            if (selectedIndex < 0 || selectedIndex >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe index.");
                return;
            }

            Recipe selectedRecipe = recipes[selectedIndex];
            selectedRecipe.ResetRecipe();
            Console.WriteLine("Recipe reset successfully!");
            DisplayRecipe(selectedRecipe);
        }

        public void ClearRecipes()
        {
            recipes.Clear();
            Console.WriteLine("All recipes have been cleared.");
        }

        private void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i]}");
            }
            Console.WriteLine($"Total Calories: {recipe.TotalCalories}");
            if (recipe.TotalCalories > 300)
            {
                Console.WriteLine("Warning: This recipe exceeds 300 calories.");
            }
        }

        private double GetScaleFactor()
        {
            Console.WriteLine("Enter the scale factor (0.5, 2, or 3):");
            double scaleFactor;
            while (!double.TryParse(Console.ReadLine(), out scaleFactor) || (scaleFactor != 0.5 && scaleFactor != 2 && scaleFactor != 3))
            {
                Console.WriteLine("Invalid scale factor. Please enter 0.5, 2, or 3.");
            }
            return scaleFactor;
        }

        private string GetValidString(string prompt)
        {
            string input;
            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine().Trim();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private int GetIntInput(string prompt)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return input;
        }

        private double GetDoubleInput(string prompt)
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return input;
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public double OriginalTotalCalories { get; private set; }
        public double TotalCalories { get; private set; }

        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
            CalculateTotalCalories();
            OriginalTotalCalories = TotalCalories;
        }

        public void ScaleRecipe(double scaleFactor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= scaleFactor;
            }
            CalculateTotalCalories();
        }

        public void ResetRecipe()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
            CalculateTotalCalories();
        }

        private void CalculateTotalCalories()
        {
            TotalCalories = Ingredients.Sum(i => i.Calories);
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}