using System;
using System.Linq;
using System.Threading;
using AI2048.Game;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AI2048
{
    public class GamePage : IDisposable
    {
        private readonly RemoteWebDriver _driver;
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

        public string Score { get { return _driver.FindElement(By.ClassName("score-container")).Text.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries)[0]; } }

        /// <summary>
        /// numeration starts from Upper Left corner
        /// </summary>
        public Grid GridState
        {
            // tile format is: <div class="tile tile-32 tile-position-2-1 tile-merged">32</div>
            get
            {
                return retryOnSeleniumException(() =>
                {
                    var grid = new int[4, 4];

                    var tiles = _driver.FindElementsByClassName("tile");
                    foreach (var tl in tiles)
                    {
                        var positionStr = tl.GetAttribute("class").Split(' ').First(c => c.StartsWith("tile-position-")).Replace("tile-position-", "");
                        var x = int.Parse(positionStr[0].ToString()) - 1;
                        var y = int.Parse(positionStr[2].ToString()) - 1;
                        grid[x, y] = int.Parse(tl.Text);
                    }
                    return new Grid(grid);
                });
            }
        }

        public Grid GridStateNoNew
        {
            // tile format is: <div class="tile tile-32 tile-position-2-1 tile-merged">32</div>
            get
            {
                return retryOnSeleniumException(() =>
                {
                    var grid = new int[4, 4];

                    var tiles = _driver.FindElementsByClassName("tile").Where(t => !t.GetAttribute("class").Contains("tile-new"));
                    foreach (var tl in tiles)
                    {
                        var positionStr = tl.GetAttribute("class").Split(' ').First(c => c.StartsWith("tile-position-")).Replace("tile-position-", "");
                        var x = int.Parse(positionStr[0].ToString()) - 1;
                        var y = int.Parse(positionStr[2].ToString()) - 1;
                        grid[x, y] = int.Parse(tl.Text);
                    }
                    return new Grid(grid);
                });
            }
        }

        public int NewEl
        {
            get
            {
                return retryOnSeleniumException(() =>
                {
                    var newTile = _driver.FindElementsByClassName("tile").First(t => t.GetAttribute("class").Contains("tile-new"));
                    return int.Parse(newTile.Text);
                });
            }
        }

        private T retryOnSeleniumException<T>(Func<T> func, int retry = 1)
        {
            try
            {
                return func();
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(retry * 200);
                return retryOnSeleniumException(func, retry+1);
            }
            catch (FormatException) // the world is not yet ready, trying again
            {
                Thread.Sleep(retry * 200);
                return retryOnSeleniumException(func, retry + 1);
            }
        }

        public bool CanMove 
        {
            get { return _driver.FindElementByClassName("game-message").Displayed == false; }
        }

        public Screenshot TakeScreenshot()
        {
            return ((ITakesScreenshot) _driver).GetScreenshot();
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}