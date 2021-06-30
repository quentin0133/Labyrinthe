using UnityEngine;

public class Cell
{
    private int _id;
    private GameObject _sprite;

    public Cell(int id)
    {
        Id = id;
    }

    public int Id { get => _id; set => _id = value; }
    public GameObject Sprite { get => _sprite; set => _sprite = value; }
}
