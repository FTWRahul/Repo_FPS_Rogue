[System.Serializable]
public struct Damage
{
    public bool isLethal;

    public Damage(bool lethal)
    {
        isLethal = lethal;
    }
}