using UnityEngine;

public class UnitKilledArgs
{
    public GameObject unit;
}
public class VictoryEventArgs
{

}
public class DefeatEventArgs
{

}
public class ButtonPressedArgs
{
    public string name;
}
public class TowerBuildCommandArgs
{
    public int currentLevel;
}
public class TowerSelectionArgs
{
    
}
public class TooltipEventArgs
{
    public bool display;
    public Vector3 screenPosition;
    public string text;
}
public class WorldDragEvent
{
    public Vector3 delta;
    //направление, куда "тянет" мышь когда касается краев экрана
    public Vector2Int borderPnrDirection;
}