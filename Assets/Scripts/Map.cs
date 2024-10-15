using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour, IMapService
{
    [SerializeField] private List<Cell> cells;
    [SerializeField] private Cell _selectedCell;

    private Cell[,] map = new Cell[10, 10];
    private IMediatorMap _mediatorMap;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IMapService>(this);
    }

    public void Configurate(IMediatorMap mediatorMap)
    {
        _mediatorMap = mediatorMap;
        //fill the map
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var cell = cells[i * 10 + j];
                cell.Configure(new Position { x = i, y = j }, _mediatorMap.GetInputHandler());
                cell.OnCellClickedDown += c =>
                {
                    if (c.IsSelect()) return;
                    if (_selectedCell != c)
                    {
                        _selectedCell.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                        _selectedCell.IsSelect(false);
                        _selectedCell = c;
                        _selectedCell.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                        _selectedCell.IsSelect(true);
                    }
                };
                map[i, j] = cell;
            }
        }

        _selectedCell = map[0, 0];
        _selectedCell.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}