using UnityEngine;

[CreateAssetMenu(menuName = "Create PropertiesSo", fileName = "PropertiesSo", order = 0)]
public class PropertiesSo : ScriptableObject
{
    public string houseName;
    public string linkAddress = "https://www.google.com/";

    [Header("Order Settings")]
    public string orderIDType = "O";
    public int orderIDNumber = 2051920;
    public int orderIncreaseAmount = 50;
    public int orderMaxAmount = 800;
    
    [Header("Ingredient Settings")]
    public string ingredientIDType = "E";
    public int ingredientIDNumber = 575;

    [Header("OEE Settings")]
    public int minOee = 75;
    public int maxOee = 85;

    [Header("Range")] 
    public float rangeOfChange = 0.1f;

    public int stoveTemperature = 500;
    public int stoveWeight = 800;
    public int topMoldTemperature = 25;
    public int bottomMoldTemperature = 15;
    public float acceleration = 0.15f;
    public float pressure = 0.250f;
}
