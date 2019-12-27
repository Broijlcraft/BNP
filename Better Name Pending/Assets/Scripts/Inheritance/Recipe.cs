using System;
using UnityEngine;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

// Custom serializable class
[Serializable]
public class Ingredient {
    public string name;
    public int amount = 1;
    public IngredientUnit unit;
}
[Serializable]
public class Testttttt {
    public string yes;
    public string no;
    public bool nono;
}

public class Recipe : MonoBehaviour {
    public Ingredient potionResult;
    public Testttttt testttttt;
    public Testttttt testtttttt;
    public Ingredient[] potionIngredients;
}