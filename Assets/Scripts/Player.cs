using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Variables

    private int level;
    private int butterflyCount;
    private int coins;
    private int currentLine;
    private bool isDead;
    private Vector3 position;

    private Vector3 velocity;

    private static Player player;

    #endregion

    #region Functions

    #region Constructors

    private Player()
    {
        coins = 0;
        butterflyCount = 0;
        level = 1;

        isDead = false;
    }

    #endregion

    #region Get Functions


    public int getButterflyCount()
    {
        return butterflyCount;
    }

    public static Player getInstance()
    {
        if (player == null)
        {
            player = new Player();
        }
        return player;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }

    public Vector3 getVelocity()
    {
        return this.velocity;
    }

    public int getCurrentLine()
    {
        return currentLine;
    }

    public bool getIsDead()
    {
        return isDead;
    }
    #endregion

    #region Set Functions

    public void setLevel(int level)
    {
        this.level = level;
    }

    public void setCoins(int coins)
    {
        this.coins = coins;
    }

    public void setButterflyCount(int butterflyCount)
    {
        this.butterflyCount = butterflyCount;
    }

    public void setPosition(Vector3 position)
    {
        this.position = position;
    }

    public void setVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void setCurrentLine(int currentLine)
    {
        this.currentLine = currentLine;
    }

    public void setIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    #endregion

    #endregion


}
