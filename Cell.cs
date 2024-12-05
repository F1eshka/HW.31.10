namespace Maze
{
    public class Character
    {
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public LevelForm Parent { get; set; }
        public int MedalCount { get; set; }
        public int StepCount { get; set; }
        public int AmountOfHealth { get; set; }

        public Character(LevelForm parent)
        {
            PosX = 0;
            PosY = 2;
            Parent = parent;
        }

        public void Clear()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HALL)];
        }

      

        public void Move(int deltaY, int deltaX)
        {
            PosY = (ushort)(PosY + deltaY);
            PosX = (ushort)(PosX + deltaX);
        }

        public void MoveRight() => Move(0, 1);
        public void MoveLeft() => Move(0, -1);
        public void MoveUp() => Move(-1, 0);
        public void MoveDown() => Move(1, 0);

        public void Show()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HERO)];
        }
        public bool CheckVictory()
        {
            if (Parent.Hero.PosX == Configuration.Columns - 1 && Parent.Hero.PosY == Configuration.Rows - 3)
            {
                MessageBox.Show("Victory - A way out");
                return true;
            }
            return false;
        }
    }
}