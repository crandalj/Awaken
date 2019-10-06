public class Stats
{
    public int ATTACK;
    public float HEALTH;
    public float MAX_HEALTH;
    public int SPEED;

    public Stats(int attack, int health, int speed)
    {
        this.ATTACK = attack;
        this.HEALTH = health;
        this.MAX_HEALTH = this.HEALTH;
        this.SPEED = speed;
    }

    public Stats()
    {
        this.ATTACK = 1;
        this.HEALTH = 1;
        this.MAX_HEALTH = this.HEALTH;
        this.SPEED = 1;
    }
         
}
