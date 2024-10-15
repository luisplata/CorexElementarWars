using UnityEngine;

public static class Global
{
    public static Color core = Color.gray;
    public static Color summon = Color.cyan;
    public static Color swamp = Color.black;
    public static Color boosterPlus = Color.blue;
    public static Color boosterplusplus = Color.magenta;
    public static Color empty = Color.white;
    public static Color disable = Color.clear;

    public static Color GetColor(TypeOfCell typeOfCell)
    {
        return typeOfCell switch
        {
            TypeOfCell.Core => core,
            TypeOfCell.Summon => summon,
            TypeOfCell.Swamp => swamp,
            TypeOfCell.BoosterPlus => boosterPlus,
            TypeOfCell.Boosterplusplus => boosterplusplus,
            TypeOfCell.Empty => empty,
            TypeOfCell.Disable => disable,
            _ => empty
        };
    }
}