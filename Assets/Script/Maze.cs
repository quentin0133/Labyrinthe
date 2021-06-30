using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Maze : MonoBehaviour
{
    [Range(5, 50), SerializeField]
    private int width = 5;
    [Range(5, 50), SerializeField]
    private int height = 5;

    [SerializeField]
    private GameObject spriteWall;
    [SerializeField]
    private GameObject spriteCell;

    [SerializeField]
    private GameObject victory;

    [SerializeField]
    private UnityEvent endGeneration;

    private List<GameObject> _spriteClone;
    private Cell[,] _cells;
    private Wall[,] _verticalWall;
    private Wall[,] _horizontalWall;

    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }

    public void Start()
    {
        width = (int) Parameter.instance.Width;
        height = (int)Parameter.instance.Height;

        _spriteClone = new List<GameObject>();

        _cells = new Cell[Width, Height];
        _verticalWall = new Wall[Width + 1, Height];
        _horizontalWall = new Wall[Width, Height + 1];

        int id = 0;

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _cells[i, j] = new Cell(id);
                _verticalWall[i, j] = new Wall();
                _horizontalWall[i, j] = new Wall();
                id++;
            }
        }

        for (int i = 0; i < Width; i++)
        {
            _horizontalWall[i, Height] = new Wall();
        }

        for (int j = 0; j < Height; j++)
        {
            _verticalWall[Width, j] = new Wall();
        }

        victory.transform.position = new Vector2(Width - 1, Height - 1);

        Generate();
    }

    public void Generate()
    {
        // On détruit les clones s'il y en avait avant / We destroy the clones if there was before
        DestroyClone();

        // Génération des cellules / Generation of cells
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _cells[i, j].Sprite = Instantiate(spriteCell, new Vector3(i, j, 0), Quaternion.identity);
                _verticalWall[i, j].Sprite = Instantiate(spriteWall, new Vector3(i - 0.5f, j, 0), Quaternion.identity);
                _horizontalWall[i, j].Sprite = Instantiate(spriteWall, new Vector3(i, j - 0.5f, 0), new Quaternion(0, 0, 90, 90));

                _spriteClone.Add(_cells[i, j].Sprite);
                _spriteClone.Add(_verticalWall[i, j].Sprite);
                _spriteClone.Add(_horizontalWall[i, j].Sprite);

                _cells[i, j].Sprite.name = "Cell " + i + "/" + j;
                _verticalWall[i, j].Sprite.name = "Wall vertical " + i + "/" + j;
                _horizontalWall[i, j].Sprite.name = "Wall horizontal " + i + "/" + j;
            }
        }

        // Génération des murs horizontales / Generation of horizontal walls
        for (int i = 0; i < Width; i++)
        {
            _horizontalWall[i, Height].Sprite = Instantiate(spriteWall, new Vector3(i, Height - 0.5f, 0), new Quaternion(0, 0, 90, 90));
            _spriteClone.Add(_horizontalWall[i, Height].Sprite);
            _horizontalWall[i, Height].Sprite.name = "Wall horizontal " + i + "/" + Height;
        }

        // Génération des murs verticales / Generation of vertical walls
        for (int j = 0; j < Height; j++)
        {
            _verticalWall[Width, j].Sprite = Instantiate(spriteWall, new Vector3(Width - 0.5f, j, 0), Quaternion.identity);
            _spriteClone.Add(_verticalWall[Width, j].Sprite);
            _verticalWall[Width, j].Sprite.name = "Wall vertical " + Width  + "/" + j;
        }

        StartCoroutine("MakeMaze");
    }

    public IEnumerator MakeMaze()
    {
        bool reroll;
        bool sameId;

        int randomCellX;
        int randomCellY;

        Cell randomCell;
        Cell targetCell = null;

        int direction;
        int idTarget;

        while(!HasUniqueId()) {
            reroll = false;
            sameId = false;
            randomCellX = Random.Range(0, Width);
            randomCellY = Random.Range(0, Height);
            randomCell = _cells[randomCellX, randomCellY];
            do
            {
                direction = Random.Range(1, 5);
                if ((randomCellX == 0 && direction == 1)
                || (randomCellY == 0 && direction == 2)
                || (randomCellX == Width - 1 && direction == 3)
                || (randomCellY == Height - 1 && direction == 4))
                    reroll = true;
                else
                    reroll = false;
            } while (reroll);

            switch (direction)
            {
                case 1:
                    // Gauche
                    targetCell = _cells[randomCellX - 1, randomCellY];
                    if (targetCell.Id != randomCell.Id)
                        Destroy(_verticalWall[randomCellX, randomCellY].Sprite);
                    else
                        sameId = true;
                    break;
                case 2:
                    // Bas
                    targetCell = _cells[randomCellX, randomCellY - 1];
                    if (targetCell.Id != randomCell.Id)
                        Destroy(_horizontalWall[randomCellX, randomCellY].Sprite);
                    else
                        sameId = true;
                    break;
                case 3:
                    // Droite
                    targetCell = _cells[randomCellX + 1, randomCellY];
                    if (targetCell.Id != randomCell.Id)
                        Destroy(_verticalWall[randomCellX + 1, randomCellY].Sprite);
                    else
                        sameId = true;
                    break;
                case 4:
                    // Haut
                    targetCell = _cells[randomCellX, randomCellY + 1];
                    if (targetCell.Id != randomCell.Id)
                        Destroy(_horizontalWall[randomCellX, randomCellY + 1].Sprite);
                    else
                        sameId = true;
                    break;
            }

            idTarget = targetCell.Id;

            if (!sameId)
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        if (idTarget == _cells[i, j].Id)
                        {
                            _cells[i, j].Id = randomCell.Id;
                        }
                    }
                }
            }

            yield return new WaitForFixedUpdate();
        }

        endGeneration.Invoke();
    }

    public bool HasUniqueId()
    {
        int id = _cells[0, 0].Id;
        foreach(Cell cell in _cells)
        {
            if (id != cell.Id)
                return false;
        }
        return true;
    }

    public void DestroyClone()
    {
        int id = 0;

        _cells = new Cell[Width, Height];

        foreach (GameObject clone in _spriteClone)
        {
            Destroy(clone);
        }
        _spriteClone.Clear();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _cells[i, j] = new Cell(id);
                id++;
            }
        }
    }
}
