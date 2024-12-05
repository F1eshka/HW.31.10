namespace Maze
{
    public class Maze
    {
        public LevelForm Parent { get; set; } 
        public Cell[,] cells;
        public static Random r = new Random();

        public Maze(LevelForm parent)
        {
            Parent = parent;
            cells = new Cell[Configuration.Rows, Configuration.Columns];
        }

        public void Generate()
        {
            for (ushort row = 0; row < Configuration.Rows; row++)
            {
                for (ushort col = 0; col < Configuration.Columns; col++)
                {
                    CellType cell = CellType.HALL;

                    if (r.Next(5) == 0)
                    {
                        cell = CellType.WALL;
                    }

                    if (r.Next(250) == 0)
                    {
                        cell = CellType.MEDAL;
                    }

                    if (r.Next(250) == 0)
                    {
                        cell = CellType.ENEMY;
                    }

                    if (row == 0 || col == 0 ||
                        row == Configuration.Rows - 1 ||
                        col == Configuration.Columns - 1)
                    {
                        cell = CellType.WALL;
                    }

                    if (col == Parent.Hero.PosX &&
                        row == Parent.Hero.PosY)
                    {
                        cell = CellType.HERO;
                    }

                    if (col == Parent.Hero.PosX + 1 &&
                        row == Parent.Hero.PosY ||
                        col == Configuration.Columns - 1 &&
                        row == Configuration.Rows - 3)
                    {
                        cell = CellType.HALL;
                    }

                    cells[row, col] = new Cell(cell);

                    var picture = new PictureBox();
                    picture.Name = "pic" + row + "_" + col;
                    picture.Width = Configuration.PictureSide;
                    picture.Height = Configuration.PictureSide;
                    picture.Location = new Point(
                        col * Configuration.PictureSide,
                        row * Configuration.PictureSide);

                    picture.BackgroundImage = cells[row, col].Texture;
                    picture.Visible = false;
                    Parent.Controls.Add(picture);
                }
            }
        }
        public void Show()
        {
            foreach (var control in Parent.Controls)
            {
                if (control is PictureBox)
                    ((PictureBox)control).Visible = true;
            }
        }
    }
}