using UnityEngine;

[CreateAssetMenu(fileName = "ScrapDataSO", menuName = "Scriptable Objects/ScrapItem")]
public class ScrapDataSO : ScriptableObject
{
    public string ScrapName;
    public Sprite ScrapIcon;
    public RocketScrapType ScrapType;
}
