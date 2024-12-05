namespace Maze
{
    public partial class LevelForm : Form
    {
        public Maze maze;
        public Character Hero;
        public TextBox textBox;
        private AudioPlayer audioPlayer;
        private int timeLeft = 60;

        public LevelForm()
        {
            audioPlayer = new AudioPlayer();
            InitializeComponent();
            FormSettings();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            audioPlayer.PlaySound(@"C:\Users\Rog\Desktop\звуки\astral-creepy-dark-logo-254198.mp3");
            this.FormClosing += LevelForm_FormClosing;
            StartGameProcess();
        }

        public void FormSettings()
        {
            Text = Configuration.Title;
            BackColor = Configuration.Background;
            StartPosition = FormStartPosition.CenterScreen;
            statusStrip1.BackColor = Configuration.Background;
        }

        public void StartGameProcess()
        {
            Hero = new Character(this) { AmountOfHealth = 100 };
            maze = new Maze(this);
            UpdateStatusLabels();
            timeLeft = 60;
            toolStripStatusLabel4.Text = $"There's time left: {timeLeft} sec.";
            timer1.Start();
            maze.Generate();
            maze.Show();
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) MoveHero(0, 1);
            else if (e.KeyCode == Keys.Left && Hero.PosX > 0) MoveHero(0, -1);
            else if (e.KeyCode == Keys.Up && Hero.PosY > 0) MoveHero(-1, 0);
            else if (e.KeyCode == Keys.Down) MoveHero(1, 0);
        }

        private void MoveHero(int deltaY, int deltaX)
        {
            Hero.StepCount++;
            UpdateStepCount();

            var targetCell = maze.cells[Hero.PosY + deltaY, Hero.PosX + deltaX];
            if (targetCell.Type != CellType.WALL)
            {
                if (targetCell.Type == CellType.MEDAL) CollectMedal();
                if (targetCell.Type == CellType.ENEMY) TakeDamage();

                Hero.Clear();
                Hero.Move(deltaY, deltaX);
                Hero.Show();

                if (Hero.CheckVictory())
                {
                    audioPlayer.PlaySound(@"C:\Users\Rog\Desktop\звуки\goodresult-82807.mp3");
                    MessageBox.Show("Победа!");
                    this.Close();
                }
            }
        }

        private void CollectMedal()
        {
            Hero.MedalCount++;
            audioPlayer.PlaySound(@"C:\Users\Rog\Desktop\звуки\coin-257878.mp3");
            UpdateMedalCount();
        }

        private void TakeDamage()
        {
            Hero.AmountOfHealth -= 20;
            UpdateHealth();
            if (Hero.AmountOfHealth <= 0)
            {
                MessageBox.Show("You've lost! You're out of health points.");
                this.Close();
            }
        }

        private void UpdateStatusLabels()
        {
            toolStripStatusLabel1.Text = $"Medals: {Hero.MedalCount}";
            toolStripStatusLabel2.Text = $"Step Count: {Hero.StepCount}";
            toolStripStatusLabel3.Text = $"Amount Of Health: {Hero.AmountOfHealth}";
        }

        private void UpdateStepCount()
        {
            toolStripStatusLabel2.Text = $"Step Count: {Hero.StepCount}";
        }

        private void UpdateMedalCount()
        {
            toolStripStatusLabel1.Text = $"Medals: {Hero.MedalCount}";
        }

        private void UpdateHealth()
        {
            toolStripStatusLabel3.Text = $"Amount Of Health: {Hero.AmountOfHealth}";
        }

        private void LevelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioPlayer.Stop();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                toolStripStatusLabel4.Text = $"There's time left: {timeLeft} sec.";
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Time's up!");
                this.Close();
            }
        }
    }
}