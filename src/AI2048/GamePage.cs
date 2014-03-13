using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AI2048
{
    public class GamePage : IDisposable
    {
        private readonly FirefoxDriver _driver;
        private readonly IWebElement _gameEl;

        public GamePage()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://gabrielecirulli.github.io/2048/");

            _gameEl = _driver.FindElementByClassName("game-container");
        }

        public void Turn(Move move)
        {
            switch (move)
            {
                case Move.Left:
                    _gameEl.SendKeys(Keys.Left);
                    break;
                case Move.Right:
                    _gameEl.SendKeys(Keys.Right);
                    break;
                case Move.Up:
                    _gameEl.SendKeys(Keys.Up);
                    break;
                case Move.Down:
                    _gameEl.SendKeys(Keys.Down);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("move");
            }

        }

        public string Score { get { return _driver.FindElement(By.ClassName("score-container")).Text.Split('\n')[0]; } }

        /// <summary>
        /// numeration starts from Upper Left corner
        /// </summary>
        public int[,] GridState
        {
            // tile format is: <div class="tile tile-32 tile-position-2-1 tile-merged">32</div>
            get
            {
                var grid = new int[4, 4];

                var tiles = _driver.FindElementsByClassName("tile");
                foreach (var tl in tiles)
                {
                    var positionStr = tl.GetAttribute("class").Split(' ').First(c => c.StartsWith("tile-position-")).Replace("tile-position-", "");

                    var x = int.Parse(positionStr[0].ToString())-1;
                    var y = int.Parse(positionStr[2].ToString())-1;
                    grid[x, y] = int.Parse(tl.Text);
                }
                return grid;
            }
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}