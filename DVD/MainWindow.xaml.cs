using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DVD
{
   
    public partial class MainWindow : Window
    {

        private double xSpeed = 3;
        private double ySpeed = 3;
        private double xPos = 0;
        private double yPos = 0;
        private int score = 0;
        private DispatcherTimer timer;
       

        public MainWindow()
        {
            InitializeComponent();
            StartBouncing();
            ShowTimedText();
        }

        private  void StartBouncing()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += movement;
            timer.Start();


        }
        private void movement(object sender, EventArgs e)
        {
            double canvasWidth = mainBackground.ActualWidth;
            double canvasHeight = mainBackground.ActualHeight;

            xPos += xSpeed;
            yPos += ySpeed;


            if (xPos + DVDlogo.ActualWidth >= canvasWidth || xPos <= 0)
            {
                xSpeed = -xSpeed;

            }
            else if (xPos <= 0)
            {
                xPos = 0;
                xSpeed = -xSpeed;
            }
            if (yPos + DVDlogo.ActualHeight >= canvasHeight || yPos <= 0)
            {

                ySpeed = -ySpeed;

            }
            else if (yPos <= 0)
            {
                yPos = 0;
                ySpeed = -ySpeed;
            }
            Canvas.SetLeft(DVDlogo, xPos);
            Canvas.SetTop(DVDlogo, yPos);

            CornerScore(canvasWidth, canvasHeight);
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    yPos -= 5;
                    break;
                case Key.Right:
                    xPos += 5;
                    break;
                case Key.Left:
                    xPos -= 5;
                    break;
                case Key.Down:
                    yPos += 5;
                    break;
                case Key.Space:
                    TimedText.Text = string.Empty;  // Tar bort texten.
                    TimedText.Visibility = Visibility.Collapsed;  // Döljer textblock.
                    e.Handled = true;  // Markerar event som hanterad. 
                    break;
            }
    
                double canvasWidth = mainBackground.ActualWidth;
                double canvasHeight = mainBackground.ActualHeight;

                if (xPos < 0) xPos = 0;
                if (xPos + DVDlogo.ActualWidth > canvasWidth) xPos = canvasWidth - DVDlogo.ActualWidth;
                if (yPos < 0) yPos = 0;
                if (yPos + DVDlogo.ActualHeight > canvasHeight) yPos = canvasHeight - DVDlogo.ActualHeight;

                
                Canvas.SetLeft(DVDlogo, xPos);
                Canvas.SetTop(DVDlogo, yPos);

        }

        private void CornerScore(double canvasWidth, double canvasHeight)
        {
            bool TopLeft = (xPos <= 0 && yPos <= 0);
            bool TopRight = (xPos + DVDlogo.ActualWidth >= canvasWidth && yPos <= 0);
            bool BottomLeft = (xPos <= 0 && yPos + DVDlogo.ActualHeight >= canvasHeight);
            bool BottomRight = (xPos + DVDlogo.ActualWidth >= canvasWidth && yPos + DVDlogo.ActualHeight >= canvasHeight);

            if (TopLeft || TopRight || BottomLeft || BottomRight)
            {
                score++;

                UpdateScoreDisplay();
            }
        }
        private void UpdateScoreDisplay()
        {
            ScoreText.Text = $"SCORE: {score}";
        }



        private void ShowTimedText()
        {
            TimedText.Visibility = Visibility.Visible;
            TimedText.Text = "WELCOME TO DVD-GAME.\nCONTROL THE LOGO USING ARROWS.\nPRESS SPACE TO HIDE THIS MESSAGE.";
        }

    }
}